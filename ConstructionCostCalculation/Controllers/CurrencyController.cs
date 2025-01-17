using ConstructionCostCalculation.ViewModels;
using ConstructioncostcalculationBLL.Repositories;
using ConstructioncostcalculationDAL.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ConstructionCostCalculation.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IToastNotification _toastNotification;
        public CurrencyController(IUnitOfwork unitOfwork, IToastNotification toastNotification)
        {
            this.unitOfwork = unitOfwork;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            try
            {
                var currencies = await unitOfwork.CurrenciesRepository.GetAllAsync();
                var totalCategories = currencies.Count();
                currencies = currencies
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();



                var model = new PaginatedList<Currency>
                {
                    Items = currencies,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalCategories
                };

                return View(model);
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Currency currency)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", currency);
                }

                var result = await unitOfwork.CurrenciesRepository.AddAsync(currency);
                if (!result)
                {
                    return View("Index", currency);
                }

                await unitOfwork.SaveAsync();

                _toastNotification.AddSuccessToastMessage("تم الاضافة بنجاح");
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                return Problem(e.Message);

            }
        }

        public async Task<IActionResult> Edit(int id)
        {

            return View(await unitOfwork.CurrenciesRepository.GetByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return View(currency);
            }


            var item = await unitOfwork.CurrenciesRepository.GetByIdAsync(currency.CurrencyId);

            item.CurrencyName = currency.CurrencyName;


            await unitOfwork.CurrenciesRepository.UpdateAsync(item);

            await unitOfwork.SaveAsync();
            _toastNotification.AddSuccessToastMessage("تم تعديل بنجاح");
            return RedirectToAction(nameof(Index));
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0 || int.IsNegative(id))
                    return BadRequest();


                var item = await unitOfwork.CurrenciesRepository.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                var result = await unitOfwork.CurrenciesRepository.DeleteByIdAsync(id);

                if (!result)
                {
                    return BadRequest();
                }
                await unitOfwork.SaveAsync();
                return Ok();
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }

        }
    }
}
