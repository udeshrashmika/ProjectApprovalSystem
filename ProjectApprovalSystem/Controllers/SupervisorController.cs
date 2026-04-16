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
    }
}