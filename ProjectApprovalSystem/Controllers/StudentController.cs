using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Models;
using ProjectApprovalSystem.Data;
using Microsoft.AspNetCore.Authorization;
<<<<<<< HEAD
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
=======
using System.Linq;
>>>>>>> 6a7daf63367755c886348cc9e0a615ed2d0ec8c9

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
                proposal.StudentEmail = User.Identity.Name;

                var user = await _userManager.GetUserAsync(User);
                proposal.StudentEmail = user?.Email;

                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync();

                return RedirectToAction("MyStatus", "Student");
            }
            return View(proposal);
        }

        [HttpGet]
        public IActionResult MyStatus()
        {
            var userEmail = User.Identity.Name;
            var proposal = _context.Proposals.FirstOrDefault(p => p.StudentEmail == userEmail);

            return View(proposal);
        }
    }
}