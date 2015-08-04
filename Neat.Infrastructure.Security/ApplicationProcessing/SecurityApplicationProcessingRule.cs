using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.InterceptionExtension;
using MongoRepository;
using Neat.Data;
using Neat.Infrastructure.ApplicationProcessing;
using Neat.Infrastructure.Security.Attribute;

namespace Neat.Infrastructure.Security.ApplicationProcessing
{
    public class SecurityApplicationProcessingRule : IApplicationProcessingRule
    {
        private readonly ISecurityAuthorizationProvider _securityAuthorizationProvider;
        private readonly IGenericRepository _genericRepository;

        public SecurityApplicationProcessingRule(ISecurityAuthorizationProvider securityAuthorizationProvider, IGenericRepository genericRepository)
        {
            _securityAuthorizationProvider = securityAuthorizationProvider;
            _genericRepository = genericRepository;
        }

        public Type AttributeType
        {
            get { return typeof (SecuredActionAttribute); }
        }

        public object ProcessAfter(object input, IList<CustomAttributeNamedArgument> customAttributeNamedArguments)
        {
            var action = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Action").TypedValue.Value as string;
            var parameters = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Parameters").TypedValue.Value as string;
            if (action != null && action == "Read" && parameters == null)
            {
                var isQueryable = false;
                var inputQuerable = input as IQueryable;
                var inputEnumerable = input as IEnumerable;
                if (inputQuerable != null)
                {
                    isQueryable = true;
                }
                if (inputEnumerable != null)
                {
                    var counter = 0;
                    var inputType = input.GetType();
                    var inputGenericArgs = new Type[0];
                    var buildType = typeof (List<>);
                    if (inputType.IsGenericType)
                    {
                        inputGenericArgs = inputType.GetGenericArguments();
                        buildType = buildType.MakeGenericType(inputGenericArgs);
                    }
                    input = Activator.CreateInstance(buildType);
                    var inputList = input as IList;
                    foreach (var inputItem in inputEnumerable)
                    {
                        var response = _securityAuthorizationProvider.CheckAuthorization(inputItem, null, action);
                        if (response.IsAuthorized)
                        {
                            inputList.Add(response.SecuredObject);
                        }
                        
                        counter++;
                    }
                    if (isQueryable)
                    {
                        input = inputList.AsQueryable();
                    }
                    else
                    {
                        input = inputList;
                    }
                }
                else
                {
                    var response = _securityAuthorizationProvider.CheckAuthorization(input, null, action);
                    if (!response.IsAuthorized)
                    {
                        return null;
                    }
                    input = response.SecuredObject;
                }
            }

            return input;
        }

        public void ProcessBefore(IParameterCollection inputs, IList<CustomAttributeNamedArgument> customAttributeNamedArguments)
        {
            var action = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Action").TypedValue.Value as string;
            var parameters = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Parameters").TypedValue.Value as string;
            if (action != null && action == "Update" && parameters != null)
            {
                var parameterList = parameters.Split(new [] {","}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var parameter in parameterList)
                {
                    if (!String.IsNullOrWhiteSpace(parameter) && inputs.ContainsParameter(parameter))
                    {
                        var flaggedInput = inputs[parameter];
                        var flaggedEntity = flaggedInput as IEntity<string>;
                        if (flaggedEntity != null)
                        {
                            var originalInput = _genericRepository.GetById(flaggedInput.GetType(), flaggedEntity.Id);
                            var response = _securityAuthorizationProvider.CheckAuthorization(flaggedInput, originalInput, action);
                            var flaggedInputPropertyInfoList = flaggedInput.GetType().GetProperties();
                            var responsePropertyInfoList = response.SecuredObject.GetType().GetProperties();
                            for (var i = 0; i < flaggedInputPropertyInfoList.Length; i++)
                            {
                                flaggedInputPropertyInfoList[i].SetValue(flaggedInput, responsePropertyInfoList[i].GetValue(response.SecuredObject));
                            }
                            if (!response.IsAuthorized)
                            {
                                throw new SecurityException(response.AuthorizationMessage);
                            }
                        }
                        else
                        {
                            throw new SecurityException("Cannot Determine Access!");
                        }
                    }
                }
            }
        }
    }
}