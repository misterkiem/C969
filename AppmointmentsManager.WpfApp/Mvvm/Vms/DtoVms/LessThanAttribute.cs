using System.ComponentModel.DataAnnotations;

namespace AppointmentsManager.WpfApp.Mvvm.Vms.DtoVms;
public class LessThanAttribute : ValidationAttribute
{
    public LessThanAttribute(string propertyName)
    {
        PropertyName = propertyName;
    }

    public string PropertyName { get; }

    protected override ValidationResult IsValid(object? value, ValidationContext context)
    {
        object instance = context.ObjectInstance;
        object? otherValue = instance.GetType().GetProperty(PropertyName)?.GetValue(instance);
        if (otherValue is null) return ValidationResult.Success!;
        if (((IComparable)value!).CompareTo(otherValue) <= 0) return ValidationResult.Success!;
        return new($"Value is less than {PropertyName}");
    }
}


public class GreaterThanAttribute : ValidationAttribute
{
    public GreaterThanAttribute(string propertyName)
    {
        PropertyName = propertyName;
    }

    public string PropertyName { get; }

    protected override ValidationResult IsValid(object? value, ValidationContext context)
    {
        object instance = context.ObjectInstance;
        object? otherValue = instance.GetType().GetProperty(PropertyName)?.GetValue(instance);
        if (otherValue is null) return ValidationResult.Success!;
        if (((IComparable)value!).CompareTo(otherValue) >= 0) return ValidationResult.Success!;
        return new($"Value is less than {PropertyName}");
    }
}
