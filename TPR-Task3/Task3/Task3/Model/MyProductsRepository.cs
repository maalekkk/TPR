using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Model
{
    public class MyProductsRepository
    {
        private List<MyProduct> _myProductsList;

        public MyProductsRepository()
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                _myProductsList = new List<MyProduct>();
                List<Product> products = db.GetProducts();
                int i = 0;
                foreach (Product product in products)
                {
                    _myProductsList.Add(new MyProduct(product));
                }
            }
        }

        public MyProductsRepository(int n)
        {
            using (ProductDataContext db = new ProductDataContext())
            {
                _myProductsList = new List<MyProduct>();
                List<Product> products = db.GetNProducts(n);
                int i = 0;
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
            List<MyProduct> withoutNulls = _myProductsList.Where(prod => prod.ProductCategory != null).Select(prod => prod).ToList();
            var result = from myProduct in withoutNulls
                         orderby myProduct.Name descending
                         where myProduct.ProductCategory.Name.Equals(categoryName)
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
