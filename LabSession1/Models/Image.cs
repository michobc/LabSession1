using System.ComponentModel.DataAnnotations;

namespace LabSession1.Models;

public class Image
{
    public string name { get; set; }
    [Required]
    public IFormFile file { get; set; }
}