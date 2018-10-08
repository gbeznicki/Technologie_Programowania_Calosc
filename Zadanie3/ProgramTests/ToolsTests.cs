using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;

namespace ProgramTests
{
    [TestClass()]
    public class ToolsTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Tools.DataContext = new BazaDanychDataContext();
        }
        [TestMethod()]
        public void GetProductByNameTest()
        {
            /* sprawdzamy czy kwerenda
            select p.Name from Production.Product p where p.Name like '%Chain%' 
            i testowana funkcja zwracają identyczne wartości dla string-a "Chain" */
            var products = Tools.GetProductByName("Chain");
            List<string> productNames = products.Select(n => n.Name).ToList();
            List<string> toTestList = new List<string>()
            {
                "Chain", "Chain Stays", "Chainring", "Chainring Bolts", "Chainring Nut"
            };

            Assert.AreEqual(products.Count, toTestList.Count);
            foreach (var it in productNames)
            {
                Assert.IsTrue(toTestList.Contains(it));
            }
        }

        [TestMethod()]
        public void GetProductsByVendorNameTest()
        {
            var productsByVendorName = Tools.GetProductsByVendorName("bicycles");
            List<string> productNames = productsByVendorName.Select(n => n.Name).ToList();
            List<string> toTestList = new List<string>()
            {
                "Front Brakes", "Headset Ball Bearings", "HL Road Seat/Saddle", "LL Mountain Rim",
                "LL Mountain Seat/Saddle", "LL Road Rim", "ML Mountain Rim", "ML Mountain Seat/Saddle",
                "ML Road Rim", "Rear Brakes", "Thin-Jam Hex Nut 1", "Thin-Jam Hex Nut 10",
                "Thin-Jam Hex Nut 11", "Thin-Jam Hex Nut 12", "Thin-Jam Hex Nut 13", "Thin-Jam Hex Nut 14",
                "Thin-Jam Hex Nut 15", "Thin-Jam Hex Nut 16", "Thin-Jam Hex Nut 2", "Thin-Jam Hex Nut 3",
                "Thin-Jam Hex Nut 4", "Thin-Jam Hex Nut 5", "Thin-Jam Hex Nut 6", "Thin-Jam Hex Nut 7",
                "Thin-Jam Hex Nut 8", "Thin-Jam Hex Nut 9"
            };

            Assert.AreEqual(productsByVendorName.Count, toTestList.Count);
            foreach (var it in productNames)
            {
                Assert.IsTrue(toTestList.Contains(it));
            }
        }

        [TestMethod()]
        public void GetProductNamesByVendorNameTest()
        {
            List<Product> productsByVendorName = Tools.GetProductsByVendorName("Cycles");
            List<string> toTestList = productsByVendorName.Select(n => n.Name).ToList();
            List<string> productNamesByVendorName = Tools.GetProductNamesByVendorName("Cycles");

            // sprawdzamy czy listy mają równe długości
            Assert.AreEqual(toTestList.Count, productNamesByVendorName.Count);

            // sprawdzamy czy każdy element z productNamesByVendorName zawiera się w liście toTestList
            foreach (var it in productNamesByVendorName)
            {
                Assert.IsTrue(toTestList.Contains(it));
            }
        }

        [TestMethod()]
        public void GetProductVendorByProductNameTest()
        {
            /*
            select distinct v.Name from Purchasing.Vendor v 
            join Purchasing.ProductVendor pv on v.BusinessEntityID = pv.BusinessEntityID
            join Production.Product p on pv.ProductID = p.ProductID
            where p.Name like '%Crankarm%'
             */
            string productVendor = Tools.GetProductVendorByProductName("Crankarm");
            string toTestString =
                "Proseware, Inc.\n" +
                "Vision Cycles, Inc.\n" +
                "West Junction Cycles\n";

            Assert.AreEqual(productVendor, toTestString);
        }

        /*
       select prod.ProductID
       from Production.Product prod, Production.ProductReview rev
       where prod.ProductID = rev.ProductID
       group by prod.ProductID
       having count(*) = 1
        */
        [TestMethod()]
        public void GetProductsWithNRecentReviewsTest()
        {
            int number = 1;
            var products = Tools.GetProductsWithNRecentReviews(number);

            List<int> expectedProductIds = new List<int>()
            {
                709,
                798
            };
            int expectedCount = 2;

            Assert.AreEqual(expectedCount, products.Count);

            foreach (var product in products)
            {
                expectedProductIds.Contains(product.ProductID);
            }
        }

        /*
        select top 3 prod.ProductID, rev.ReviewDate
        from Production.Product prod, Production.ProductReview rev
        where prod.ProductID = rev.ProductID
        order by rev.ReviewDate desc

            testujemy dla 3 produktów
         */
        [TestMethod()]
        public void GetNRecentlyReviewedProductsTest()
        {
            int number = 3;
            var products = Tools.GetNRecentlyReviewedProducts(number);

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

        /*
        select top 12 p.Name
        from Production.Product p, Production.ProductSubcategory ps, Production.ProductCategory pc
        where p.ProductSubcategoryID = ps.ProductSubcategoryID and ps.ProductCategoryID = pc.ProductCategoryID and pc.Name = 'Bikes'
        order by p.Name asc

            testujemy dla kategorii Bikes i 12 produktów
         */
        [TestMethod()]
        public void GetNProductsFromCategoryTest()
        {
            int number = 12;
            string category = "Bikes";
            var products = Tools.GetNProductsFromCategory(category, number);

            List<int> expectedProductIds = new List<int>()
            {
                775,
                776,
                777,
                778,
                771,
                772,
                773,
                774,
                782,
                783,
                784,
                779,
            };

            Assert.AreEqual(number, products.Count);

            foreach (var product in products)
            {
                Assert.IsTrue(expectedProductIds.Contains(product.ProductID));
            }
        }

        /*
        select sum(p.StandardCost)
        from Production.Product p, Production.ProductSubcategory ps, Production.ProductCategory pc
        where p.ProductSubcategoryID = ps.ProductSubcategoryID and ps.ProductCategoryID = pc.ProductCategoryID and pc.Name = 'Bikes'

            testujemy dla kategorii Bikes
         */
        [TestMethod()]
        public void GetTotalStandardCostByCategoryTest()
        {
            var category = Tools.DataContext.ProductCategory.First(c => c.Name.Equals("Bikes"));
            var cost = Tools.GetTotalStandardCostByCategory(category);

            int expectedCost = 92092;
            Assert.AreEqual(expectedCost, cost);
        }

    }
}