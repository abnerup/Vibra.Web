using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vibra.Web.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expense
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Expenses.Include(e => e.Categorie).Include(e => e.Customer).Include(e => e.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.Categorie)
                .Include(e => e.Customer)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Name");

            var list = new List<SelectListItem>();

            list.Add(new SelectListItem {Text = "",Value=""});

            foreach (var item in _context.Customers)
            {
                list.Add(new SelectListItem { Text = item.CommercialName, Value = item.Id.ToString()});
            }
           
            ViewData["CustomerId"] = list;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Amount,ActualDate,TransactionDate,UserId,CustomerId,CategorieId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var item in _context.Customers)
            {
                list.Add(new SelectListItem { Text = item.CommercialName, Value = item.Id.ToString() });
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Name", expense.CategorieId);
            ViewData["CustomerId"] = new SelectList(list, "Id", "CommercialName", expense.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", expense.UserId);
            return View(expense);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            if (expense.CustomerId.HasValue)
            {
                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "CommercialName", expense.CustomerId);
            }
            else 
            {
                var list = new List<SelectListItem>();

                list.Add(new SelectListItem { Text = "", Value = "" });

                foreach (var item in _context.Customers)
                {
                    list.Add(new SelectListItem { Text = item.CommercialName, Value = item.Id.ToString() });
                }

                ViewData["CustomerId"] = list;
            }
            
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Name", expense.CategorieId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", expense.UserId);
            return View(expense);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Amount,ActualDate,TransactionDate,UserId,CustomerId,CategorieId")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var item in _context.Customers)
            {
                list.Add(new SelectListItem { Text = item.CommercialName, Value = item.Id.ToString() });
            }
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Name", expense.CategorieId);
            ViewData["CustomerId"] = new SelectList(list, "Id", "CommercialName", expense.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", expense.UserId);
            return View(expense);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.Categorie)
                .Include(e => e.Customer)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expenses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Expenses'  is null.");
            }
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
          return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
