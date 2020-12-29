using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Task3;

namespace Task3Tests
{
    [TestClass]
    public class QuerySyntaxMethodsTests
    {
        [TestMethod]
        public void GetProductByNameTest()
        {
            List<Product> result = QuerySyntaxMethods.GetProductsByName("Chain");
            Assert.AreEqual(5, result.Count);
            foreach(Product res in result)
            {
                Assert.IsTrue(res.Name.Contains("Chain"));
            }
        }

        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
            List<Product> result = QuerySyntaxMethods.GetProductsByVendorName("International");
            Assert.AreEqual(1,result.Count);
        }

        [TestMethod]
        public void GetProductNameByVendorNameTest()
        {
           List<string> result = QuerySyntaxMethods.GetProtuctNamesByVendorName("International");
           Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetProduvtVendorByProductNameTest()
        {
            string result = QuerySyntaxMethods.GetProductVendorByProductName("Chain");
            Assert.AreEqual("Varsity Sport Co.", result);
        }

        [TestMethod]
        public void GetProductsWithRecentReviewsTest()
        {
            List<Product> result = QuerySyntaxMethods.GetProductsWithRecentReviews(3);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetNRecentlyReviewdProductdTest()
        {
            List<Product> result = QuerySyntaxMethods.GetNRecentlyReviewedProductd(3);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            List<Product> result = QuerySyntaxMethods.GetNProductsFromCategory("Bikes", 5);
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
            ProductCategory pc = new ProductCategory
            {
                Name = "Bikes"
            };

            Assert.AreEqual(92092.8230m, QuerySyntaxMethods.GetTotalStandardCostByCategory(pc));
        }
    }
}
