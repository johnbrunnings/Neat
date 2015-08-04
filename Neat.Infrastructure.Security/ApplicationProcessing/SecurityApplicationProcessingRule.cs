using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security;
using Microsoft.Practices.Unity.InterceptionExtension;
using MongoRepository;
using Neat.Data;
using Neat.Infrastructure.ApplicationProcessing;
using Neat.Infrastructure.Security.Attribute;
using Neat.Infrastructure.Security.Context;

namespace Neat.Infrastructure.Security.ApplicationProcessing
{
    public class SecurityApplicationProcessingRule : IApplicationProcessingRule
    {
        private readonly ISecurityAuthorizationProvider _securityAuthorizationProvider;
        private readonly IGenericRepository _genericRepository;
        private readonly ISecurityContext _securityContext;

        public SecurityApplicationProcessingRule(ISecurityAuthorizationProvider securityAuthorizationProvider, IGenericRepository genericRepository, ISecurityContext securityContext)
        {
            _securityAuthorizationProvider = securityAuthorizationProvider;
            _genericRepository = genericRepository;
            _securityContext = securityContext;
        }

        public Type AttributeType
        {
            get { return typeof (SecuredActionAttribute); }
        }

        // TODO: Break in to Supporting Objects, User, Object, and Field Level Security Processing
        public object ProcessAfter(object output, IList<CustomAttributeNamedArgument> customAttributeNamedArguments)
        {
            var action = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Action").TypedValue.Value as string;
            if (action == null)
            {
                throw new ConfigurationException("No Security Action Specified for Method!");
            }
            if (_securityContext.EnableObjectLevelSecurity)
            {
                if (action == "Read")
                {
                    var parameters = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Parameters").TypedValue.Value as string;
                    if (parameters != null)
                    {
                        throw new ConfigurationException("Incorrect Parameter Setup, Do Not Specify Parameters on Read Security!");
                    }
                    // NOTE: Makes Assumption on Enumerable and Querable Collections only
                    var outputEnumerable = output as IEnumerable;
                    var outputQuerable = output as IQueryable;
                    if (outputEnumerable != null)
                    {
                        var outputType = output.GetType();
                        var outputList = output as IList;
                        if (outputType.IsGenericType)
                        {
                            var outputGenericArgs = new Type[0];
                            var buildType = typeof(List<>);
                            outputGenericArgs = outputType.GetGenericArguments();
                            buildType = buildType.MakeGenericType(outputGenericArgs);
                            var outputGeneric = Activator.CreateInstance(buildType);
                            outputList = outputGeneric as IList;
                        }
                        
                        // TODO: Do a bit more work on making this List for other scenarios
                        foreach (var outputItem in outputEnumerable)
                        {
                            var response = _securityAuthorizationProvider.CheckObjectAuthorization(outputItem, null, action);
                            if (response.IsAuthorized)
                            {
                                outputList.Add(response.SecuredObject);
                            }
                        }
                        if (outputQuerable != null)
                        {
                            output = outputList.AsQueryable();
                        }
                        else
                        {
                            output = outputList;
                        }
                    }
                    else
                    {
                        var response = _securityAuthorizationProvider.CheckObjectAuthorization(output, null, action);
                        if (!response.IsAuthorized)
                        {
                            return null;
                        }
                        output = response.SecuredObject;
                    }
                }
            }

            return output;
        }

        // TODO: Break in to Supporting Objects, User, Object, and Field Level Security Processing
        public void ProcessBefore(IParameterCollection inputs, IList<CustomAttributeNamedArgument> customAttributeNamedArguments)
        {
            var action = customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Action").TypedValue.Value as string;
            if (action == null)
            {
                throw new ConfigurationException("No Security Action Specified for Method!");
            }
            if (_securityContext.EnableUserLevelSecurity)
            {
                var response = _securityAuthorizationProvider.CheckUserAuthorization(action);
                if (!response.IsAuthorized)
                {
                    throw new SecurityException(response.AuthorizationMessage);
                }
            }
            if (_securityContext.EnableObjectLevelSecurity)
            {
                if (action != "Read")
                {
                    var parameters =
                        customAttributeNamedArguments.FirstOrDefault(x => x.MemberName == "Parameters").TypedValue.Value
                            as string;
                    if (parameters == null)
                    {
                        parameters = string.Empty;
                    }
                    var parameterList = parameters.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var parameter in parameterList)
                    {
                        if (!String.IsNullOrWhiteSpace(parameter) && inputs.ContainsParameter(parameter))
                        {
                            var flaggedInput = inputs[parameter];
                            if (action == "Update" || action == "Create")
                            {
                                if (_securityContext.EnableFieldLevelSecurity)
                                {
                                    // NOTE: This implementation Makes a Hard Assumption on Mongo
                                    // TODO: Decouple from Mongo
                                    var flaggedInputType = flaggedInput.GetType();
                                    var originalInput = Activator.CreateInstance(flaggedInputType);
                                    if (action == "Update")
                                    {
                                        var flaggedEntity = flaggedInput as IEntity<string>;
                                        if (flaggedEntity != null)
                                        {
                                            originalInput = _genericRepository.GetById(flaggedInput.GetType(), flaggedEntity.Id);
                                        }
                                        else
                                        {
                                            throw new SecurityException("Cannot Determine Access!");
                                        }
                                    }
                                    var response = _securityAuthorizationProvider.CheckObjectAuthorization(flaggedInput, originalInput, action);
                                    if (!response.IsAuthorized)
                                    {
                                        throw new SecurityException(response.AuthorizationMessage);
                                    }
                                    var flaggedInputPropertyInfoList = flaggedInput.GetType().GetProperties();
                                    var responsePropertyInfoList = response.SecuredObject.GetType().GetProperties();
                                    for (var i = 0; i < flaggedInputPropertyInfoList.Length; i++)
                                    {
                                        flaggedInputPropertyInfoList[i].SetValue(flaggedInput, responsePropertyInfoList[i].GetValue(response.SecuredObject));
                                        // TODO: Make a Deep Set
                                    }
                                }
                                else
                                {
                                    var response = _securityAuthorizationProvider.CheckObjectAuthorization(flaggedInput, null, action);
                                    if (!response.IsAuthorized)
                                    {
                                        throw new SecurityException(response.AuthorizationMessage);
                                    }
                                }
                            }
                            if (action == "Delete")
                            {
                                var response = _securityAuthorizationProvider.CheckObjectAuthorization(flaggedInput, null, action);
                                if (!response.IsAuthorized)
                                {
                                    throw new SecurityException(response.AuthorizationMessage);
                                }
                            }
                        }
                        else
                        {
                            throw new ConfigurationException(string.Format("Incorrect Parameter Specified {0}, Parameter Does Not Exist!", parameter));
                        }
                    }
                }
            }
        }
    }
}