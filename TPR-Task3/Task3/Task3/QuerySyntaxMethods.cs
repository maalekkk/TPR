using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public static class QuerySyntaxMethods
    {
        public static List<Product> GetFirstNProducts()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var output = from product in db.Products
                             select product;

                return output.ToList();
            }

        }

        public static List<ProductVendor> GetFirstNProductVendor(int number)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var output = from vendor in db.ProductVendors
                             select vendor;

                return output.Take(number).ToList();
            }
        }

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
                var result = from productReview in db.ProductReviews
                             orderby productReview.ReviewDate descending
                             group productReview.Product by productReview.ProductID into _group
                             select _group.First();
                return result.Take(howManyProducts).ToList();
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.Products.OrderByDescending(product => product.Name)
                                        .Where(product => product.ProductSubcategory.ProductCategory.Name.Equals(categoryName))
                                        .Select(product => product);
                return result.ToList();
            }
        }

        public static decimal GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.Products.Where(product => product.ProductSubcategory.ProductCategory.Equals(category))
                                        .Select(product => product.StandardCost);
                return result.Sum();
            }
        }
    }
}
