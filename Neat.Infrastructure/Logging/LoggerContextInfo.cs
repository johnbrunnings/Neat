using System;

namespace Neat.Infrastructure.Logging
{
    public class LoggerContextInfo
    {
        public Type ObjectType { get; set; }
        public string MethodName { get; set; }

        public override string ToString()
        {
            return string.Format("ObjectType: {0}, MethodName: {1}", ObjectType, MethodName);
        }
    }
}