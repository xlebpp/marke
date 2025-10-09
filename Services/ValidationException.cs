using marketplaceE.DTOs;

namespace marketplaceE.Services
{
    public class ValidationException:Exception
    {
        public List<ValidationError> Errors { get; }
        public ValidationException(string message, List<ValidationError> errors) : base(message)
        {Errors = errors; }
       
    }
}
