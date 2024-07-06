using FluentValidation;
using LabSession1.Models;

namespace LabSession1.FluentValidators;

public class ImageValidator : AbstractValidator<Image>
{
    private readonly string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
    public ImageValidator()
    {
        RuleFor(img => img.file)
            .NotNull().WithMessage("File must not be null");

        RuleFor(img => img.file.FileName)
            .NotEmpty().WithMessage("File name must not be empty")
            .Must(IsValidExtension).WithMessage("Invalid file extension");
    }
    private bool IsValidExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return validExtensions.Contains(extension.ToLower());
    }
}