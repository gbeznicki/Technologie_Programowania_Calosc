using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer;

namespace Zadanie4.Model
{
    public class DataLayer
    {
        public IEnumerable<ProductReview> ProductReviews
        {
            get
            {
                List<ProductReview> reviews = DataRepository.GetAllProductReviews();
                return reviews;
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                List<Product> products = DataRepository.GetAllProducts();
                return products;
            }
        }
    }
}
