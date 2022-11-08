using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Vibra.Web.Areas.Identity.Data
{
    public class Customer 
    {
        public int Id { get; set; }

        [Required]
        public string commercialName { get; set; } = string.Empty;
        [Required]
        public string legalName { get; set; } = string.Empty;
        [Required]
        public string Cnpj { get; set; } = string.Empty;

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

    }
}
