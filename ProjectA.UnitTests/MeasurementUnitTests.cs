using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectA.Core.Features.Measurements;
using ProjectA.Core;
using System.Linq;

namespace ProjectA.UnitTests
{
    [TestClass]
    public class MeasurementUnitTests : BaseTest
    {
        [TestMethod]
        public void SaveUnit()
        {
            var item = new MeasurementUnit
            {
                Name = "Unit1",
                Unit = "F"
            };

            item.Save();

            var dbItem = MeasurementUnit.Query(x => x.Name == "Unit1").Single();
            Assert.AreEqual(item.Name, dbItem.Name);
        }
    }
}
