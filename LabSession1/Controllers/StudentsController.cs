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
        return students.Find(s => s.id == id);
    }

    [HttpGet("getStudentFilter/{name}")]
    public List<Student> GetFiltered(string name)
    {
        return students.FindAll(s => s.name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    [HttpGet("getCurrentDate")]
    public string GetDate()
    {
        var cultureHeader = HttpContext.Request.Headers["Accept-Language"].ToString();
        // here i splited the accept-language in the header by the comma to take the first part that contains en-EN etc.
        var culture = cultureHeader.Split(',').FirstOrDefault(); 
        var currentDate = DateTime.Now.ToString(new CultureInfo(culture));
        return currentDate;
    }
    
    [HttpPost("update")]
    public void UpdateStudentName([FromQuery] long id, [FromQuery] string name, [FromQuery] string email)
    {
        var student = students.Find(s => s.id == id);
        student.name = name;
        student.email = email;
    }
}