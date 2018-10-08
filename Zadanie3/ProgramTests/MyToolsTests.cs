using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Tests
{
    [TestClass()]
    public class MyToolsTests
    {
        private List<MyProduct> myProducts;

        [TestInitialize()]
        public void TestInitialize()
        {
            var dc = new BazaDanychDataContext();
            Tools.DataContext = dc;
            MyTools.DataContext = dc;
            myProducts = new List<MyProduct>();

            // wypełnienie listy MyProduct
            foreach (var product in dc.Product.ToList())
            {
                myProducts.Add(new MyProduct(product));
            }
        }

        [TestMethod()]
        public void GetProductByNameTest()
        {
            var result = MyTools.GetProductByName(myProducts, "Flat");

            int expectedCount = 9;
            List<int> expectedIds = new List<int>()
            {
                341,
                343,
                346,
                345,
                348,
                342,
                349,
                347,
                344
            };

            Assert.AreEqual(expectedCount, result.Count);

            foreach (var myProduct in result)
            {
                Assert.IsTrue(expectedIds.Contains(myProduct.ProductID));
            }
        }

        [TestMethod()]
        public void GetProductsByVendorNameTest()
        {
            var result = MyTools.GetProductsByVendorName(myProducts, "Australia Bike Retailer");

            int expectedCount = 16;
            List<int> expectedIds = new List<int>()
            {
                422,
                423,
                424,
                425,
                426,
                427,
                428,
                429,
                430,
                431,
                432,
                433,
                434,
                435,
                436,
                437
            };

            Assert.AreEqual(expectedCount, result.Count);
            foreach (var myProduct in result)
            {
                Assert.IsTrue(expectedIds.Contains(myProduct.ProductID));
            }
        }

        [TestMethod()]
        public void GetNRecentlyReviewedProductsTest()
        {
            int number = 3;
            var products = MyTools.GetNRecentlyReviewedProducts(myProducts, number);


            List<int> expectedProductIds = new List<int>()
            {
                937,
                798
            };

            Assert.AreEqual(number, products.Count);

            foreach (var product in products)
            {
                Assert.IsTrue(expectedProductIds.Contains(product.ProductID));
            }
        }
    }
}