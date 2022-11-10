using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibra.Web.Models
{
    public class Revenue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "datetime2(7)")]
        [Display(Name = "Date of occurrence")]
        public DateTime ActualDate { get; set; }

        [Required]
        [Column(TypeName = "datetime2(7)")]
        [Display(Name = "Competency date")]
        public DateTime TransactionDate { get; set; }

        public ApplicationUser? User { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }

    }
}
