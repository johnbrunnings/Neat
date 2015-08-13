using System;
using Neat.Infrastructure.Config;

namespace Neat.Infrastructure.Validation.Context
{
    public class ValidationContext : IValidationContext
    {
        private readonly IConfig _config;

        public ValidationContext(IConfig config)
        {
            _config = config;
        }

        public bool EnableValidation
        {
            get { return Convert.ToBoolean(_config.GetSetting("Validation:EnableValidation")); }
        }
    }
}