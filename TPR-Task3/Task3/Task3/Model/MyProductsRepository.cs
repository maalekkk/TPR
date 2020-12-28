using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Model
{
    class MyProductsRepository
    {
        private List<MyProduct> _myProductsList;

        public MyProductsRepository()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                _myProductsList = new List<MyProduct>();
                List<Product> products = db.GetProducts();
                foreach (Product product in products)
                {
                    _myProductsList.Add(new MyProduct(product));
                }
            }
        }

        public List<MyProduct> GetMyProductsByName(string namePart)
        {
            var result = from myProduct in _myProductsList
                         where myProduct.Name.Contains(namePart)
                         select myProduct;
            return result.ToList();
        }

        public List<MyProduct> GetNMyProductsFromCategory(string categoryName, int n)
        {
            var result = from myProduct in _myProductsList
                         orderby myProduct.Name descending
                         where myProduct.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                         select myProduct;
            return result.ToList();
        }

        public List<MyProduct> GetNRecentlyReviewedProductd(int howManyProducts)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                var result = from myProduct in _myProductsList
                             join review in db.ProductReviews on myProduct.ProductID equals review.ProductID
                             orderby review.ReviewDate descending
                             select myProduct;
                return result.Take(howManyProducts).ToList();
            }
        }
    }
}
