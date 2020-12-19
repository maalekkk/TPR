using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public static class MethodSyntaxMethods
    {
        public static List<Product> GetProductsByName(String namePart)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.Products.Where(product => product.Name.Contains(namePart))
                                        .Select(product => product);
                return result.ToList();
            }

        }

        public static List<Product> GetProductsByVendorName(String vendorName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.ProductVendors.Where(productVendor => productVendor.Vendor.Name.Equals(vendorName))
                                                .Select(productVendor => productVendor.Product);
                return result.ToList();
            }
        }

        public static List<string> GetProtuctNamesByVendorName(String vendorName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.ProductVendors.Where(productVendor => productVendor.Vendor.Name.Equals(vendorName))
                                                .Select(productVendor => productVendor.Product.Name);
                return result.ToList();
            }
        }

        public static string GetProductVendorByProductName(string productName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.ProductVendors.Where(productVendor => productVendor.Product.Name.Equals(productName))
                                                .Select(productVendor => productVendor.Vendor.Name);
                return result.Single();
            }
        }

        public static List<Product> GetProductsWithRecentReviews(int howManyReviews)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.ProductReviews.OrderByDescending(productReview => productReview.ReviewDate)
                                                .Select(productReview => productReview.Product);
                return result.Take(howManyReviews).ToList();
            }
        }

        public static List<Product> GetNRecentlyReviewedProductd(int howManyProducts)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = db.ProductReviews.OrderByDescending(productReview => productReview.ReviewDate).GroupBy(productReview => productReview.ProductID)

                var result1 = from productReview in db.ProductReviews
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
                var result = from product in db.Products
                             orderby product.Name descending
                             where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                             select product;
                return result.ToList();
            }
        }

        public static decimal GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from product in db.Products
                             where product.ProductSubcategory.ProductCategory.Equals(category)
                             select product.StandardCost;
                return result.Sum();
            }
        }
    }
}
