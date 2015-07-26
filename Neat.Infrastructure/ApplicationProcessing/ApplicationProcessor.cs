using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Neat.Infrastructure.ApplicationProcessing
{
    public class ApplicationProcessor : IApplicationProcessor
    {
        private readonly IEnumerable<IApplicationProcessingRule> _applicationProcessingRules;
        private bool _rulesNotLoaded = true;
        private readonly object _lock = new object();
        private Dictionary<Type, IApplicationProcessingRule> _strategyHash;

        public ApplicationProcessor(IEnumerable<IApplicationProcessingRule> applicationProcessingRules)
        {
            _applicationProcessingRules = applicationProcessingRules;
        }

        public object ProcessAfter(object input, IEnumerable<CustomAttributeData> customAttributeDatas)
        {
            InitializeDictionary();
            var returnValue = input;
            foreach (var customAttributeData in customAttributeDatas)
            {
                if (_strategyHash.ContainsKey(customAttributeData.AttributeType))
                {
                    returnValue = _strategyHash[customAttributeData.AttributeType].ProcessAfter(returnValue, customAttributeData.NamedArguments);
                }
            }
            return returnValue;
        }

        public void ProcessBefore(IParameterCollection inputs, IEnumerable<CustomAttributeData> customAttributeDatas)
        {
            InitializeDictionary();
            foreach (var customAttributeData in customAttributeDatas)
            {
                if (_strategyHash.ContainsKey(customAttributeData.AttributeType))
                {
                    _strategyHash[customAttributeData.AttributeType].ProcessBefore(inputs, customAttributeData.NamedArguments);
                }
            }
        }

        private void InitializeDictionary()
        {
            if (_rulesNotLoaded)
            {
                lock (_lock)
                {
                    if (_rulesNotLoaded)
                    {
                        var workingHash = _applicationProcessingRules.ToDictionary(rule => rule.AttributeType);
                        Thread.MemoryBarrier();
                        _strategyHash = workingHash;
                        _rulesNotLoaded = false;
                    }
                }
            }
        }
    }
}