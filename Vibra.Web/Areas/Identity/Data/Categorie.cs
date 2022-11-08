using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Vibra.Web.Areas.Identity.Data
{
    public class Categorie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

    }
}
