using Microsoft.AspNetCore.Mvc;
using StokTakipProjesi2.Models;
using StokTakipProjesi2.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace StokTakipProjesi2.Controllers
{
    public class AdminController : Controller
    { 
        List<Stock> StockList = StoreController.StockList;

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        public IActionResult Read(int id)
        {
            Stock stock = StockList.FirstOrDefault(s => s.StockID == id);
            if (stock == null)
                return NotFound();
            return View(stock);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            Stock stock = StockList.FirstOrDefault(s => s.StockID == id);
                  stock.UpdatedAt = DateTime.Now; 
            if (stock == null)
                return NotFound();
            return View(stock);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
            stock.UpdatedAt = DateTime.Now; 

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var stock = StockList.FirstOrDefault(s => s.StockID == id);
            if (stock == null)
                return NotFound();

            return View(stock);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
