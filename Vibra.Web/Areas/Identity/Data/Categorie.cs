﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibra.Web.Areas.Identity.Data
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

    }
}
