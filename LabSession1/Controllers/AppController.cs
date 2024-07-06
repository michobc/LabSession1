using LabSession1.Models;
using LabSession1.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabSession1.Controllers;

[ApiController]
public class AppController : ControllerBase
{
    private readonly IAppService _appService;
    public AppController(IAppService appService)
    {
        _appService = appService;
    }
    
    [HttpGet("getCurrentDate")]
    public string GetDate([FromHeader] string lang)
    {
        //var cultureHeader = HttpContext.Request.Headers["Accept-Language"].ToString();
        // here i splited the accept-language in the header by the comma to take the first part that contains en-EN etc.
        // var culture = cultureHeader.Split(',').FirstOrDefault();
        return _appService.GetDate(lang);
    }
    
    [HttpPost("uploadImage")]
    public Task<string> UploadImage([FromForm] Image image)
    {
        return _appService.UploadImage(image);
    }
}