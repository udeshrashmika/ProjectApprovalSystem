using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Models;
using ProjectApprovalSystem.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace ProjectApprovalSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectProposal proposal)
        {
            if (ModelState.IsValid)
            {
                proposal.Status = "Pending";
                proposal.StudentEmail = User.Identity.Name;

                _context.Proposals.Add(proposal);
                _context.SaveChanges();

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