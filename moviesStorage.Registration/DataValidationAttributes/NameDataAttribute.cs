using System.ComponentModel.DataAnnotations;

namespace moviesStorage.Registration.DataValidationAttributes;

public class NameDataAttribute : StringLengthAttribute
{
    public NameDataAttribute(int maximumLength) : base(maximumLength)
    {
        ErrorMessage = $"This field should be from {MinimumLength} to {MaximumLength} characters long.";
        MinimumLength = 3;
    }
    
}