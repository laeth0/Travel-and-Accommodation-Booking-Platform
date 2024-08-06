


namespace Booking.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResidenceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;
    private readonly ILogger<ResidenceController> _logger;

    public ResidenceController(
        IUnitOfWork unitOfWork,
        IServiceManager serviceManager,
        IMapper mapper,
        ILogger<ResidenceController> logger
        )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }




    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index([FromQuery] string? ResidenceType, [FromQuery] string? CityId, [FromQuery] string? OwnerId, [FromQuery] int PageSize = 0, [FromQuery] int PageNumber = 0)
    {
        try
        {
            IEnumerable<Residence> Residences = await _unitOfWork.ResidenceRepository.GetAllAsync(PageSize, PageNumber);


            if (!string.IsNullOrEmpty(ResidenceType))
                Residences = Residences.Where(x => x.Type == ResidenceType);


            if (!string.IsNullOrEmpty(CityId))
            {
                if (!Guid.TryParse(CityId, out Guid GuidCityId))
                    return BadRequest(new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Invalid City Id" }
                    });
                Residences = Residences.Where(x => x.CityId == GuidCityId);
            }


            if (!string.IsNullOrEmpty(OwnerId))
                Residences = Residences.Where(x => x.OwnerId == OwnerId);


            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Residence are retrieved successfully",
                Result = _mapper.Map<IReadOnlyList<ResidenceResponseDTO>>(Residences.ToList())
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





    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Details([FromQuery] string id)
    {
        try
        {
            if (!Guid.TryParse(id, out Guid GuidId))
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid Residence Id" }
                });

            var Residence = await _unitOfWork.ResidenceRepository.GetByIdAsync(GuidId);

            if (Residence is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Residence not found" }
                });

            return Ok(new SuccessResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Citiy is retrieved successfully",
                Result = _mapper.Map<ResidenceResponseDTO>(Residence)
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
    public async Task<ActionResult> Ctrate([FromForm] ResidenceCreateRequestDTO model, [FromHeader] string Authorization)
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
                    Errors = new List<string> { "You are not authorized to create a residence" }
                });

            if (!Enum.GetNames<ResidencesTypes>().Contains(model.Type))
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid Residence Type" }
                });

            var Residence = _mapper.Map<Residence>(model);
            Residence.ImageName = (await _serviceManager.FileService.UploadFile(model.Image))!;
            try
            {
                await _unitOfWork.ResidenceRepository.AddAsync(Residence);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Residence with id {Residence.Id} is created");

                return CreatedAtAction(nameof(Details), new { id = Residence.Id }, new SuccessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Residence is created successfully",
                    Result = _mapper.Map<ResidenceResponseDTO>(Residence)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (!string.IsNullOrEmpty(Residence.ImageName))
                    _serviceManager.FileService.DeleteFile(Residence.ImageName);

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







    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Update([FromForm] ResidenceUpdateRequestDTO model, [FromHeader] string Authorization)
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

            if (!Enum.GetNames<ResidencesTypes>().Contains(model.Type))
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid Residence Type" }
                });

            var Residence = await _unitOfWork.ResidenceRepository.GetByIdAsync(model.Id);

            if (Residence is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Residence not found" }
                });

            var OldImageName = Residence.ImageName;
            Residence = _mapper.Map(model, Residence);

            if (model.Image is { })
                Residence.ImageName = (await _serviceManager.FileService.UploadFile(model.Image))!;

            try
            {
                var ResidenceResponse = _unitOfWork.ResidenceRepository.Update(Residence);
                await _unitOfWork.SaveChangesAsync();
                _serviceManager.FileService.DeleteFile(OldImageName);

                return Ok(new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Residence is updated successfully",
                    Result = _mapper.Map<ResidenceResponseDTO>(ResidenceResponse)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _serviceManager.FileService.DeleteFile(Residence.ImageName);

                if (!string.IsNullOrEmpty(Residence.ImageName))
                    _serviceManager.FileService.DeleteFile(Residence.ImageName);

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
                    Errors = new List<string> { "Invalid Residence Id" }
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Any(x => x == nameof(UserRoles.Manager) || x == nameof(UserRoles.Admin)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized to change user role" }
                });


            var Residence = await _unitOfWork.ResidenceRepository.GetByIdAsync(GuidId);
            if (Residence is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Residence not found" }
                });


            var ResidenceResponse = _unitOfWork.ResidenceRepository.Delete(Residence);
            await _unitOfWork.SaveChangesAsync();
            _serviceManager.FileService.DeleteFile(Residence.ImageName);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Residence is deleted successfully",
                Result = _mapper.Map<ResidenceResponseDTO>(ResidenceResponse)
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










}
