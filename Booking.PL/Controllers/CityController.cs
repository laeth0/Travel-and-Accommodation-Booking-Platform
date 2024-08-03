using AutoMapper;
using Booking.BLL.Enums;
using Booking.BLL.Interfaces;
using Booking.BLL.IService;
using Booking.DAL.Entities;
using Booking.PL.CustomizeResponses;
using Booking.PL.DTO.City;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;
        private readonly IFileService _fileService;

        public CityController(
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CityController> logger,
            IFileService fileService
            )
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }




        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        //[ResponseCache(CacheProfileName = "Default60Sec")] //[ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
        public async Task<ActionResult> Index([FromQuery] int PageSize = 0, [FromQuery] int PageNumber = 0)
        {
            try
            {
                var cities = await _unitOfWork.CityRepository.GetAllAsync(PageSize, PageNumber);
                var mappedCities = _mapper.Map<IReadOnlyList<CityResponseDTO>>(cities);
                var response = new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Cities are retrieved successfully",
                    Result = mappedCities
                };
                return Ok(response);
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
                {
                    var Response = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Invalid City Id" }
                    };
                    _logger.LogWarning("Invalid Model State");
                    return BadRequest(Response);
                }
                var city = await _unitOfWork.CityRepository.GetByIdAsync(GuidId);

                if (city is null)
                {
                    var NotFoundResponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "City not found" }
                    };
                    return NotFound(NotFoundResponse);
                }

                var cityResponse = _mapper.Map<CityResponseDTO>(city);
                var response = new SuccessResponse
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Citiy is retrieved successfully",
                    Result = cityResponse
                };

                return Ok(response);
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




        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuccessResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]// if the user role is not Admin or Manager
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))] // if there is no token(no role)
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ApiResponse>> Ctrate([FromForm] CityCreateRequestDTO model, [FromHeader] string Authorization)
        {
            // show this vedio https://www.youtube.com/watch?v=pDtDEVbEDdQ&list=PL3ewn8T-zRWgO-GAdXjVRh-6thRog6ddg&index=16
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                {
                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        Errors = new List<string> { "You should send a token" }
                    };
                    return StatusCode(StatusCodes.Status403Forbidden, errorresponse);
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var validationResponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = errors
                    };
                    return BadRequest(validationResponse);
                }

                var userRole = _tokenService.GetValueFromToken(Authorization, "Role");
                if (userRole != nameof(UserRoles.Admin) && userRole != nameof(UserRoles.Manager))
                {
                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Errors = new List<string> { "You are not authorized to create a City" }
                    };
                    return StatusCode(StatusCodes.Status401Unauthorized, errorresponse);
                }


                var city = _mapper.Map<City>(model);
                city.ImageName = (await _fileService.UploadFile(model.Image))!;
                try
                {
                    await _unitOfWork.CityRepository.AddAsync(city);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { ex.Message }
                    };

                    if (!string.IsNullOrEmpty(city.ImageName))
                        _fileService.DeleteFile(city.ImageName);

                    return BadRequest(errorresponse);
                }


                var CityResponse = _mapper.Map<CityResponseDTO>(city);
                var response = new SuccessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "City is created successfully",
                    Result = CityResponse
                };

                _logger.LogInformation($"City with id {city.Id} is created");
                return CreatedAtAction(nameof(Details), new { id = city.Id }, response);
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
                {
                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        Errors = new List<string> { "You should send a valid token" }
                    };
                    return StatusCode(StatusCodes.Status403Forbidden, errorresponse);
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var validationResponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = errors
                    };
                    return BadRequest(validationResponse);
                }

                var userRole = _tokenService.GetValueFromToken(Authorization, "Role");
                if (userRole != nameof(UserRoles.Admin) && userRole != nameof(UserRoles.Manager))
                {
                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Errors = new List<string> { "You are not authorized to create a product" }
                    };
                    return StatusCode(StatusCodes.Status401Unauthorized, errorresponse);
                }

                var City = await _unitOfWork.CityRepository.GetByIdAsync(model.Id);

                if (City is null)
                {
                    var NotFoundResponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "City not found" }
                    };
                    return NotFound(NotFoundResponse);
                }

                var OldImageName = City.ImageName;
                City = _mapper.Map(model, City);

                if (model.Image is { })
                    City.ImageName = (await _fileService.UploadFile(model.Image))!;

                try
                {
                    _unitOfWork.CityRepository.Update(City);
                    await _unitOfWork.SaveChangesAsync();
                    _fileService.DeleteFile(OldImageName);

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
                    _fileService.DeleteFile(City.ImageName);

                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { ex.Message }
                    };

                    if (!string.IsNullOrEmpty(City.ImageName))
                        _fileService.DeleteFile(City.ImageName);

                    return BadRequest(errorresponse);
                }


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
                {
                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        Errors = new List<string> { "You should send a valid token" }
                    };
                    return StatusCode(StatusCodes.Status403Forbidden, errorresponse);
                }

                if (!Guid.TryParse(id, out Guid GuidId))
                {
                    var Response = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string> { "Invalid City Id" }
                    };
                    _logger.LogWarning("Invalid Model State");
                    return BadRequest(Response);
                }

                var userRole = _tokenService.GetValueFromToken(Authorization, "Role");
                if (userRole != nameof(UserRoles.Admin) && userRole != nameof(UserRoles.Manager))
                {
                    var errorresponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Errors = new List<string> { "You are not authorized to create a product" }
                    };
                    return StatusCode(StatusCodes.Status401Unauthorized, errorresponse);
                }

                var City = await _unitOfWork.CityRepository.GetByIdAsync(GuidId);
                if (City is null)
                {
                    var NotFoundResponse = new ErrorResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Errors = new List<string> { "City not found" }
                    };
                    return NotFound(NotFoundResponse);
                }

                var SuccessResponse = new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "City is deleted successfully",
                    Result = _mapper.Map<CityResponseDTO>(City)
                };

                _unitOfWork.CityRepository.Delete(City);
                await _unitOfWork.SaveChangesAsync();
                _fileService.DeleteFile(City.ImageName);

                _logger.LogInformation($"City with id {City.Id} is deleted");
                return Ok(SuccessResponse);
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




    }
}
