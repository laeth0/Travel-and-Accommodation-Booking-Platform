

using AutoMapper;
using MediatR;

namespace Booking.API.Controllers;

public class RoomBookingController : BaseController
{
    public RoomBookingController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
        : base(mapper, logger, mediator)
    {
    }


    /*


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))] // if there is no token(no role)
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index([FromHeader] string Authorization, [FromQuery] int PageSize = 0, [FromQuery] int PageNumber = 0, bool JustActiveBooking = false)
    {
        try
        {
            if (string.IsNullOrEmpty(Authorization))
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You should send a token" }
                });

            var userId = _serviceManager.TokenService.GetValueFromToken(Authorization, "Id");
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "Invalid token" }
                });


            IReadOnlyList<RoomBooking> RoomBooking;

            if (JustActiveBooking)
                RoomBooking = await _unitOfWork.RoomBookingRepository.GetAllAsync(PageSize, PageNumber, x => x.GuestId == userId && x.CheckIn > DateTime.Now);
            else
                RoomBooking = await _unitOfWork.RoomBookingRepository.GetAllAsync(PageSize, PageNumber, x => x.GuestId == userId);


            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Booking Rooms are retrieved successfully",
                Result = _mapper.Map<IReadOnlyList<RoomBookingResponseDTO>>(RoomBooking)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            });
        }
    }







    [HttpGet("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Details(Guid id)
    {
        try
        {
            var Booking = await _unitOfWork.RoomBookingRepository.GetByIdAsync(id);

            if (Booking is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Booking not found" }
                });


            return Ok(new SuccessResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Booking is retrieved successfully",
                Result = _mapper.Map<RoomBookingResponseDTO>(Booking)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            });
        }
    }








    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]// if the user role is not Admin or Manager
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))] // if there is no token(no role)
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Ctrate([FromForm] RoomBookingCreateRequestDTO model, [FromHeader] string Authorization)
    {
        // show this vedio https://www.youtube.com/watch?v=pDtDEVbEDdQ&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=16
        try
        {
            if (string.IsNullOrEmpty(Authorization))
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You should send a token" }
                });


            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = ModelState.Values
                                .SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage)
                                .ToList()
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Any(x => x == nameof(UserRoles.Manager) || x == nameof(UserRoles.Admin)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized to create a city" }
                });

            if (model.CheckIn < DateTime.Now || model.CheckOut < DateTime.Now)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "CheckIn and CheckOut should be greater than the current date" }
                });

            if (model.CheckIn >= model.CheckOut)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "CheckIn should be less than CheckOut" }
                });

            var room = await _unitOfWork.RoomRepository.GetByIdAsync(model.RoomId);
            if (room is null)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid Room Id" }
                });



            var RoomBooking = _mapper.Map<RoomBooking>(model);//   8/8/2024 10:00:00 AM
            RoomBooking.GuestId = _serviceManager.TokenService.GetValueFromToken(Authorization, "Id");

            //TimeSpan differenceTime = RoomBooking.CheckOut -  RoomBooking.CheckIn ;
            //RoomBooking.TotalPrice = differenceTime.Days * room.Price;
            RoomBooking.TotalPrice = RoomBooking.CheckOut.Subtract(RoomBooking.CheckIn).Days * room.Price;


            var createdBookingRoom = await _unitOfWork.RoomBookingRepository.AddAsync(RoomBooking);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Details), new { id = createdBookingRoom.Id }, new SuccessResponse
            {
                StatusCode = HttpStatusCode.Created,
                Message = "RoomBooking is created successfully",
                Result = _mapper.Map<RoomBookingResponseDTO>(createdBookingRoom)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            });

        }
    }





    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Update([FromForm] RoomBookingUpdateRequestDTO model, [FromHeader] string Authorization)
    {
        try
        {
            if (string.IsNullOrEmpty(Authorization))
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You should send a valid token" }
                });

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = ModelState.Values
                                .SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage)
                                .ToList()
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Any(x => x == nameof(UserRoles.Manager) || x == nameof(UserRoles.Admin)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized to update a city" }
                });

            if (model.CheckIn < DateTime.Now || model.CheckOut < DateTime.Now)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "CheckIn and CheckOut should be greater than the current date" }
                });

            if (model.CheckIn >= model.CheckOut)
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "CheckIn should be less than CheckOut" }
                });


            var RoomBooking = await _unitOfWork.RoomBookingRepository.GetByIdAsync(model.Id);

            if (RoomBooking is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "RoomBooking not found" }
                });

            RoomBooking = _mapper.Map(model, RoomBooking);

            RoomBooking.ModifiedAtUtc = DateTime.Now;
            _unitOfWork.RoomBookingRepository.Update(RoomBooking);

            await _unitOfWork.SaveChangesAsync();

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "RoomBooking is updated successfully",
                Result = _mapper.Map<RoomBookingResponseDTO>(RoomBooking)
            });


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            });
        }
    }








    [HttpDelete("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Delete([FromQuery] string id, [FromHeader] string Authorization)
    {
        try
        {
            if (string.IsNullOrEmpty(Authorization))
                return StatusCode(StatusCodes.Status403Forbidden, new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Errors = new List<string> { "You should send a valid token" }
                });

            if (!Guid.TryParse(id, out Guid GuidId))
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid RoomBooking Id" }
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Any(x => x == nameof(UserRoles.Manager) || x == nameof(UserRoles.Admin)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized to change user role" }
                });


            var RoomBooking = await _unitOfWork.RoomBookingRepository.GetByIdAsync(GuidId);
            if (RoomBooking is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "RoomBooking not found" }
                });


            _unitOfWork.RoomBookingRepository.Delete(RoomBooking);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "RoomBooking is deleted successfully",
                Result = _mapper.Map<RoomBookingResponseDTO>(RoomBooking)
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            });
        }
    }





    */



}
