using System.Globalization;
using FluentValidation.Results;
using LabSession1.FluentValidators;
using LabSession1.Models;

namespace LabSession1.Services;

public class AppService : IAppService
{
    public string GetDate(string culture)
    {
        try
        {
            var currentDate = DateTime.Now.ToString("D",new CultureInfo(culture));
            return currentDate;
        }
        catch (CultureNotFoundException)
        {
            throw new ArgumentException("Invalid culture identifier");
        }
    }
    
    public async Task<string> UploadImage(Image image)
    {
        var validator = new ImageValidator();
        ValidationResult result = validator.Validate(image);
        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }
        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        var fileName = Path.GetFileName(image.file.FileName);
        var filePath = Path.Combine(uploadDirectory, fileName);
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.file.CopyToAsync(stream);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error uploading file: {fileName}. {ex.Message}");
        }
        return $"images/{fileName}";
    }
}