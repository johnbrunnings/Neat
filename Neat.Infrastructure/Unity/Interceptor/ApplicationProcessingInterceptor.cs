using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.InterceptionExtension;
using Neat.Infrastructure.ApplicationProcessing;

namespace Neat.Infrastructure.Unity.Interceptor
{
    public class ApplicationProcessingInterceptor : IInterceptionBehavior
    {
        private readonly IApplicationProcessor _applicationProcessor;

        public ApplicationProcessingInterceptor(IApplicationProcessor applicationProcessor)
        {
            _applicationProcessor = applicationProcessor;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.Name == "GetType")
            {
                return getNext().Invoke(input, getNext);
            }

            _applicationProcessor.ProcessBefore(input.Inputs, input.MethodBase.CustomAttributes);

            // AFTER the target method execution
            // Yield to the next module in the pipeline
            var methodReturn = getNext()(input, getNext);

            methodReturn.ReturnValue = _applicationProcessor.ProcessAfter(methodReturn.ReturnValue,
                input.MethodBase.CustomAttributes);

            return methodReturn;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}