using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;

namespace Task3Tests
{
    [TestClass]
    public class MethodSyntaxMethodsTests
    {
        [TestMethod]
        public void GetProductByNameTest()
        {
            Assert.AreEqual(5, MethodSyntaxMethods.GetProductsByName("Chain").Count);
        }

        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
            Assert.AreEqual(1, MethodSyntaxMethods.GetProductsByVendorName("International").Count);
        }

        [TestMethod]
        public void GetProductNameByVendorNameTest()
        {
            Assert.AreEqual(1, MethodSyntaxMethods.GetProtuctNamesByVendorName("International").Count);
        }

        [TestMethod]
        public void GetProduvtVendorByProductNameTest()
        {
            Assert.AreEqual("Varsity Sport Co.", MethodSyntaxMethods.GetProductVendorByProductName("Chain"));
        }

        [TestMethod]
        public void GetProductsWithRecentReviewsTest()
        {
            Assert.AreEqual(3, MethodSyntaxMethods.GetProductsWithRecentReviews(3).Count);
        }

        [TestMethod]
        public void GetNRecentlyReviewdProductdTest()
        {
            Assert.AreEqual(3, MethodSyntaxMethods.GetNRecentlyReviewedProductd(3).Count);
        }

        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            Assert.AreEqual(5, MethodSyntaxMethods.GetNProductsFromCategory("Bikes", 5).Count);
        }

        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
            ProductCategory pc = new ProductCategory
            {
                Name = "Bikes"
            };

            Assert.AreEqual(92092.8230m, MethodSyntaxMethods.GetTotalStandardCostByCategory(pc));
        }
    }
}
