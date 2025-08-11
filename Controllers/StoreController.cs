using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakipProjesi2.Models;

namespace StokTakipProjesi2.Controllers
{
    public class StoreController : Controller
    {
        public static List<Stock> StockList =  new List<Stock>
        {
            new Stock { StockID = 1, Name = "Product A", Description = "Description A", Category = "Category 1", StockCount = 10, Price = 100.00m, Owner = "store1"},
            new Stock { StockID = 2, Name = "Product B", Description = "Description B", Category = "Category 2", StockCount = 20, Price = 200.00m, Owner = "store1" },
            new Stock { StockID = 3, Name = "Product C", Description = "Description C", Category = "Category 3", StockCount = 30, Price = 300.00m, Owner = "store1" },
            new Stock { StockID = 4, Name = "Product D", Description = "Description D", Category = "Category 4", StockCount = 50, Price = 500.00m, Owner = "store2" }
        };
        public IActionResult Dashboard()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Store Dashboard accessed by: " + User.Identity.Name);
                return View(StockList);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Stock stock) 
        {
            var lastId = StockList.Count > 0 ? StockList.Max(s => s.StockID) : 0;
            lastId++;
            stock.StockID = lastId;
            StockList.Add(stock);
            Console.WriteLine("New stock added: " + stock.Owner);
            return RedirectToAction("Dashboard");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Read()
        {
           return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Stock stock = StockList.FirstOrDefault(s => s.StockID == id);
            if (stock == null)
                return NotFound();
            return View(stock);
        }

        [HttpPost]
        public IActionResult Update(Stock updatedStock)
        {
            var stock = StockList.FirstOrDefault(s => s.StockID == updatedStock.StockID);
            if (stock == null)
                return NotFound();

            stock.Name = updatedStock.Name;
            stock.Category = updatedStock.Category;
            stock.Description = updatedStock.Description;
            stock.StockCount = updatedStock.StockCount;
            stock.Price = updatedStock.Price;

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var stock = StockList.FirstOrDefault(s => s.StockID == id);
            if (stock == null)
                return NotFound();

            return View(stock);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var stock = StockList.FirstOrDefault(s => s.StockID == id);
            if (stock == null)
                return NotFound();

            StockList.Remove(stock);
            return RedirectToAction("Dashboard");
        }
    }
}
