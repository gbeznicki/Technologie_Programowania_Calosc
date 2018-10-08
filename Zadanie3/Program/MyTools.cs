using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class MyTools
    {
        private static BazaDanychDataContext dataContext;

        public static BazaDanychDataContext DataContext { get => dataContext; set => dataContext = value; }

        public static List<MyProduct> GetProductByName(List<MyProduct> myProducts, string namePart)
        {
            return myProducts.Where(p => p.Name.Contains(namePart)).ToList();
        }

        public static List<MyProduct> GetProductsByVendorName(List<MyProduct> myProducts, string vendorName)
        {
            var vendorId = dataContext.Vendor
                .Where(v => v.Name.Equals(vendorName))
                .Select(v => v.BusinessEntityID)
                .First();

            var productIdsForVendor = dataContext.ProductVendor
                .Where(pv => pv.BusinessEntityID == vendorId)
                .Select(pv => pv.ProductID).ToList();

            var myProductsForVendor = myProducts.Where(mp => productIdsForVendor.Contains(mp.ProductID)).ToList();

            return myProductsForVendor;
        }

        public static List<MyProduct> GetNRecentlyReviewedProducts(List<MyProduct> myProducts, int howManyProducts)
        {
            var productIds =
                (from p in DataContext.Product
                 join pr in DataContext.ProductReview on p.ProductID equals pr.ProductID
                 orderby pr.ReviewDate descending
                 select p.ProductID).Take(howManyProducts).ToList();

            List<MyProduct> outProducts = new List<MyProduct>();

            foreach (var productId in productIds)
            {
                var myProduct = myProducts.First(mp => mp.ProductID == productId);
                outProducts.Add(myProduct);
            }

            return outProducts;
        }

    }
}
