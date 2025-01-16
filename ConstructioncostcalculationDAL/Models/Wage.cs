using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructioncostcalculationDAL.Models
{
    public class Wage
    {

        [Key]
        public int WageId { get; set; }
        public string WorkerName { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;


        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }//nav prop



        [ForeignKey(nameof(CurrencyId))]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }//nav prop




    }
}
