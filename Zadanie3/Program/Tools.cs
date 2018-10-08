using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class Tools
    {
        private static BazaDanychDataContext dataContext;

        public static BazaDanychDataContext DataContext { get => dataContext; set => dataContext = value; }

        //tabela producttion.product
        public static List<Product> GetProductByName(string namePart)
        {
            List<Product> outProducts =
                // wyrażenia zapytań

                //(from product in dataContext.Product
                // where product.Name.Contains(namePart)
                // select product).ToList();

                // składnia płynna
                dataContext.Product.Where(n => n.Name.Contains(namePart)).ToList();

            return outProducts;
        }

        //tabele Production.Product, Purchasing.ProductVendor, Purchasing.Vendor
        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            //var vendor = dataContext.Vendor
            //    .Where(v => v.Name.Contains(vendorName))
            //    .Select(n => n.BusinessEntityID)
            //    .ToList();
            //var productVendor = dataContext.ProductVendor
            //    .Where(pv => vendor.Contains(pv.BusinessEntityID));
            //var productIDs = productVendor.Select(n => n.ProductID)
            //    .ToList();
            //List<Product> outProducts = dataContext.Product
            //    .Where(p => productIDs.Contains(p.ProductID))
            //    .OrderBy(p => p.Name)
            //    .ToList();

            List<Product> outProducts =
                (from v in DataContext.Vendor
                 join pv in DataContext.ProductVendor on v.BusinessEntityID equals pv.BusinessEntityID
                 join p in DataContext.Product on pv.ProductID equals p.ProductID
                 where v.Name.Contains(vendorName)
                 select p).ToList();

            return outProducts;

        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            List<Product> productsByVendorName = GetProductsByVendorName(vendorName);
            List<string> productNamesList = productsByVendorName.Select(n => n.Name).ToList();

            return productNamesList;
        }

        public static string GetProductVendorByProductName(string productName)
        {
            string vendorNames = "";
            var vendors =
                (from v in DataContext.Vendor
                 join pv in DataContext.ProductVendor on v.BusinessEntityID equals pv.BusinessEntityID
                 join p in DataContext.Product on pv.ProductID equals p.ProductID
                 where p.Name.Contains(productName)
                 select v.Name).Distinct().ToList();
            foreach (var itVendor in vendors)
            {
                vendorNames += itVendor + "\n";
            }
            return vendorNames;
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyRewievs)
        {
            var productIds = (from p in DataContext.Product
                              join pr in DataContext.ProductReview on p.ProductID equals pr.ProductID
                              group p by p.ProductID into g
                              where g.Count() == howManyRewievs
                              select g.Key).ToList();
            var products = (from p in DataContext.Product
                            where productIds.Contains(p.ProductID)
                            select p).ToList();
            return products;
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            var products = (from p in DataContext.Product
                            join pr in DataContext.ProductReview on p.ProductID equals pr.ProductID
                            orderby pr.ReviewDate descending
                            select p).Take(howManyProducts).ToList();
            return products;
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            var products = (from p in DataContext.Product
                            join ps in DataContext.ProductSubcategory on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                            join pc in DataContext.ProductCategory on ps.ProductCategoryID equals pc.ProductCategoryID
                            orderby p.Name ascending
                            where pc.Name.Equals(categoryName)
                            select p).Take(n).ToList();
            return products;
        }

        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            var query = (from p in DataContext.Product
                         join ps in DataContext.ProductSubcategory on p.ProductSubcategoryID equals ps.ProductSubcategoryID
                         join pc in DataContext.ProductCategory on ps.ProductCategoryID equals pc.ProductCategoryID
                         where pc.Equals(category)
                         select p.StandardCost).Sum();

            return (int)query;
        }
    }
}
