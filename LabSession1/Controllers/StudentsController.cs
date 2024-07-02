using System.Globalization;
using LabSession1.Models;
using Microsoft.AspNetCore.Mvc;
namespace LabSession1.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private static List<Student> students = new List<Student> 
    {
        new Student{id = 211211, name = "Mario", email = "mario@inmind.lb"},
        new Student{id = 122334, name = "kevin", email = "kevin@inmind.lb"},
        new Student{id = 233445, name = "Joe", email = "joe@inmind.lb"},
        new Student{id = 344556, name = "Roudy", email = "roudy@inmind.lb"}
    };
    
    [HttpGet("getAllStudents")]
    public List<Student> GetAll() 
    {
        return students;
    }

    [HttpGet("getStudentByID/{id}")]
    public Student GetByID(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid ID");
        }
        var student = students.Find(s => s.id == id);
        if (student == null)
        {
            throw new ArgumentException("no student with this id");
        }
        return student;
    }

    [HttpGet("getStudentFilter/{name}")]
    public List<Student> GetFiltered(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        return students.FindAll(s => s.name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    [HttpGet("getCurrentDate")]
    public string GetDate()
    {
        var cultureHeader = HttpContext.Request.Headers["Accept-Language"].ToString();
        // here i splited the accept-language in the header by the comma to take the first part that contains en-EN etc.
        var culture = cultureHeader.Split(',').FirstOrDefault(); 
        try
        {
            var currentDate = DateTime.Now.ToString(new CultureInfo(culture));
            return currentDate;
        }
        catch (CultureNotFoundException)
        {
            throw new ArgumentException("Invalid culture identifier");
        }
    }
    
    [HttpPost("update")]
    public ActionResult UpdateStudentName([FromBody] Student request)
    {
        if (request.id <= 0 || string.IsNullOrEmpty(request.name) || string.IsNullOrEmpty(request.email))
        {
            throw new ArgumentException("Invalid request data");
        }
        var student = students.Find(s => s.id == request.id);
        if (student == null)
        {
            return NotFound();
        }
        student.name = request.name;
        student.email = request.email;
        return Ok(student);
    }
    // in progress
    [HttpPost("upload")]
    public async Task<ActionResult> UploadImage([FromForm] IFormFile image)
    {
        if (image.Length == 0)
        {
            return BadRequest("Image is required.");
        }
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        return Ok(new { path = $"images/{image.FileName}" });
    }
    
    [HttpDelete("deleteStudent/{id}")]
    public ActionResult DeleteStudent(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid ID");
        }
        var student = students.Find(s => s.id == id);
        if (student == null)
        {
            return NotFound();
        }
        students.Remove(student);
        return NoContent();
    }
}