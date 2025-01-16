using ConstructioncostcalculationBLL.Repositories;
using ConstructioncostcalculationDAL.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ConstructionCostCalculation.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IToastNotification _toastNotification;
        public CategoriesController(IUnitOfwork unitOfwork, IToastNotification toastNotification)
        {
            this.unitOfwork = unitOfwork;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await unitOfwork.CategoriesRepository.GetAllAsync());
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", category);
                }

                var result = await unitOfwork.CategoriesRepository.AddAsync(category);
                if (!result)
                {
                    return View("Index", category);
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
                var item = await unitOfwork.CategoriesRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return NotFound();
                }


                return View(item);
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }


                var item = await unitOfwork.CategoriesRepository.GetByIdAsync(category.CategoryId);

                item.CategoryName = category.CategoryName;


                await unitOfwork.CategoriesRepository.UpdateAsync(item);

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


                var item = await unitOfwork.CategoriesRepository.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                var result = await unitOfwork.CategoriesRepository.DeleteByIdAsync(id);

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
