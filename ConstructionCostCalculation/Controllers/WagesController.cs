using ConstructionCostCalculation.ViewModels;
using ConstructioncostcalculationBLL.Repositories;
using ConstructioncostcalculationDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace ConstructionCostCalculation.Controllers
{
    public class WagesController : Controller
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IToastNotification _toastNotification;
        public WagesController(IUnitOfwork unitOfwork, IToastNotification toastNotification)
        {
            this.unitOfwork = unitOfwork;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            try
            {




                var wages = await unitOfwork.WagesRepository.GetAllAsync();
                var totalCategories = wages.Count();
                wages = wages
                                .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();



                var model = new PaginatedList<Wage>
                {
                    Items = wages,
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


        public async Task<IActionResult> Create()
        {
            try
            {

                var model = new WageCreateFormViewModel()
                {
                    Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() }),
                    Categories = (await unitOfwork.CategoriesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() })
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
        public async Task<IActionResult> Create(WageCreateFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() });
                    model.Categories = (await unitOfwork.CategoriesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() });
                    return View(model);
                }

                var wage = new Wage()
                {
                    WorkerName = model.WorkerName,
                    Description = model.Description,
                    Amount = model.Amount,
                    InvoiceNumber = model.InvoiceNumber,
                    Date = model.Date,
                    CategoryId = model.CategoryId,
                    CurrencyId = model.CurrencyId
                };

                var result = await unitOfwork.WagesRepository.AddAsync(wage);
                if (!result)
                {
                    model.Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() });
                    model.Categories = (await unitOfwork.CategoriesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() });
                    return View(model);
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

            try
            {
                var item = await unitOfwork.WagesRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                var model = new WageEditFormViewModel()
                {
                    WageId = item.WageId,
                    WorkerName = item.WorkerName,
                    Description = item.Description,
                    Amount = item.Amount,
                    InvoiceNumber = item.InvoiceNumber,
                    CategoryId = item.CategoryId,
                    Categories = (await unitOfwork.CategoriesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() }),
                    CurrencyId = item.CurrencyId,
                    Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() })
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
        public async Task<IActionResult> Edit(WageEditFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() });
                    model.Categories = (await unitOfwork.CategoriesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() });
                    return View(model);
                }

                var item = await unitOfwork.WagesRepository.GetByIdAsync(model.WageId);

                item.WorkerName = model.WorkerName;
                item.Description = model.Description;
                item.Amount = model.Amount;
                item.InvoiceNumber = model.InvoiceNumber;
                item.CurrencyId = model.CurrencyId;
                item.CategoryId = model.CategoryId;

                var result = await unitOfwork.WagesRepository.UpdateAsync(item);
                if (!result)
                {
                    model.Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() });
                    model.Categories = (await unitOfwork.CategoriesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() });
                    return View(model);
                }

                await unitOfwork.SaveAsync();
                _toastNotification.AddSuccessToastMessage("تم تعديل بنجاح");
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }




        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0 || int.IsNegative(id))
                    return BadRequest();


                var item = await unitOfwork.WagesRepository.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                var result = await unitOfwork.WagesRepository.DeleteByIdAsync(id);

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
