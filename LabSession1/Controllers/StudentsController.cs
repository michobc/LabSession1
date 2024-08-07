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
    private readonly IObjectMapperService _objectMapperService;

    public StudentsController(IStudentService studentService, IObjectMapperService objectMapperService)
    {
        _studentService = studentService;
        _objectMapperService = objectMapperService;
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
    
    [HttpGet("mapStudentToPerson/{id}")]
    public ActionResult<Person> MapStudentToPerson(long id)
    {
        try
        {
            var student = _studentService.GetByID(id);
            var person = _objectMapperService.Map<Student, Person>(student);
            return Ok(person);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}