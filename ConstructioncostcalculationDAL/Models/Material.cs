using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructioncostcalculationDAL.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        public string MaterialName { get; set; }
        public string Description { get; set; }

        public float UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal Total => (decimal)(UnitPrice * Quantity);

        public DateTime Date { get; set; } = DateTime.Now;


        [ForeignKey(nameof(CurrencyId))]
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }//nav prop
    }
}
