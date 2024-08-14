


using AutoMapper;
using Booking.API.CustomizeResponses;
using Booking.Application.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

public class RoomController : BaseController
{
    public RoomController(IMapper mapper, ILogger<BaseController> logger, IMediator mediator)
     : base(mapper, logger, mediator)
    {
    }




    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> GetAllRoomType(CancellationToken cancellationToken = default)
    {

        var query = new GetAllRoomTypeQuery();

        var RoomTypes = await _mediator.Send(query, cancellationToken);

        var response = new SuccessResponse { data = RoomTypes };

        return Ok(response);

    }



    /*
    /// <summary>
    ///  get all rooms with optional pagination 
    /// </summary>
    /// <remarks>
    /// sample request:
    /// 
    ///     GET /api/Room
    ///     {
    ///     }
    /// </remarks>
    /// <param name="PageSize"></param>
    /// <param name="PageNumber"></param>
    /// <returns>a list of rooms</returns>
    /// <response code="200">if the rooms are retrieved successfully</response>
    /// <response code="500">if there is an internal server error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index([FromQuery] int PageSize = 0, [FromQuery] int PageNumber = 0)
    {
        try
        {
            var rooms = await _unitOfWork.RoomRepository.GetAllAsync(PageSize, PageNumber);
            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Rooms are retrieved successfully",
                Result = _mapper.Map<IReadOnlyList<RoomResponseDTO>>(rooms)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            var response = new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = new List<string> { "Internal Server Error", ex.Message }
            };
            return StatusCode(StatusCodes.Status500InternalServerError, response);
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
            var city = await _unitOfWork.RoomRepository.GetByIdAsync(id);

            if (city is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Room not found" }
                });


            return Ok(new SuccessResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Room is retrieved successfully",
                Result = _mapper.Map<RoomResponseDTO>(city)
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
    public async Task<ActionResult> Ctrate([FromForm] RoomCreateRequestDTO model, [FromHeader] string Authorization)
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

            var city = _mapper.Map<Room>(model);
            city.ImageName = (await _serviceManager.FileService.UploadFile(model.Image))!;
            try
            {
                await _unitOfWork.RoomRepository.AddAsync(city);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (!string.IsNullOrEmpty(city.ImageName))
                    _serviceManager.FileService.DeleteFile(city.ImageName);

                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                });
            }

            _logger.LogInformation($"Room with id {city.Id} is created");

            return CreatedAtAction(nameof(Details), new { id = city.Id }, new SuccessResponse
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Room is created successfully",
                Result = _mapper.Map<RoomResponseDTO>(city)
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
    public async Task<ActionResult> Update([FromForm] RoomUpdateRequestDTO model, [FromHeader] string Authorization)
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


            var Room = await _unitOfWork.RoomRepository.GetByIdAsync(model.Id);

            if (Room is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Room not found" }
                });

            var OldImageName = Room.ImageName;
            Room = _mapper.Map(model, Room);

            if (model.Image is { })
                Room.ImageName = (await _serviceManager.FileService.UploadFile(model.Image))!;

            try
            {
                _unitOfWork.RoomRepository.Update(Room);
                await _unitOfWork.SaveChangesAsync();
                _serviceManager.FileService.DeleteFile(OldImageName);

                return Ok(new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Room is updated successfully",
                    Result = _mapper.Map<RoomResponseDTO>(Room)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _serviceManager.FileService.DeleteFile(Room.ImageName);

                if (!string.IsNullOrEmpty(Room.ImageName))
                    _serviceManager.FileService.DeleteFile(Room.ImageName);

                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                });
            }


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
                    Errors = new List<string> { "Invalid Room Id" }
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Any(x => x == nameof(UserRoles.Manager) || x == nameof(UserRoles.Admin)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized to change user role" }
                });


            var Room = await _unitOfWork.RoomRepository.GetByIdAsync(GuidId);
            if (Room is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Room not found" }
                });


            _unitOfWork.RoomRepository.Delete(Room);
            await _unitOfWork.SaveChangesAsync();
            _serviceManager.FileService.DeleteFile(Room.ImageName);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Room is deleted successfully",
                Result = _mapper.Map<RoomResponseDTO>(Room)
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
