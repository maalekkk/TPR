using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public static class QuerySyntaxMethods
    {

        public static List<Product> GetProductsByName(String namePart)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from product in db.Products
                             where product.Name.Contains(namePart)
                             select product;

                return result.ToList();
            }

        }

        public static List<Product> GetProductsByVendorName(String vendorName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from productVendor in db.ProductVendors
                             where productVendor.Vendor.Name.Equals(vendorName)
                             select productVendor.Product;
                return result.ToList();
            }
        }

        public static List<string> GetProtuctNamesByVendorName(String vendorName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from productVendor in db.ProductVendors
                             where productVendor.Vendor.Name.Equals(vendorName)
                             select productVendor.Product.Name;
                return result.ToList();
            }
        }

        public static string GetProductVendorByProductName(string productName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from productVendor in db.ProductVendors
                             where productVendor.Product.Name.Equals(productName)
                             select productVendor.Vendor.Name;
                return result.Single();
            }
        }

        public static List<Product> GetProductsWithRecentReviews(int howManyReviews)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from productReview in db.ProductReviews
                             orderby productReview.ReviewDate descending
                             select productReview.Product;
                return result.Take(howManyReviews).ToList();
            }
        }

        public static List<Product> GetNRecentlyReviewedProductd(int howManyProducts)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from product in db.Products
                             join review in db.ProductReviews on product.ProductID equals review.ProductID
                             orderby review.ReviewDate descending
                             select product;
                return result.Take(howManyProducts).ToList();
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from product in db.Products
                             orderby product.Name descending
                             where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                             select product;
                return result.Take(n).ToList();
            }
        }

        public static decimal GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from product in db.Products
                             where product.ProductSubcategory.ProductCategory.Name.Equals(category.Name)
                             select product.StandardCost;
                return result.Sum();
            }
        }
    }
}
