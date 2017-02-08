using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFrameworkFramework.Models;

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
    }
}