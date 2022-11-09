using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibra.Web.Areas.Identity.Data
{
    public class Customer 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Comercial name")]
        public string CommercialName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Legal name")]
        public string LegalName { get; set; } = string.Empty;


        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Cnpj { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

    }
}
