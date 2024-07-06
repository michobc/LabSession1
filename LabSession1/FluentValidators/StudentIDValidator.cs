using FluentValidation;
using LabSession1.Models;

namespace LabSession1.FluentValidators;

public class StudentIDValidator : AbstractValidator<long>
{
    public StudentIDValidator()
    {
        RuleFor(id => id).GreaterThan(0).WithMessage("Invalid ID");
    }
}