using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class DataRepository
    {
        private static DataBaseDataContext dataContext = new DataBaseDataContext();

        public static DataBaseDataContext DataContext
        {
            get => dataContext;
            set => dataContext = value;
        }

        public static void CreateReview(ProductReview v)
        {
            dataContext.ProductReview.InsertOnSubmit(v);
            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {

            }
        }

        public static void DeleteReviewById(int id)
        {
            var productReviewToDelete =
                (from review in dataContext.ProductReview
                 where review.ProductReviewID == id
                 select review).First();

            if (productReviewToDelete != null)
            {
                DataContext.ProductReview.DeleteOnSubmit(productReviewToDelete);
            }

            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {

            }
        }

        public static List<ProductReview> GetAllProductReviews()
        {
            List<ProductReview> reviews = (from productReview in dataContext.ProductReview
                                           select productReview).ToList();

            return reviews;
        }

        public static void UpdateProductReview(ProductReview review)
        {
            ProductReview updatedReview = dataContext.ProductReview.Single(r => r.ProductReviewID == review.ProductReviewID);
            updatedReview.ProductID = review.ProductID;
            updatedReview.ReviewerName = review.ReviewerName;
            updatedReview.ReviewDate = review.ReviewDate;
            updatedReview.EmailAddress = review.EmailAddress;
            updatedReview.Rating = review.Rating;
            updatedReview.Comments = review.Comments;
            updatedReview.ModifiedDate = review.ModifiedDate;
            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception e)
            {

            }
        }

        public static List<Product> GetAllProducts()
        {
            List<Product> products = (from product in dataContext.Product
                                      select product).ToList();
            return products;
        }

        public static int GetIdForProductName(string productName)
        {
            var id = (from p in dataContext.Product
                      where p.Name == productName
                      select p.ProductID).First();
            return id;
        }

        public static bool IsProductIdValid(int id)
        {
            int counter = (from p in dataContext.Product
                where p.ProductID == id
                select p).ToList().Count();
            if (counter >= 1) return true;
            else return false;
        }
    }
}
