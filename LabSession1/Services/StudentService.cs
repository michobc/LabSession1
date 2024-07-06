using System.Globalization;
using FluentValidation.Results;
using LabSession1.FluentValidators;
using LabSession1.Models;
using Microsoft.AspNetCore.Mvc;
namespace LabSession1.Services;
//TODO: Check Fluent Validation

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
        var validator = new StudentIDValidator();
        ValidationResult result = validator.Validate(id);
        
        if (!result.IsValid)
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
    
    public void UpdateStudentName(Student request)
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