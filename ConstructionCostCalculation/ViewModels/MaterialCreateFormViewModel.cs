using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ConstructionCostCalculation.ViewModels
{
    public class MaterialCreateFormViewModel
    {



        [Required(ErrorMessage = " أسم المادة مطلوبة")]
        [Display(Name = "أسم المادة")]
        public string MaterialName { get; set; }


        [Required(ErrorMessage = " الوصف مطلوب")]
        [Display(Name = "الوصف")]
        public string Description { get; set; }


        [Required(ErrorMessage = " سعر الوحدة مطلوبة")]
        [Display(Name = "سعر الوحدة")]
        public float UnitPrice { get; set; }


        [Required(ErrorMessage = " الكمية مطلوبة")]
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = " العملة مطلوبة")]
        [Display(Name = "العملة")]
        public int CurrencyId { get; set; }

        public IEnumerable<SelectListItem>? Currencies { get; set; }
    }
}
