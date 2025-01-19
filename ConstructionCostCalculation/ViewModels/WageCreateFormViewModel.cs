using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ConstructionCostCalculation.ViewModels
{
    public class WageCreateFormViewModel
    {


        [Display(Name = "أسم العامل")]
        [Required(ErrorMessage = "أسم العامل مطلوب")]
        public string WorkerName { get; set; }

        [Display(Name = " الوصف")]
        [Required(ErrorMessage = " الوصف مطلوب")]
        public string Description { get; set; }


        [Display(Name = "المبلغ")]
        [Required(ErrorMessage = "المبلغ مطلوب")]
        public float Amount { get; set; }


        [Display(Name = "رقم الفاتورة")]
        [Required(ErrorMessage = "رقم الفاتورة مطلوب")]
        public int InvoiceNumber { get; set; }

        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; } = DateTime.Now;


        [Display(Name = "الفئة")]
        [Required(ErrorMessage = "الفئة مطلوبة")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }




        [Display(Name = "العملة")]
        [Required(ErrorMessage = "العملة مطلوبة")]
        public int CurrencyId { get; set; }
        public IEnumerable<SelectListItem>? Currencies { get; set; }


    }
}
