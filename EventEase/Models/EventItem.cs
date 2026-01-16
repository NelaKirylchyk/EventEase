namespace EventEase.Models;

using System.ComponentModel.DataAnnotations;

public sealed class EventItem
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [CustomValidation(typeof(EventItem), nameof(ValidateDate))]
    public DateTime Date { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; } = string.Empty;

    public static ValidationResult? ValidateDate(DateTime date, ValidationContext context)
    {
        if (date == default)
            return new ValidationResult("Date is required.");
        if (date.Date < DateTime.Today)
            return new ValidationResult("Date cannot be in the past.");
        return ValidationResult.Success;
    }
}