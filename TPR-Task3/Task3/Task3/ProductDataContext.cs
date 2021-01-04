using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    public partial class ProductDataContext
    {
        public List<Product> GetProducts()
        {
            IQueryable<Product> output = from product in Products
                         select product;

            return output.ToList();

        }

        public List<ProductVendor> GetProductVendors()
        {
            IQueryable<ProductVendor> output = from vendor in ProductVendors
                         select vendor;

            return output.ToList();
        }

        public List<Product> GetNProducts(int n)
        {
            IQueryable<Product> output = from product in Products
                         select product;

            return output.Take(n).ToList();
        }

        public List<ProductVendor> GetNProductVendors(int n)
        {
            IQueryable<ProductVendor> output = from vendor in ProductVendors
                         select vendor;

            return output.Take(n).ToList();
        }

    }
}
