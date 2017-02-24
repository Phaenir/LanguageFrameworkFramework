using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFrameworkFramework.Models;
using System.Text;

namespace LanguageFrameworkFramework.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }
        public ViewResult AutoProperty()
        {
            //Create new object Product
            Product myProduct = new Product();
            //Set property some value
            myProduct.Name = "Kayak";
            //Get property value
            string productName = myProduct.Name;
            //Generate View
            return View("Result", (object)String.Format("Product name: {0}", productName));
        }
        public ViewResult CreateProduct()
        {
            //Create new object Product
            Product myProduct = new Product()
            {
                //Set values for properties
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }
        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"apple",10 },{"orange",20},{"plum",30}
            };
            return View("Result", (object)stringArray[1]);
        }
        public ViewResult UseExtension()
        {
            //Create and fill object ShoppingCart
            ShoppingCart cart = new ShoppingCart()
            {
                Products = new List<Product>
                {
                    new Product{Name="Kayak", Price=275M},
                    new Product{Name="Lifejacket",Price=48.95M},
                    new Product{Name="Soccer ball",Price=19.50M},
                    new Product{Name="Corner flag",Price=34.95M}
                }
            };
            //Get total price of products in cart
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }
        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kayak", Price=275M },
                    new Product {Name="Lifejacket", Price=48.95M },
                    new Product {Name="Soccer ball", Price=19.50M },
                    new Product {Name="Corner flag",Price=34.95M }
                }
            };
            //Создать и заполнить массив объектов Product
            Product[] productArray =
            {
                new Product {Name="Kayak", Price=275M },
                    new Product {Name="Lifejacket", Price=48.95M },
                    new Product {Name="Soccer ball", Price=19.50M },
                    new Product {Name="Corner flag",Price=34.95M }
            };
            //Получить общую стоимость товаров в корзине
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();
            
            return View("Result",(object)String.Format("Cart Total: {0},\n\rArray Total: {1}",cartTotal,arrayTotal));
        }
        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kayak", Price=275M, Category="Watersports" },
                    new Product {Name="Lifejacket", Price=48.95M, Category="Watersports" },
                    new Product {Name="Soccer ball", Price=19.50M, Category="Soccer" },
                    new Product {Name="Corner flag",Price=34.95M, Category="Soccer" }
                }
            };
            decimal total = 0;
            foreach(Product prod in products.FilterByCategory("Soccer"))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }
        public ViewResult UseFilterExtensionMethod2()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kayak", Price=275M, Category="Watersports" },
                    new Product {Name="Lifejacket", Price=48.95M, Category="Watersports" },
                    new Product {Name="Soccer ball", Price=19.50M, Category="Soccer" },
                    new Product {Name="Corner flag",Price=34.95M, Category="Soccer" }
                }
            };
            Func<Product, bool> categoryFilter = delegate (Product prod)
                {
                    return prod.Category == "Soccer";
                };
            decimal total = 0;
            foreach (Product prod in products.Filter(categoryFilter))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }
        public ViewResult UseFilterExtensionMethodLambda()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kayak", Price=275M, Category="Watersports" },
                    new Product {Name="Lifejacket", Price=48.95M, Category="Watersports" },
                    new Product {Name="Soccer ball", Price=19.50M, Category="Soccer" },
                    new Product {Name="Corner flag",Price=34.95M, Category="Soccer" }
                }
            };

            decimal total = 0;
            foreach (Product prod in products.Filter(prod=>prod.Category=="Soccer"||prod.Price>20))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }
        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new {Name="MVC", Category="Pattern"},
                new {Name="Hat", Category="Clothing"},
                new {Name="Apple", Category="Fruit"}
            };
            System.Text.StringBuilder result = new StringBuilder();
            foreach(var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }
        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product {Name="Kayak", Price=275M, Category="Watersports" },
                    new Product {Name="Lifejacket", Price=48.95M, Category="Watersports" },
                    new Product {Name="Soccer ball", Price=19.50M, Category="Soccer" },
                    new Product {Name="Corner flag",Price=34.95M, Category="Soccer" }
            };
            //Определить массив для хранения результатов
            Product[] foundProducts = new Product[3];
            //Сортировать содержимое массива
            Array.Sort(products, (item1, item2) => { return Comparer<decimal>.Default.Compare(item1.Price, item2.Price); });
            //Получить три первых элемента массива в качестве результатов
            Array.Copy(products, foundProducts, 3);
            //Создать результат
            StringBuilder result = new StringBuilder();
            foreach(Product p in foundProducts)
            {
                result.AppendFormat("Price: {0}", p.Price);
            }
            return View("Result", (object)result.ToString());
        }
        public ViewResult FindProductsLinq()
        {
            Product[] products =
            {
                new Product {Name="Kayak", Price=275M, Category="Watersports" },
                    new Product {Name="Lifejacket", Price=48.95M, Category="Watersports" },
                    new Product {Name="Soccer ball", Price=19.50M, Category="Soccer" },
                    new Product {Name="Corner flag",Price=34.95M, Category="Soccer" }
            };
            var foundProducts = from match in products orderby match.Price descending select new { match.Name, match.Price };
            //Создать результат
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0}", p.Price);
                if (++count == 3) break;
            }
            return View("Result", (object)result.ToString());
        }
        public ViewResult FindProductsPointLinq()
        {
            Product[] products =
            {
                new Product {Name="Kayak", Price=275M, Category="Watersports" },
                    new Product {Name="Lifejacket", Price=48.95M, Category="Watersports" },
                    new Product {Name="Soccer ball", Price=19.50M, Category="Soccer" },
                    new Product {Name="Corner flag",Price=34.95M, Category="Soccer" }
            };
            var foundProducts = products.OrderBy(e => e.Price).Take(3).Select(e => new { e.Name, e.Price });
            //Создать результат
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0}", p.Price);
            }
            return View("Result", (object)result.ToString());
        }
        public ViewResult ShowLength()
        {
            long? len = MyAsyncMethods.GetPageLength().Result;
            return View("Result", (object)String.Format("page length: {0}", len));
        }
        public async System.Threading.Tasks.Task<ViewResult> ShowLength2()
        {
            long? len = await MyAsyncMethods.GetPageLength2();
            return View("Result", (object)String.Format("page length: {0}", len));
        }
    }
}