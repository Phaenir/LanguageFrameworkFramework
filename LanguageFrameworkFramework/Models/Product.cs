using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFrameworkFramework.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        private string name;
        public string Name { get {
                return name;
            } set
            {
                name = value;
            }
        }
        //Описание
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Test { get; set; }
    }
}