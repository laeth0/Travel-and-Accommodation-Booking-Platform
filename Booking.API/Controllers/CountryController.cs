
using Booking.PL.Commands;
using Booking.PL.queries;
using MediatR;

namespace Booking.PL.Controllers;

public class CountryController : BaseController
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;


    public CountryController(
        IUnitOfWork unitOfWork,
        IServiceManager serviceManager,
        IMapper mapper,
        ILogger<CountryController> logger,
        IMediator mediator) : base(unitOfWork, mapper, logger, mediator)
    {
        _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    // this ( User.Identity.Name ) return the User Name

    //var claimsIdentity = User.Identity as ClaimsIdentity;
    //var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
    //var userId = userIdClaim?.Value;
    //return Ok(userId);
    //-------------------------------------------------------------------------------------
    //var claimsIdentity = User.Claims.FirstOrDefault(x=>x.Type== ClaimTypes.NameIdentifier);
    //var userId = claimsIdentity?.Value;
    //return Ok(userId);



    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index([FromQuery] int PageSize = 0, [FromQuery] int PageNumber = 0, CancellationToken cancellationToken = default)
    {
        var Countries = await _mediator.Send(new GetCountryQuery(), cancellationToken);
        var mappedCountries = _mapper.Map<IReadOnlyList<CountryResponseDTO>>(Countries);
        
        return Problem(statusCode: StatusCodes.Status400BadRequest,
            detail: "Countries are retrieved successfully"
            );

    }



    /*

    [HttpGet("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Details(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {

            var Country = await _unitOfWork.CountryRepository.GetByIdAsync(id);

            if (Country is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Country not found" }
                });

            return Ok(new SuccessResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Citiy is retrieved successfully",
                Result = _mapper.Map<CountryResponseDTO>(Country)
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

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]// if the user role is not Admin or Manager
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))] // if there is no token(no role)
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Ctrate([FromForm] CountryCreateRequestDTO model, CancellationToken cancellationToken = default)
    {
        // show this vedio https://www.youtube.com/watch?v=pDtDEVbEDdQ&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=16
        var Country = _mapper.Map<Country>(model);
        await _mediator.Send(new AddCountryCommand(Country), cancellationToken);

        return Ok(_mapper.Map<CountryResponseDTO>(Country));
    }


    /*




    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Update([FromForm] CountryUpdateRequestDTO model, [FromHeader] string Authorization, CancellationToken cancellationToken = default)
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
                    Errors = new List<string> { "You are not authorized to update a residence" }
                });


            var Country = await _unitOfWork.CountryRepository.GetByIdAsync(model.Id);

            if (Country is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Country not found" }
                });

            var OldImageName = Country.ImageName;
            Country = _mapper.Map(model, Country);

            if (model.Image is { })
                Country.ImageName = (await _serviceManager.FileService.UploadFile(model.Image))!;

            try
            {
                var CountryResponse = _unitOfWork.CountryRepository.Update(Country);
                await _unitOfWork.SaveChangesAsync();
                _serviceManager.FileService.DeleteFile(OldImageName);

                return Ok(new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Country is updated successfully",
                    Result = _mapper.Map<CountryResponseDTO>(CountryResponse)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _serviceManager.FileService.DeleteFile(Country.ImageName);

                if (!string.IsNullOrEmpty(Country.ImageName))
                    _serviceManager.FileService.DeleteFile(Country.ImageName);

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
    public async Task<ActionResult> Delete([FromQuery] string id, [FromHeader] string Authorization, CancellationToken cancellationToken = default)
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
                    Errors = new List<string> { "Invalid Country Id" }
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Any(x => x == nameof(UserRoles.Manager) || x == nameof(UserRoles.Admin)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized to change user role" }
                });


            var Country = await _unitOfWork.CountryRepository.GetByIdAsync(GuidId);
            if (Country is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Country not found" }
                });


            var CountryResponse = _unitOfWork.CountryRepository.Delete(Country);
            await _unitOfWork.SaveChangesAsync();
            _serviceManager.FileService.DeleteFile(Country.ImageName);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Country is deleted successfully",
                Result = _mapper.Map<CountryResponseDTO>(CountryResponse)
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
