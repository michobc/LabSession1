using System.ComponentModel.DataAnnotations;

namespace LabSession1.Models;

public class Student
{
    [Required(ErrorMessage = "Please enter your Id")] 
    public long id { get; set; }
    
    [Required]
    [MinLength(3, ErrorMessage="Please enter at least 3 characters")]
    public string name { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage="Please enter a valid email address")]
    public string email { get; set; }
}