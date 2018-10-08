using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var dc = new BazaDanychDataContext();
            Tools.DataContext = dc;
            MyTools.DataContext = dc;
            List<MyProduct> myProducts = new List<MyProduct>();

            // wypełnienie listy MyProduct
            foreach (var product in dc.Product.ToList())
            {
                myProducts.Add(new MyProduct(product));
            }

            int number = 5;
            var myProducts1 = MyTools.GetNRecentlyReviewedProducts(myProducts, number);

            foreach (var itProduct in myProducts1)
            {
                Console.WriteLine(itProduct.Name);
            }
        }
    }
}
