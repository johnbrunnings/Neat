using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Neat.Infrastructure.ApplicationProcessing
{
    public interface IApplicationProcessingRule
    {
        Type AttributeType { get; }
        // TODO: Decouple from Unity
        object ProcessAfter(object output, IList<CustomAttributeNamedArgument> customAttributeNamedArguments);
        void ProcessBefore(IParameterCollection inputs, IList<CustomAttributeNamedArgument> customAttributeNamedArguments);
    }
}