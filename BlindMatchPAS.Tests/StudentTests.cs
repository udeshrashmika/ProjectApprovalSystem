using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjectApprovalSystem.Controllers;
using ProjectApprovalSystem.Data;
using BlindMatchPAS.Models;

namespace BlindMatchPAS.Tests
{
    public class StudentTests
    {
        private ApplicationDbContext GetInMemoryDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void CreateProposal_SetsInitialStatusAndSaves()
        {
            var db = GetInMemoryDB();
            var controller = new StudentController(db);

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, "student@test.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = userPrincipal }
            };

            var newProposal = new ProjectProposal
            {
                Title = "AI Research",
                Abstract = "Testing AI models",
                TechStack = "Python",
                ResearchArea = "Machine Learning"
            };

            var result = controller.Create(newProposal);
            var savedProposal = db.Proposals.FirstOrDefault();

            Assert.NotNull(savedProposal);
            Assert.Equal("Pending", savedProposal.Status);
            Assert.Equal("student@test.com", savedProposal.StudentEmail);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}