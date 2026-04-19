using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Moq;
using ProjectApprovalSystem.Data;
using ProjectApprovalSystem.Controllers;
using BlindMatchPAS.Models;

namespace BlindMatchPAS.Tests
{
    public class SupervisorTests
    {
        private ApplicationDbContext GetInMemoryDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Test_ConfirmMatch_ChangesStatus()
        {
            
            var db = GetInMemoryDB();

            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var fakeSupervisor = new IdentityUser { Email = "supervisor@test.com", UserName = "supervisor@test.com" };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(fakeSupervisor);

            var controller = new SupervisorController(db, userManagerMock.Object);

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, "supervisor@test.com"),
                new Claim(ClaimTypes.Email, "supervisor@test.com")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = userPrincipal }
            };

            
            var proposal = new ProjectProposal
            {
                Id = 99,
                Title = "Udesh Final Test",
                Status = "Pending",
                Abstract = "Test Abstract",
                TechStack = "C#"
            };
            db.Proposals.Add(proposal);
            await db.SaveChangesAsync();

           
            var result = await controller.ConfirmMatch(99);

           
            var updated = await db.Proposals.FindAsync(99);
            Assert.NotNull(updated);
            Assert.Equal("Matched", updated.Status);
            Assert.Equal("supervisor@test.com", updated.SupervisorEmail);
        }
    }
}