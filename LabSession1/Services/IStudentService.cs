using LabSession1.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabSession1.Services;

public interface IStudentService
{
    // GET REQUESTS
    List<Student> GetAll();
    Student GetByID(long id);
    List<Student> GetFiltered(string name);
    // POST REQUESTS
    void UpdateStudentName(Student request);
    // DELETE
    void DeleteStudent(long id);
}