using System.ComponentModel.DataAnnotations;

namespace ConstructioncostcalculationDAL.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "رقم الفئة")]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        [Display(Name = "أسم الفئة")]
        [Required(ErrorMessage = "أسم الفئة مطلوب")]
        public string CategoryName { get; set; }
    }
}
