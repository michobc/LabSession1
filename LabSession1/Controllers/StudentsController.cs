using System.Globalization;
using LabSession1.Models;
using LabSession1.Services;
using Microsoft.AspNetCore.Mvc;
namespace LabSession1.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet("getAllStudents")]
    public List<Student> GetAll() 
    {
        return _studentService.GetAll();
    }

    [HttpGet("getStudentByID/{id}")]
    public Student GetByID(long id)
    {
        return _studentService.GetByID(id);
    }

    [HttpGet("getStudentFilter/{name}")]
    public List<Student> GetFiltered(string name)
    {
        return _studentService.GetFiltered(name);
    }

    [HttpGet("getCurrentDate")]
    public string GetDate()
    {
        var cultureHeader = HttpContext.Request.Headers["Accept-Language"].ToString();
        // here i splited the accept-language in the header by the comma to take the first part that contains en-EN etc.
        var culture = cultureHeader.Split(',').FirstOrDefault();
        return _studentService.GetDate(culture);
    }
    
    [HttpPost("update")]
    public ActionResult UpdateStudentName([FromBody] Student request)
    {
        try
        {
            _studentService.UpdateStudentName(request);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        return Ok();
    }
    

    [HttpPost("upload")]
    public Task<string> UploadImage([FromForm] Image image)
    {
        return _studentService.UploadImage(image);
    }
    
    [HttpDelete("deleteStudent/{id}")]
    public ActionResult DeleteStudent(long id)
    {
        try
        {
            _studentService.DeleteStudent(id);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        return Ok();
    }
}