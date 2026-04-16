using Microsoft.AspNetCore.Mvc;
using BlindMatchPAS.Models;
using ProjectApprovalSystem.Data;
using Microsoft.AspNetCore.Authorization; 

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

                _context.Proposals.Add(proposal);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(proposal);
        }
    }
}