using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public partial class ProductDataContext
    {
        public List<Product> GetProducts()
        {
            var output = from product in Products
                         select product;

            return output.ToList();

        }

        public List<ProductVendor> GetProductVendors()
        {
            var output = from vendor in ProductVendors
                         select vendor;

            return output.ToList();
        }

    }
}
