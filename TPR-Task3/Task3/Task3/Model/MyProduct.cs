using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Model
{
    public class MyProduct
    {
        private int _ProductID;
        private string _Name;
        private decimal _StandardCost;
        private System.Nullable<int> _ProductSubcategoryID;
        private EntitySet<ProductReview> _ProductReviews;
        private EntitySet<ProductVendor> _ProductVendors;
        private ProductSubcategory _ProductSubcategory;
        public MyProduct(Product product)
        {
            ProductID = product.ProductID;
            Name = product.Name;
            StandardCost = product.StandardCost;
            ProductSubcategoryID = product.ProductSubcategoryID;
            ProductVendors = product.ProductVendors;
            ProductReviews = product.ProductReviews;
            ProductSubcategory = product.ProductSubcategory;
        }

        public int ProductID { get => _ProductID; set => _ProductID = value; }
        public string Name { get => _Name; set => _Name = value; }
        public decimal StandardCost { get => _StandardCost; set => _StandardCost = value; }
        public int? ProductSubcategoryID { get => _ProductSubcategoryID; set => _ProductSubcategoryID = value; }
        public EntitySet<ProductReview> ProductReviews { get => _ProductReviews; set => _ProductReviews = value; }
        public EntitySet<ProductVendor> ProductVendors { get => _ProductVendors; set => _ProductVendors = value; }
        public ProductSubcategory ProductSubcategory { get => _ProductSubcategory; set => _ProductSubcategory = value; }
    }
}
