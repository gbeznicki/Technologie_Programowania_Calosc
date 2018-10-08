using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerTests
{
    [TestClass()]
    public class DataRepositoryTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            DataRepository.DataContext = new DataBaseDataContext();
        }



        [TestMethod()]
        public void GetAllProductReviewsTest()
        {
            var lista = DataRepository.GetAllProductReviews().Select(v => v.ProductReviewID);

            Assert.AreEqual(4, lista.Count());
            Assert.IsTrue(lista.Contains(1));
            Assert.IsTrue(lista.Contains(2));
            Assert.IsTrue(lista.Contains(3));
            Assert.IsTrue(lista.Contains(4));
        }

        [TestMethod()]
        public void CreateProductReviewTest()
        {
            int ilosc1 = DataRepository.GetAllProductReviews().Count();

            ProductReview v = new ProductReview()
            {
                ProductID = 709,
                ReviewerName = "testowy",
                ReviewDate = DateTime.Now,
                EmailAddress = "test@test.pl",
                Rating = 5,
                Comments = "no comment",
                ModifiedDate = new DateTime(2018, 6, 4),
            };

            DataRepository.CreateReview(v);

            int ilosc2 = DataRepository.GetAllProductReviews().Count;

            Assert.AreNotEqual(ilosc1, ilosc2);

            // czyszczenie bazy
            DataRepository.DeleteReviewById(v.ProductReviewID);

        }

        [TestMethod()]
        public void DeleteProductReviewByIdTest()
        {
            ProductReview v = new ProductReview()
            {
                ProductID = 709,
                ReviewerName = "testowy",
                ReviewDate = DateTime.Now,
                EmailAddress = "test@test.pl",
                Rating = 5,
                Comments = "no comment",
                ModifiedDate = new DateTime(2018, 6, 4),
            };

            DataRepository.CreateReview(v);

            int ilosc1 = DataRepository.GetAllProductReviews().Count();

            DataRepository.DeleteReviewById(v.ProductReviewID);

            int ilosc2 = DataRepository.GetAllProductReviews().Count;

            Assert.AreNotEqual(ilosc1, ilosc2);
        }

        [TestMethod()]
        public void UpdateVendorTest()
        {
            ProductReview v = new ProductReview()
            {
                ProductID = 709,
                ReviewerName = "testowy2",
                ReviewDate = DateTime.Now,
                EmailAddress = "test2@test2.pl",
                Rating = 5,
                Comments = "only one comment, no comment",
                ModifiedDate = new DateTime(2018, 6, 4),
            };

            DataRepository.CreateReview(v);

            v.ReviewerName = "noweImie";

            DataRepository.UpdateProductReview(v);

            List<ProductReview> toTestList = DataRepository.GetAllProductReviews();

            Assert.AreEqual(toTestList.Last().ReviewerName, "noweImie");

            // czyszczenie bazy
            DataRepository.DeleteReviewById(v.ProductReviewID);
        }
    }
}