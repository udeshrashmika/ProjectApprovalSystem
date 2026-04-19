using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectApprovalSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectApprovalSystem.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Oversight()
        {

            var proposals = await _context.Proposals.ToListAsync();
            return View(proposals);
        }
    }
}