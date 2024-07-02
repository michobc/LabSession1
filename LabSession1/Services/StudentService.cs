using System.Globalization;
using LabSession1.Models;
using Microsoft.AspNetCore.Mvc;
namespace LabSession1.Services;

public class StudentService : IStudentService
{
    private static List<Student> students = new List<Student> 
    {
        new Student{id = 211211, name = "Mario", email = "mario@inmind.lb"},
        new Student{id = 122334, name = "kevin", email = "kevin@inmind.lb"},
        new Student{id = 233445, name = "Joe", email = "joe@inmind.lb"},
        new Student{id = 344556, name = "Roudy", email = "roudy@inmind.lb"}
    };
    
    public List<Student> GetAll() 
    {
        return students;
    }
    
    public Student GetByID(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid ID");
        }
        var student = students.Find(s => s.id == id);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }
        return student;
    }
    
    public List<Student> GetFiltered(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        return students.FindAll(s => s.name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
    
    public string GetDate(string culture)
    {
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
    
    public void UpdateStudentName([FromBody] Student request)
    {
        if (request.id <= 0 || string.IsNullOrEmpty(request.name) || string.IsNullOrEmpty(request.email))
        {
            throw new ArgumentException("Invalid request data");
        }
        var student = students.Find(s => s.id == request.id);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }
        student.name = request.name;
        student.email = request.email;
    }
    
    // in progress
    public async Task<string> UploadImage([FromForm] IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            throw new ArgumentException("Image is required.");
        }

        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        var fileName = Path.GetFileName(image.FileName);
        var filePath = Path.Combine(uploadDirectory, fileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error uploading file: {fileName}. {ex.Message}");
        }

        return $"images/{fileName}";
    }
    
    public void DeleteStudent(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid ID");
        }
        var student = students.Find(s => s.id == id);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }
        students.Remove(student);
    }
}