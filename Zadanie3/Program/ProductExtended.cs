using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public static class ProductExtended
    {
        public static List<Product> PodzielNaStrony(this List<Product> products, int rozmiar, int nrStrony)
        {
            int poczatek = rozmiar * (nrStrony - 1);
            int ileRekordow = products.Count;
            if (ileRekordow < rozmiar * nrStrony)
            {
                if (ileRekordow + rozmiar > rozmiar * nrStrony)
                {
                    int nowyRozmiar = ileRekordow + rozmiar - rozmiar * nrStrony;
                    List<Product> outProducts = products.GetRange(poczatek, nowyRozmiar);
                    return outProducts;
                }
                else
                {
                    throw new Exception("Przekroczono zakres wyników");
                }
            }
            else
            {
                List<Product> outProducts = products.GetRange(poczatek, rozmiar);
                return outProducts;
            }

        }

        public static List<Product> GetProductsWithoutCategory(this List<Product> products)
        {
            var productsWithoutCategory = products.Where(p => p.ProductSubcategoryID is null).ToList();
            return productsWithoutCategory;
        }

        public static string GetProductAndVendorString(this List<Product> products)
        {
            BazaDanychDataContext dataContext = new BazaDanychDataContext();

            string outString = "";

            List<int> productIds = new List<int>();
            foreach (var product in products)
            {
                productIds.Add(product.ProductID);
            }

            var productVendor =
                (from v in dataContext.Vendor
                    join pv in dataContext.ProductVendor on v.BusinessEntityID equals pv.BusinessEntityID
                    join p in dataContext.Product on pv.ProductID equals p.ProductID
                    where productIds.Contains(p.ProductID)
                    select new
                    {
                        productName = p.Name,
                        vendorName = v.Name
                    }).Distinct().ToList();
            foreach (var it in productVendor)
            {
                outString += it.productName + "-" + it.vendorName + "\n";
            }

            return outString;
        }



    }
}
