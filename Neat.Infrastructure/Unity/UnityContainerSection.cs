using System.IO;
using System.Xml;
using Microsoft.Practices.Unity.Configuration;

namespace Neat.Infrastructure.Unity
{
    public class UnityContainerSection : UnityConfigurationSection
    {
        public void LoadXml(string containerConfigString)
        {
            var xmlTextReader = new XmlTextReader(new StringReader(containerConfigString));
            DeserializeSection(xmlTextReader);
        }
    }
}