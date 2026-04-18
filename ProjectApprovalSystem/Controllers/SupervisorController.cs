using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectApprovalSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectApprovalSystem.Controllers
{
    [Authorize(Roles = "Supervisor")] 
    public class SupervisorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupervisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> BlindDashboard()
        {
            var proposals = await _context.Proposals.ToListAsync();
            return View(proposals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Proposals.FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> ConfirmMatch(int id)
        {
            var project = await _context.Proposals.FindAsync(id);

            if (project != null)
            {
                project.Status = "Matched";


                _context.Update(project);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(BlindDashboard));
        }
    }
}