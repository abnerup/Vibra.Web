using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vibra.Web.Controllers
{
    public class RevenuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _session;

        public string UserId { get { return _session.HttpContext.Session.GetString("UserId"); } }

        public RevenuesController(ApplicationDbContext context, IHttpContextAccessor session)
        {
            _context = context;
            _session = session;
        }

        // GET: Revenues
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Revenues.Where(d=>d.UserId == UserId).Include(r => r.Customer).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Revenues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Revenues == null)
            {
                return NotFound();
            }

            var revenue = await _context.Revenues
                .Include(r => r.Customer)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revenue == null)
            {
                return NotFound();
            }

            return View(revenue);
        }

        // GET: Revenues/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(d => d.UserId == UserId), "Id", "CommercialName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Revenues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Amount,ActualDate,TransactionDate,UserId,CustomerId")] Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revenue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(d => d.UserId == UserId), "Id", "CommercialName", revenue.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", revenue.UserId);
            return View(revenue);
        }

        // GET: Revenues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Revenues == null)
            {
                return NotFound();
            }

            var revenue = await _context.Revenues.FindAsync(id);
            if (revenue == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(d=>d.UserId == UserId), "Id", "CommercialName", revenue.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", revenue.UserId);
            return View(revenue);
        }

        // POST: Revenues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Amount,ActualDate,TransactionDate,UserId,CustomerId")] Revenue revenue)
        {
            if (id != revenue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revenue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevenueExists(revenue.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(d => d.UserId == UserId), "Id", "CommercialName", revenue.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", revenue.UserId);
            return View(revenue);
        }

        // GET: Revenues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Revenues == null)
            {
                return NotFound();
            }

            var revenue = await _context.Revenues
                .Include(r => r.Customer)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revenue == null)
            {
                return NotFound();
            }

            return View(revenue);
        }

        // POST: Revenues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Revenues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Revenues'  is null.");
            }
            var revenue = await _context.Revenues.FindAsync(id);
            if (revenue != null)
            {
                _context.Revenues.Remove(revenue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevenueExists(int id)
        {
          return _context.Revenues.Any(e => e.Id == id);
        }
    }
}
