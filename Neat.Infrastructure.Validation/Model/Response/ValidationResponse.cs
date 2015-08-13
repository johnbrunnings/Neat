using System.Collections.Generic;

namespace Neat.Infrastructure.Validation.Model.Response
{
    public class ValidationResponse
    {
        public ValidationResponse()
        {
            ValidationErrors = new List<string>();
            IsValid = true;
        }

        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; }

        public override string ToString()
        {
            return string.Format("IsValid: {0}, ValidationErrors: {1}", IsValid, ValidationErrors);
        }
    }
}