using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Models;
using ProjectApprovalSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ProjectApprovalSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectProposal proposal)
        {
            if (ModelState.IsValid)
            {
                proposal.Status = "Pending";

                var user = await _userManager.GetUserAsync(User);
                proposal.StudentEmail = user?.Email;

                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(proposal);
        }
    }
}