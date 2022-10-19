using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LionCbdShop.Domain.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    
    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
        {
            return ValidationResult.Success;
        }
        
        var extension = Path.GetExtension(file.FileName);
        if (!_extensions.Contains(extension.ToLower()))
        {
            return new ValidationResult(GetErrorMessage(extension));
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage(string extenstion)
    {
        return $"{extenstion} is not allowed file extenstion!";
    }
}