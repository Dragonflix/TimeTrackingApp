using System.ComponentModel.DataAnnotations;

public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (comparisonProperty == null)
            return new ValidationResult($"Unknown property: {_comparisonProperty}");

        var comparisonValue = (DateTime)comparisonProperty.GetValue(validationContext.ObjectInstance);
        var currentValue = (DateTime)value;

        return currentValue > comparisonValue
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage);
    }
}