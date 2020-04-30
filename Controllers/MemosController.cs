using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemoPlus.Models;
using Microsoft.AspNetCore.Identity;

namespace MemoPlus.Controllers
{
    public class MemosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MemosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        // GET: Memos

        public async Task<IActionResult> Index()
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Login", "Account");
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return View(await _context.Memos.Where(memo => memo.ApplicationUser == user).ToListAsync());

        }

        // GET: Memos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Login", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var memo = await _context.Memos
                .FirstOrDefaultAsync(m => m.MemoId == id);
            if (memo == null)
            {
                return NotFound();
            }

            return View(memo);

        }

        // GET: Memos/Create
        public IActionResult Create()
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Login", "Account");
            return View();
        }

        // POST: Memos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemoId,MemoTitle,MemoText,CreationDate,IsFixed")] Memo memo)
        {
            if (ModelState.IsValid)
            {
                memo.ApplicationUser = await _userManager.GetUserAsync(User);
                _context.Add(memo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memo);
        }

        // GET: Memos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Login", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var memo = await _context.Memos.FindAsync(id);
            if (memo == null)
            {
                return NotFound();
            }
            return View(memo);
        }

        // POST: Memos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemoId,MemoTitle,MemoText,CreationDate,IsFixed")] Memo memo)
        {
            if (id != memo.MemoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemoExists(memo.MemoId))
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
            return View(memo);
        }

        // GET: Memos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Login", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var memo = await _context.Memos
                .FirstOrDefaultAsync(m => m.MemoId == id);
            if (memo == null)
            {
                return NotFound();
            }

            return View(memo);
        }

        // POST: Memos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memo = await _context.Memos.FindAsync(id);
            _context.Memos.Remove(memo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Memos
        public async Task<IActionResult> Results(string searchText)
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Login", "Account");
            ApplicationUser user = await _userManager.GetUserAsync(User);
            ViewBag.SearchText = searchText;
            return View(await _context.Memos.Where(memo => memo.ApplicationUser == user && memo.MemoText.Contains(searchText)).ToListAsync());
        }

        private bool MemoExists(int id)
        {
            return _context.Memos.Any(e => e.MemoId == id);
        }
    }
}
