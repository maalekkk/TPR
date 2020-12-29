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
                List<Product> products = db.GetProducts();
                List<Product> result = products.GetProductsWithoutCategory_QuerySyntax();
                Assert.AreEqual(209, result.Count);
                foreach(Product res in result)
                {
                    Assert.IsNull(res.ProductSubcategory);
                }
            }
        }

        [TestMethod]
        public void GetProductsWithoutCategory_MethodSyntaxTest()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> products = db.GetProducts();
                List<Product> result = products.GetProductsWithoutCategory_MethodSyntax();
                Assert.AreEqual(209, result.Count);
                foreach (Product res in result)
                {
                    Assert.IsNull(res.ProductSubcategory);
                }
            }
        }

        [TestMethod]
        public void GetPaginatedProducts_QuerySyntax()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> products = db.GetProducts();
                List<Product> page0 = products.GetPaginatedProducts_QuerySyntax(20, 0);
                List<Product> page1 = products.GetPaginatedProducts_QuerySyntax(20, 1);
                Assert.AreEqual(20, page0.Count);
                Assert.AreEqual(20, page1.Count);
                Assert.AreEqual(1, page0[0].ProductID);
                Assert.AreEqual(332, page1[0].ProductID);
            }
        }

        [TestMethod]
        public void GetPaginatedProducts_MethodSyntax()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                List<Product> products = db.GetProducts();
                List<Product> page0 = products.GetPaginatedProducts_MethodSyntax(20, 0);
                List<Product> page1 = products.GetPaginatedProducts_MethodSyntax(20, 1);
                Assert.AreEqual(20, page0.Count);
                Assert.AreEqual(20, page1.Count);
                Assert.AreEqual(1, page0[0].ProductID);
                Assert.AreEqual(332, page1[0].ProductID);
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
