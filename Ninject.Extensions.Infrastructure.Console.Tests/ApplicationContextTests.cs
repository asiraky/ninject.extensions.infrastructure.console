using System.Linq;
using NUnit.Framework;

namespace Ninject.Extensions.Infrastructure.Console.Tests
{
    [TestFixture]
    public class ApplicationContextTests
    {
        [Test]
        public void GivenAnArugumentListAContextIsBuilt()
        {
            //arrange
            var args = new[] { "-db", "sales", "-valuelessoption1", "-collection", "invoices" };
            
            //act
            var appContext = new ApplicationContext(args);

            //assert
            Assert.AreEqual(3, appContext.Count());
            Assert.AreEqual("sales", appContext.RawArgs["db"]);
            Assert.AreEqual("", appContext.RawArgs["valuelessoption1"]);
            Assert.AreEqual("invoices", appContext.RawArgs["collection"]);
        }
    }
}
