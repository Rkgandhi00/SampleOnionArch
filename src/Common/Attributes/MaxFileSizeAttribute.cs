using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Common.Attributes
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly int _minFileSize;
        public FileSizeAttribute(int maxFileSize, int minFileSize)
        {
            _maxFileSize = maxFileSize;
            _minFileSize = minFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var fileLength = file.Length;
                if (fileLength > _maxFileSize || fileLength < _minFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize} bytes and minimum file size is {_minFileSize}.";
        }
    }
}
