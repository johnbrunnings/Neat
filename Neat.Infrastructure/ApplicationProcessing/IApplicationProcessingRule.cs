using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Neat.Infrastructure.ApplicationProcessing
{
    public interface IApplicationProcessingRule
    {
        Type AttributeType { get; }
        object ProcessAfter(object input, IList<CustomAttributeNamedArgument> customAttributeNamedArguments);
        void ProcessBefore(IParameterCollection inputs, IList<CustomAttributeNamedArgument> customAttributeNamedArguments);
    }
}