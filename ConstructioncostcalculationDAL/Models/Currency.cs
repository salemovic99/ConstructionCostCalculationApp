using System.ComponentModel.DataAnnotations;

namespace ConstructioncostcalculationDAL.Models
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }

        [MaxLength(100)]
        [Display(Name = "أسم العملة")]
        public string CurrencyName { get; set; }
    }
}
