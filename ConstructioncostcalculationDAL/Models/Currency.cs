using System.ComponentModel.DataAnnotations;

namespace ConstructioncostcalculationDAL.Models
{
    public class Currency
    {
        [Key]
        [Display(Name = "رقم العملة")]
        public int CurrencyId { get; set; }

        [MaxLength(100)]
        [Display(Name = "أسم العملة")]
        [Required(ErrorMessage = "أسم العملة مطلوب")]
        public string CurrencyName { get; set; }
    }
}
