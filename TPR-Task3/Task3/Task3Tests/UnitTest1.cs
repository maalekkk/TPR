using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;

namespace Task3Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ProductDataContext productDataContext = new ProductDataContext();
            Assert.AreEqual(504, productDataContext.GetFirstNProducts().Count);
        }
    }
}
