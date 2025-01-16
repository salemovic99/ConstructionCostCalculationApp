using System.ComponentModel.DataAnnotations;

namespace ConstructioncostcalculationDAL.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(100)]
        [Display(Name = "أسم الفئة")]
        public string CategoryName { get; set; }
    }
}
