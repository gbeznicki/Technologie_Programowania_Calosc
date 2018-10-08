using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;

namespace ProgramTests
{
    [TestClass()]
    public class ProductExtendedTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Tools.DataContext = new BazaDanychDataContext();
        }

        [TestMethod()]
        public void PodzielNaStronyTest()
        {
            // przypadek 1: ilość wyników na stronie mniejsza od rozmiaru strony
            int rozmiarStrony = 5;
            int nrStrony = 6;
            int poczatekPrzedzialu = (nrStrony - 1) * rozmiarStrony;
            List<Product> productsByVendorName = Tools.GetProductsByVendorName("bicycles");
            int ileRekordow = productsByVendorName.Count;

            List<Product> productsPage6 =
                Tools.GetProductsByVendorName("bicycles")
                    .PodzielNaStrony(rozmiarStrony, nrStrony);

            int expectedSize = 1;
            Assert.AreEqual(productsPage6.Count, expectedSize);

            for (int i = poczatekPrzedzialu; i < poczatekPrzedzialu + expectedSize; i++)
            {
                Assert.IsTrue(productsPage6.Contains(productsByVendorName[i]));
            }

            // przypadek 2: ilość wyników na stronie równa rozmiarowi strony
            nrStrony = 5;
            poczatekPrzedzialu = (nrStrony - 1) * rozmiarStrony;

            List<Product> productsPage5 =
                Tools.GetProductsByVendorName("bicycles")
                    .PodzielNaStrony(rozmiarStrony, nrStrony);

            int expectedSize2 = rozmiarStrony;

            Assert.AreEqual(productsPage5.Count, expectedSize2);

            for (int i = poczatekPrzedzialu; i < poczatekPrzedzialu + expectedSize2; i++)
            {
                Assert.IsTrue(productsPage5.Contains(productsByVendorName[i]));
            }


        }

        [TestMethod()]
        public void GetProductsWithoutCategoryTest()
        {
            var allProducts = (from p in Tools.DataContext.Product
                               select p).ToList();

            var productsWithoutCategory = allProducts.GetProductsWithoutCategory();

            int expectedCount = 209;

            Assert.AreEqual(expectedCount, productsWithoutCategory.Count);
        }

        [TestMethod()]
        public void GetProductAndVendorStringTest()
        {
            var productsByName = Tools.GetProductByName("headset");
            string productVendorString = productsByName.GetProductAndVendorString();
            
            Assert.AreEqual(productVendorString, "Headset Ball Bearings-American Bicycles and Wheels\n");
        }


    }
}