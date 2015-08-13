using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Neat.Infrastructure.ApplicationProcessing;
using Neat.Infrastructure.Validation.Attribute;
using Neat.Infrastructure.Validation.Context;
using Neat.Infrastructure.Validation.Model.Response;
using ValidationContext = Neat.Infrastructure.Validation.Context.ValidationContext;

namespace Neat.Infrastructure.Validation.ApplicationProcessing
{
    public class ValidationApplicationProcessingRule : IApplicationProcessingRule
    {
        private readonly IValidationContext _validationContext;

        public ValidationApplicationProcessingRule(IValidationContext validationContext)
        {
            _validationContext = validationContext;
        }

        public Type AttributeType
        {
            get { return typeof(ValidateAttribute); }
        }

        public object ProcessAfter(object output, IList<CustomAttributeNamedArgument> customAttributeNamedArguments)
        {
            return output;
        }

        // TODO: Break in to Supporting Objects, User, Object, and Field Level Security Processing
        public void ProcessBefore(IParameterCollection inputs, IList<CustomAttributeNamedArgument> customAttributeNamedArguments)
        {
            if (_validationContext.EnableValidation)
            {
                var parameters = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Parameters").TypedValue.Value as string;
                if (parameters == null)
                {
                    parameters = string.Empty;
                }
                var parameterList = parameters.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                var validationMessages = new List<string>();
                var isValid = true;
                if (parameterList.Length == 0 && inputs.Count > 0)
                {
                    var flaggedInput = inputs[0];
                    if (flaggedInput != null)
                    {
                        var response = Validate(flaggedInput);
                        isValid = response.IsValid;
                        validationMessages.AddRange(response.ValidationErrors);
                    }
                }
                else
                {
                    foreach (var parameter in parameterList)
                    {
                        if (!String.IsNullOrWhiteSpace(parameter) && inputs.ContainsParameter(parameter))
                        {
                            var flaggedInput = inputs[parameter];
                            if (flaggedInput != null)
                            {
                                var response = Validate(flaggedInput);
                                isValid = isValid && response.IsValid;
                                validationMessages.AddRange(response.ValidationErrors);
                            }
                        }
                        else
                        {
                            throw new ConfigurationErrorsException(string.Format("Incorrect Parameter Specified {0}, Parameter Does Not Exist!", parameter));
                        }
                    }
                }

                if (!isValid)
                {
                    var validationMessageStringBuilder = new StringBuilder();
                    foreach (var validationMessage in validationMessages)
                    {
                        validationMessageStringBuilder.Append(validationMessage);
                        validationMessageStringBuilder.Append(" ");
                    }
                    throw new ValidationException(validationMessageStringBuilder.ToString());
                }
            }
        }

        private ValidationResponse Validate(object objectToValidate)
        {
            var validationResponse = new ValidationResponse();
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(objectToValidate, new System.ComponentModel.DataAnnotations.ValidationContext(objectToValidate), results, true))
            {
                validationResponse.IsValid = false;
                validationResponse.ValidationErrors.AddRange(results.Select(validationResult => validationResult.ErrorMessage));
            }

            return validationResponse;
        }
    }
}