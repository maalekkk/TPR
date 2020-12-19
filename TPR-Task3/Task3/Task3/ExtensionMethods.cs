using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public static class ExtensionMethods
    {
        public static List<Product> GetProductsWithoutCategory_QuerySyntax(this List<Product> products)
        {
            var result = from product in products
                         where product.ProductSubcategoryID == null
                         select product;
            return result.ToList();
        }

        public static List<Product> GetProductsWithoutCategory_MethodSyntax(this List<Product> products)
        {
            var result = products.Where(product => product.ProductSubcategoryID == null)
                                    .Select(product => product);
            return result.ToList();
        }

        public static List<Product> GetPaginatedProducts_QuerySyntax(this List<Product> products, int size ,int page)
        {
            var result = from product in products
                         select product;
            return result.Skip(size * page).Take(size).ToList();
        }

        public static List<Product> GetPaginatedProducts_MethodSyntax(this List<Product> products, int size, int page)
        {
            var result = products.Select(product => product);
            return result.Skip(size * page).Take(size).ToList();
        }

        public static string GetProductVendorString_QuerySyntax(this List<Product> products, List<ProductVendor> productVendors)
        {
            var result = from product in products
                         from productVendor in productVendors
                         where productVendor.ProductID == product.ProductID
                         select new { Product = product.Name, Vendor = productVendor.Vendor.Name };
            StringBuilder resultString = new StringBuilder();
            foreach (var productVendor in result)
            {
                resultString.Append(productVendor.Product).Append("-").Append(productVendor.Vendor).AppendLine();
            }
            return resultString.ToString();
        }

        public static string GetProductVendorString_MethodSyntax(this List<Product> products, List<ProductVendor> productVendors)
        {
            var result = products.Join(productVendors, product => product.ProductID, productVendor => productVendor.ProductID, (product, productVendor)
                                     => new { Product = product.Name, Vendor = productVendor.Vendor.Name });
            StringBuilder resultString = new StringBuilder();
            foreach (var productVendor in result)
            {
                resultString.Append(productVendor.Product).Append("-").Append(productVendor.Vendor).AppendLine();
            }
            return resultString.ToString();
        }
    }
}
