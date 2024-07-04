using LabSession1.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabSession1.Services;

public interface IStudentService
{
    // GET REQUESTS
    List<Student> GetAll();
    Student GetByID(long id);
    List<Student> GetFiltered(string name);
    string GetDate(string culture);
    // POST REQUESTS
    void UpdateStudentName(Student request);
    Task<string> UploadImage(Image image);
    // DELETE
    void DeleteStudent(long id);
}