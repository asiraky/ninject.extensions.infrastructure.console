using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Extensions.Infrastructure.Console;
using NUnit.Framework;


namespace Ninject.Infrastructure.Conole.Tests
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
