using System.ComponentModel.DataAnnotations;

public class ValidatePdfAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var file = value as IFormFile;
        if (file == null)
        {
            // return new ValidationResult("file can't be null");
            return ValidationResult.Success;
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == ".pdf")
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("only pdf files are allowed");

    }
}

public class ValidateImageAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var file = value as IFormFile;
        if (file == null)
        {
            // return new ValidationResult("file can't be null");
            return ValidationResult.Success;
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("jpg or jpeg or png files are allowed");
    }
}

public class ValidateVideoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var file = value as IFormFile;
        if (file == null)
        {
            return new ValidationResult("file can't be null");
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (extension == ".mp4")
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("only mp4 files are allowed");
    }
}