using Microsoft.AspNetCore.Identity;

namespace Vibra.Web.Areas.Identity.Data
{
    public class Expense
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime ActualDate { get; set; }

        public DateTime TransactionDate { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public Customer? Customer { get; set; }

        public int? CustomerId { get; set; }

        public Categorie? Categorie { get; set; }

        public int? CategorieId { get; set; }

    }
}
