using Microsoft.AspNetCore.Mvc;

namespace bug_tracker_web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
        private Ticket? nullTicket = null;

        public TicketsController(DataContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            TempData["ticket"] = nullTicket;
            return View();
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                TempData["ticket"] = (Ticket)_context.Tickets.First(x => x.Id == id);
            }
            return PartialView("_Details");
        }

        // GET: Tickets/TicketModel/5
        public async Task<IActionResult> TicketModel()
        {
            return PartialView("_TicketModel", await GetTicketList(1));
        }

        [HttpPost]
        public async Task<IActionResult> TicketModel(int index)
        {
            TempData["index"] = index;
            return PartialView("_TicketModel", await GetTicketList(index));
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'DataContext.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
          return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<TicketView> GetTicketList(int currentPage)
        {
            int maxRowPerPage = 4;
            TicketView ticket = new TicketView();

            ticket.TicketList = await (from t in _context.Tickets select t)
                .OrderBy(x => x.Id)
                .Skip((currentPage - 1) * maxRowPerPage)
                .Take(maxRowPerPage)
                .ToListAsync();

            double pageCount = (double)((decimal)_context.Tickets.Count()/Convert.ToDecimal(maxRowPerPage));

            ticket.pageCount = (int) Math.Ceiling(pageCount);
            ticket.currentPageIndex = currentPage;
            return ticket;
        }
    }
}
