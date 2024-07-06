using System.ComponentModel.DataAnnotations;

namespace LabSession1.Models;

public class Image
{
    [Required]
    public IFormFile file { get; set; }
}