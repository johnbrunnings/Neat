using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neat.WindowsPhone7.Service.Test
{
    [TestClass]
    public class RunATest
    {
        [TestInitialize]
        public void Initialize()
        {
            var foo = new object();
        }

        [TestMethod]
        public void Test()
        {
            var bar = new object();
            Assert.IsNotNull(bar);
        }
    }
}