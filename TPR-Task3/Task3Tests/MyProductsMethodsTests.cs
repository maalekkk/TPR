using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3.Model;

namespace Task3Tests
{
    [TestClass]
    public class MyProductsMethodsTests
    {
        [TestMethod]
        public void GetMyProductsByNameTest()
        {
            MyProductsRepository repo = new MyProductsRepository();
            List<MyProduct> result = repo.GetMyProductsByName("Chain");
            Assert.AreEqual(5, result.Count);
            foreach(MyProduct res in result)
            {
                Assert.IsTrue(res.Name.Contains("Chain"));
            }
        }

        [TestMethod]
        public void GetNMyProductsFromCategoryTest()
        {
            MyProductsRepository repo = new MyProductsRepository();
            List<MyProduct> result = repo.GetNMyProductsFromCategory("Bikes", 2);
            Assert.AreEqual(97, result.Count);
        }

        [TestMethod]
        public void GetNRecentlyReviewdMyProducts()
        {
            MyProductsRepository repo = new MyProductsRepository();
            List<MyProduct> result = repo.GetNRecentlyReviewedProductd(3);
            Assert.AreEqual(3, result.Count);
        }
    }
}
