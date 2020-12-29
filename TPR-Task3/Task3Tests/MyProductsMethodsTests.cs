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
            MyProductsRepository repo = new MyProductsRepository(10);
            List<MyProduct> result = repo.GetMyProductsByName("Chain");
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetNMyProductsFromCategoryTest()
        {
            MyProductsRepository repo = new MyProductsRepository(10);
            List<MyProduct> result = repo.GetNMyProductsFromCategory("Bikes", 2);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetNRecentlyReviewdMyProducts()
        {
            MyProductsRepository repo = new MyProductsRepository(10);
            List<MyProduct> result = repo.GetNRecentlyReviewedProductd(3);
            Assert.AreEqual(0, result.Count);
        }
    }
}
