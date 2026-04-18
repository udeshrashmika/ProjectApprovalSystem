using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectApprovalSystem.Data;
using System.Linq;

namespace ProjectApprovalSystem.Controllers
{


    [Authorize]
    public class SupervisorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupervisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult BlindDashboard()
        {

            var proposals = _context.Proposals.ToList();
            return View(proposals);
        }
        [HttpPost]
        public IActionResult ConfirmMatch(int projectId)
        {

            var project = _context.Proposals.Find(projectId);

            if (project != null)
            {

                project.Status = "Matched";


                _context.SaveChanges();
            }


            return RedirectToAction("BlindDashboard");
        }
    }
}