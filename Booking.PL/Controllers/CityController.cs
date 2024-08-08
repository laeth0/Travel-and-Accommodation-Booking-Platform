

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Booking.PL.Controllers;

[Route("[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CityController> _logger;
    private readonly IServiceManager _serviceManager;

    public CityController(
        IUnitOfWork unitOfWork,
        IServiceManager serviceManager,
        IMapper mapper,
        ILogger<CityController> logger
        )
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));


        // this ( User.Identity.Name ) return the User Name

        /*
         manager token
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI2ZGEzYTU0Zi1iY2E3LTQyN2QtYTMyMC0yNmRiNTNlZWZkNDciLCJ1bmlxdWVfbmFtZSI6Ik1hbmFnZXIiLCJlbWFpbCI6Im1hbmFnZXJAZ21haWwuY29tIiwianRpIjoiNjlkMmZhZDItMzlmNi00ODBkLWFmOWUtN2FmMDJhN2Q5ZjY3IiwiUm9sZXMiOiJNYW5hZ2VyIiwibmJmIjoxNzIzMDk5MjA3LCJleHAiOjE3MjU2OTEyMDcsImlhdCI6MTcyMzA5OTIwNywiaXNzIjoiU2VjdXJlQXBpIiwiYXVkIjoiU2VjdXJlQXBpVXNlciJ9.URdXGGDwmbwV7E_33zdN8ANO6MpRuEYCKiK7e9zbYwU

         user token
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJlY2RhODViMS0zMDY2LTQ3OWUtOTY5ZS03NmY2ZDA1YTM0ODEiLCJ1bmlxdWVfbmFtZSI6IlN0cmluZ1N0cmluZyIsImVtYWlsIjoidXNlckBleGFtcGxlLmNvbSIsImp0aSI6Ijc3NzhjMGE5LTRkNDctNDQ4MC1iYzc5LTExMmJmYWI4YzliZCIsIlJvbGVzIjoiVXNlciIsIm5iZiI6MTcyMzA5OTM5NCwiZXhwIjoxNzI1NjkxMzk0LCJpYXQiOjE3MjMwOTkzOTQsImlzcyI6IlNlY3VyZUFwaSIsImF1ZCI6IlNlY3VyZUFwaVVzZXIifQ.Ebjky8GkH8xlKsXpkMEa4uWW8Z2M9qRDz9cfLVwZ82s
         */

        //var claimsIdentity = User.Identity as ClaimsIdentity;
        //var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //var userId = userIdClaim?.Value;
        //return Ok(userId);
        //-------------------------------------------------------------------------------------
        //var claimsIdentity = User.Claims.FirstOrDefault(x=>x.Type== ClaimTypes.NameIdentifier);
        //var userId = claimsIdentity?.Value;
        //return Ok(userId);


    }




    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
    public async Task<ActionResult> Index([FromQuery] int PageSize = 0, [FromQuery] int PageNumber = 0, [FromQuery] string? CountryId = null)
    {
        try
        {
            Guid GuidCountryId = Guid.Empty;
            if (!string.IsNullOrEmpty(CountryId) && !Guid.TryParse(CountryId, out GuidCountryId))
                return BadRequest(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid Country Id" }
                });


            IReadOnlyList<City> cities;
            if (string.IsNullOrEmpty(CountryId))
                cities = await _unitOfWork.CityRepository.GetAllAsync(PageSize, PageNumber);
            else
                cities = await _unitOfWork.CityRepository.GetAllAsync(PageSize, PageNumber, x => x.CountryId == GuidCountryId);


            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Cities are retrieved successfully",
                Result = _mapper.Map<IReadOnlyList<CityResponseDTO>>(cities)
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
            var city = await _unitOfWork.CityRepository.GetByIdAsync(id);

            if (city is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "City not found" }
                });


            return Ok(new SuccessResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "City is retrieved successfully",
                Result = _mapper.Map<CityResponseDTO>(city)
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




    /*
    when putting
    [Authorize(Roles ="Manager")]
    [Authorize(Roles = "Admin")]
    this mean that the user should have the two roles to access this method (كانو اند)

    but when putting
    [Authorize(Roles = "Manager,Admin")]
    this mean that the user should have one of the two roles to access this method (كانو اور)
     */
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]// if the user role is not Admin or Manager
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))] // if there is no token(no role)
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Ctrate([FromForm] CityCreateRequestDTO model, [FromHeader] string Authorization)
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

            var city = _mapper.Map<City>(model);
            city.ImageName = (await _serviceManager.FileService.UploadFile(model.Image))!;
            try
            {
                await _unitOfWork.CityRepository.AddAsync(city);
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

            _logger.LogInformation($"City with id {city.Id} is created");

            return CreatedAtAction(nameof(Details), new { id = city.Id }, new SuccessResponse
            {
                StatusCode = HttpStatusCode.Created,
                Message = "City is created successfully",
                Result = _mapper.Map<CityResponseDTO>(city)
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
    public async Task<ActionResult> Update([FromForm] CityUpdateRequestDTO model, [FromHeader] string Authorization)
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


            var City = await _unitOfWork.CityRepository.GetByIdAsync(model.Id);

            if (City is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "City not found" }
                });

            var OldImageName = City.ImageName;
            City = _mapper.Map(model, City);

            if (model.Image is { })
                City.ImageName = (await _serviceManager.FileService.UploadFile(model.Image))!;

            try
            {
                _unitOfWork.CityRepository.Update(City);
                await _unitOfWork.SaveChangesAsync();
                _serviceManager.FileService.DeleteFile(OldImageName);

                return Ok(new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "City is updated successfully",
                    Result = _mapper.Map<CityResponseDTO>(City)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _serviceManager.FileService.DeleteFile(City.ImageName);

                if (!string.IsNullOrEmpty(City.ImageName))
                    _serviceManager.FileService.DeleteFile(City.ImageName);

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
                    Errors = new List<string> { "Invalid City Id" }
                });

            var userRoles = _serviceManager.TokenService.GetValueFromToken(Authorization).Split(',');
            if (!userRoles.Any(x => x == nameof(UserRoles.Manager) || x == nameof(UserRoles.Admin)))
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Errors = new List<string> { "You are not authorized to change user role" }
                });


            var City = await _unitOfWork.CityRepository.GetByIdAsync(GuidId);
            if (City is null)
                return NotFound(new ErrorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "City not found" }
                });


            _unitOfWork.CityRepository.Delete(City);
            await _unitOfWork.SaveChangesAsync();
            _serviceManager.FileService.DeleteFile(City.ImageName);

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "City is deleted successfully",
                Result = _mapper.Map<CityResponseDTO>(City)
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
