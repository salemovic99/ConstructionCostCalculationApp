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

        public async Task<IActionResult> Index()
        {
            try
            {

                ViewBag.MaterialsCostInYemeny = await unitOfwork.MaterialsRepository.getMaterialCostbyCurrency(1);
                ViewBag.MaterialsCostInSaudi = await unitOfwork.MaterialsRepository.getMaterialCostbyCurrency(2);


                ////
                ///
                ViewBag.BuildingFees = await unitOfwork.WagesRepository.getTotoalCostByCategoryAsync(7);
                ViewBag.BuildingMaterialsLiftingFees = await unitOfwork.WagesRepository.getTotoalCostByCategoryAsync(2);
                ViewBag.ConcreteWorkersFee = await unitOfwork.WagesRepository.getTotoalCostByCategoryAsync(3);
                ViewBag.Electricityinstallationfee = await unitOfwork.WagesRepository.getTotoalCostByCategoryAsync(4);
                ViewBag.Carpenterfee = await unitOfwork.WagesRepository.getTotoalCostByCategoryAsync(5);
                return View();
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
