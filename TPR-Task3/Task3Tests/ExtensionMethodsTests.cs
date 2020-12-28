using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;
using System.Collections.Generic;
namespace Task3Tests
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void GetProductsWithoutCategory_QuerySyntaxTest()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> result = db.GetProducts();
                Assert.AreEqual(209, result.GetProductsWithoutCategory_QuerySyntax().Count);
            }
        }

        [TestMethod]
        public void GetProductsWithoutCategory_MethodSyntaxTest()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> result = db.GetProducts();
                Assert.AreEqual(209, result.GetProductsWithoutCategory_MethodSyntax().Count);
            }
        }

        [TestMethod]
        public void GetPaginatedProducts_QuerySyntax()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> result = db.GetProducts();
                Assert.AreEqual(20, result.GetPaginatedProducts_QuerySyntax(20, 1).Count);
                Assert.AreEqual(332, result.GetPaginatedProducts_QuerySyntax(20, 1)[0].ProductID);
            }
        }

        [TestMethod]
        public void GetPaginatedProducts_MethodSyntax()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> result = db.GetProducts();
                Assert.AreEqual(20, result.GetPaginatedProducts_MethodSyntax(20, 1).Count);
                Assert.AreEqual(332, result.GetPaginatedProducts_MethodSyntax(20, 1)[0].ProductID);
            }
        }

        [TestMethod]
        public void GetProductVendorString_QuerySyntaxTest()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> products = db.GetProducts();
                List<ProductVendor> productVendors = db.GetProductVendors();
                string result = products.GetProductVendorString_QuerySyntax(productVendors);
                string line = result.Substring(0, result.IndexOf('\n') - 1);
                Assert.AreEqual("Adjustable Race-Litware, Inc.", line);
            }
        }

        [TestMethod]
        public void GetProductVendorString_MethodSyntaxTest()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> products = db.GetProducts();
                List<ProductVendor> productVendors = db.GetProductVendors();
                string result = products.GetProductVendorString_MethodSyntax(productVendors);
                string line = result.Substring(0, result.IndexOf('\n') - 1);
                Assert.AreEqual("Adjustable Race-Litware, Inc.", line);
            }
        }
    }
}
