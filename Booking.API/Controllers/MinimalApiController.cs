



using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController, Route("/")]
public class MinimalApiController : ControllerBase
{

    [HttpGet]
    public ActionResult Index()
    {
        return Content("Hello World!");
    }
    /*

    [HttpGet("Images/{ImageName}")]
    public ActionResult GetImage(string ImageName)
    {
        try
        {
            var Imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", "images", ImageName);

            if (System.IO.File.Exists(Imagepath))
            {
                // The using statement disposes of the FileStream before it is fully read by the Results.File method so we dont use "using" here.
                var fileStream = new FileStream(Imagepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return File(fileStream, "image/png"); // FileStream is being disposed by the Results.File method once the response is sent.
            }
            return NotFound(new ErrorResponse
            {
                Errors = new List<string> { "Image Not Found" }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse { Errors = new List<string> { ex.Message } });
        }
    }



    [HttpGet("[action]")]
    public ActionResult GetResidenceTypes()
    {
        try
        {
            var residenceTypes = Enum.GetValues<ResidencesTypes>()
                    .Select(x => new ResidenceTypesResponseDTO { Id = (int)x, Name = x.ToString() })
                    .ToList();

            // other way
            //var residenceTypesValues = Enum.GetValues<ResidencesTypes>();
            //var residenceTypesNames = Enum.GetNames<ResidencesTypes>();
            //var residenceTypes = residenceTypesValues.Zip(residenceTypesNames,
            //    (value, name) => new ResidenceTypesResponseDTO { Id = (int)value, Name = name }).ToList();

            return Ok(new SuccessResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Residence Types retrieved successfully",
                Result = residenceTypes
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResponse { Errors = new List<string> { ex.Message } });
        }
    }
    */


}
