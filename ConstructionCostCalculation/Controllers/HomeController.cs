using ConstructionCostCalculation.Models;
using ConstructioncostcalculationBLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConstructionCostCalculation.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUnitOfwork unitOfwork;
        public HomeController(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
