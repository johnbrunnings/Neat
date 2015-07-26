using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;
using Neat.Infrastructure.Logging;
using Newtonsoft.Json;

namespace Neat.Infrastructure.Unity.Interceptor
{
    public class LoggingInterceptor : IInterceptionBehavior
    {
        private static bool _shouldLogInfo;
        private static bool _hasReadShouldLog;

        protected static bool ShouldLog
        {
            get
            {
                if (!_hasReadShouldLog)
                {
                    // CHANGE: THIS
                    var logMethods = ConfigurationManager.AppSettings["Logging:LogMethods"];
                    if (bool.TryParse(logMethods, out _shouldLogInfo))
                    {
                        _hasReadShouldLog = true;
                    }
                }
                return _shouldLogInfo;
            }
        }
        
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.Name == "GetType")
            {
                return getNext().Invoke(input, getNext);
            }

            LogInfo(input);

            // AFTER the target method execution
            // Yield to the next module in the pipeline
            var methodReturn = getNext()(input, getNext);

            LogReturn(input, methodReturn);

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

        private void LogInfo(IMethodInvocation input)
        {
            if (!ShouldLog) return;
            var loggerContextInfo = new LoggerContextInfo
            {
                ObjectType = input.Target.GetType(),
                MethodName = input.MethodBase.Name
            };

            // BEFORE the target method execution
            var serializedArguments = JsonConvert.SerializeObject(GetArgs(input));
            var sessionToken = string.Empty;
            if (HttpContext.Current != null)
            {
                // Use Session?
            }
            var logProvider = ServiceLocator.Current.GetInstance<ILogProvider>();
            logProvider.LogInfo(string.Format("Session {0}: Calling {1} with [{2}]", sessionToken, input.MethodBase, serializedArguments), loggerContextInfo);
        }

        private void LogReturn(IMethodInvocation input, IMethodReturn methodReturn)
        {
            var loggerContextInfo = new LoggerContextInfo
            {
                ObjectType = input.Target.GetType(),
                MethodName = input.MethodBase.Name
            };

            var outputs = JsonConvert.SerializeObject(methodReturn.Outputs);
            var serializedArguments = JsonConvert.SerializeObject(GetArgs(input));
            var sessionToken = string.Empty;
            if (HttpContext.Current != null)
            {
                // Use Session?
            }
            // we can make logging faster to not-fire if we move this up to the top 
            if (methodReturn.Exception == null)
            {
                if (!ShouldLog) return;
                var logProvider = ServiceLocator.Current.GetInstance<ILogProvider>();
                logProvider.LogInfo(string.Format("Session {0}: Successfully finished {1} with [{2}] - Returned {3} AND Outputs {4}", sessionToken, input.MethodBase, serializedArguments, methodReturn.ReturnValue, outputs), loggerContextInfo);
            }
            else
            {
                var logMessage = "No Message";
                if (methodReturn.Exception.Data.Contains("HasBeenLogged"))
                {
                    logMessage =
                        string.Format(
                            "Session {0}: Finished {1} with [{2}] - Having Previously Logged Exception {3} with Message {4} AND Outputs {5}",
                            sessionToken, input.MethodBase, serializedArguments, methodReturn.Exception.GetType().Name,
                            methodReturn.Exception.Message, outputs);
                }
                else
                {
                    try
                    {
                        methodReturn.Exception.Data.Add("HasBeenLogged", true);
                    }
                    catch (ArgumentException)
                    {
                        // what are we going to do here?                        
                    } 
                    logMessage =
                        string.Format(
                            "Session {0}: Finished {1} with [{2}] - Having Exception {3} with Message - (StackTrace) {4} - ({5})  AND Outputs {6}",
                            sessionToken, input.MethodBase, serializedArguments, methodReturn.Exception.GetType().Name,
                            methodReturn.Exception.Message, methodReturn.Exception.StackTrace, outputs);

                }
                var logProvider = ServiceLocator.Current.GetInstance<ILogProvider>();
                logProvider.LogError(logMessage, loggerContextInfo);
            }
        }

        private List<object> GetArgs(IMethodInvocation input)
        {
            var parameterCollection = input.Arguments;
            var arguments = new List<object>();
            foreach (var parameter in parameterCollection)
            {
                if (parameter == null)
                {
                    arguments.Add("null");
                }
                else
                {
                    arguments.Add(parameter.ToString());
                }
            }
            return arguments;
        }
    }
}