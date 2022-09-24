using Microsoft.AspNetCore.Mvc;
using MVC_Intro.Models;
using System.Text;

namespace MVC_Intro.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Egg",
                Price = 300
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Milk",
                Price = 200
            },
            new ProductViewModel()
            {
                Id = 3,
                Name = "Bread",
                Price = 12
             }
        };
        public IActionResult All(string keyword)
        {
            if (keyword != null) 
            {
                var foundProducts = products
                    .Where(x => x.Name.ToLower()
                        .Contains(keyword.ToLower()));

                return View(foundProducts);
            }
            return View(products);
        }
        public IActionResult AllAsJson() 
        {
            return Json(products);
        }
        public IActionResult AllAsText()
        {
            StringBuilder result = new StringBuilder();
            foreach (var product in products)
            {
                result.Append($"Product {product.Id} : {product.Name} - {product.Price}$\n");
            }
            return Content(result.ToString());
        }
        public IActionResult ById(int id) 
        {
            var product = this.products.FirstOrDefault(x => x.Id == id);
            if (product == null) 
            {
                return BadRequest();
            }
            return View(product);
        }
    }
}
