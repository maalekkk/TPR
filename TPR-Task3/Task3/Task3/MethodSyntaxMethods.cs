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
                IQueryable<Product> result = db.Products.Where(product => product.Name.Contains(namePart))
                                        .Select(product => product);
                return result.ToList();
            }

        }

        public static List<Product> GetProductsByVendorName(String vendorName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                IQueryable<Product> result = db.ProductVendors.Where(productVendor => productVendor.Vendor.Name.Equals(vendorName))
                                                .Select(productVendor => productVendor.Product);
                return result.ToList();
            }
        }

        public static List<string> GetProtuctNamesByVendorName(String vendorName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                IQueryable<string> result = db.ProductVendors.Where(productVendor => productVendor.Vendor.Name.Equals(vendorName))
                                                .Select(productVendor => productVendor.Product.Name);
                return result.ToList();
            }
        }

        public static string GetProductVendorByProductName(string productName)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                IQueryable<string> result = db.ProductVendors.Where(productVendor => productVendor.Product.Name.Equals(productName))
                                                .Select(productVendor => productVendor.Vendor.Name);
                return result.Single();
            }
        }

        public static List<Product> GetProductsWithRecentReviews(int howManyReviews)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                IQueryable<Product> result = db.ProductReviews.OrderByDescending(productReview => productReview.ReviewDate)
                                                .Select(productReview => productReview.Product);
                return result.Take(howManyReviews).ToList();
            }
        }

        public static List<Product> GetNRecentlyReviewedProductd(int howManyProducts)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                IQueryable<Product> result = db.Products.Join(db.ProductReviews, product => product.ProductID, review => review.ProductID, (product, review) => new { Product = product, Review = review })
                    .OrderByDescending(review => review.Review.ReviewDate)
                    .Select(product => product.Product);

                return result.Take(howManyProducts).ToList();
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                IQueryable<Product> result = db.Products.OrderByDescending(product => product.Name)
                                        .Where(product => product.ProductSubcategory.ProductCategory.Name.Equals(categoryName))
                                        .Select(product => product);
                return result.Take(n).ToList();
            }
        }

        public static decimal GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                IQueryable<decimal> result = db.Products.Where(product => product.ProductSubcategory.ProductCategory.Name.Equals(category.Name))
                                        .Select(product => product.StandardCost);
                return result.Sum();
            }
        }
    }
}
