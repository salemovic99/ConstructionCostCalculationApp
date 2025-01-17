using ConstructionCostCalculation.ViewModels;
using ConstructioncostcalculationBLL.Repositories;
using ConstructioncostcalculationDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace ConstructionCostCalculation.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IToastNotification _toastNotification;
        public MaterialsController(IUnitOfwork unitOfwork, IToastNotification toastNotification)
        {
            this.unitOfwork = unitOfwork;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            try
            {
                var materials = await unitOfwork.MaterialsRepository.GetAllAsync();
                var totalCategories = materials.Count();
                materials = materials
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();



                var model = new PaginatedList<Material>
                {
                    Items = materials,
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
                var currencies = await unitOfwork.CurrenciesRepository.GetAllAsync();
                var model = new MaterialCreateFormViewModel()
                {
                    Currencies = currencies.Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() })
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
        public async Task<IActionResult> Create(MaterialCreateFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var currencies = await unitOfwork.CurrenciesRepository.GetAllAsync();

                    model.Currencies = currencies.Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() }); return View(model);
                }

                var material = new Material()
                {
                    MaterialName = model.MaterialName,
                    Description = model.Description,
                    UnitPrice = model.UnitPrice,
                    Quantity = model.Quantity,
                    CurrencyId = model.CurrencyId
                };

                var result = await unitOfwork.MaterialsRepository.AddAsync(material);
                if (!result)
                {
                    var currencies = await unitOfwork.CurrenciesRepository.GetAllAsync();

                    model.Currencies = currencies.Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() });

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
                var item = await unitOfwork.MaterialsRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                var model = new MaterialEditFormViewModel()
                {
                    MaterialId = item.MaterialId,
                    MaterialName = item.MaterialName,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
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
        public async Task<IActionResult> Edit(MaterialEditFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() });

                    return View(model);
                }

                var item = await unitOfwork.MaterialsRepository.GetByIdAsync(model.MaterialId);

                item.MaterialName = model.MaterialName;
                item.Description = model.Description;
                item.Quantity = model.Quantity;
                item.UnitPrice = model.UnitPrice;
                item.CurrencyId = model.CurrencyId;

                var result = await unitOfwork.MaterialsRepository.UpdateAsync(item);
                if (!result)
                {
                    model.Currencies = (await unitOfwork.CurrenciesRepository.GetAllAsync()).Select(c => new SelectListItem() { Text = c.CurrencyName, Value = c.CurrencyId.ToString() });
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


                var item = await unitOfwork.MaterialsRepository.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                var result = await unitOfwork.MaterialsRepository.DeleteByIdAsync(id);

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
