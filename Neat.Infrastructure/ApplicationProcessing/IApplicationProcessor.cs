using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Neat.Infrastructure.ApplicationProcessing
{
    public interface IApplicationProcessor
    {
        object ProcessAfter(object input, IEnumerable<CustomAttributeData> customAttributeDatas);
        void ProcessBefore(IParameterCollection inputs, IEnumerable<CustomAttributeData> customAttributeDatas);
    }
}