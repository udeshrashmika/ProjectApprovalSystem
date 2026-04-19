using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using ProjectApprovalSystem.Controllers;
using ProjectApprovalSystem.Data;
using BlindMatchPAS.Models;

namespace BlindMatchPAS.Tests
{
    public class AdminTests
    {
        private ApplicationDbContext GetInMemoryDB()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Oversight_ReturnsAllProposals()
        {
            var db = GetInMemoryDB();
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var controller = new AdminController(db, userManagerMock.Object);

            db.Proposals.Add(new ProjectProposal { Id = 1, Title = "P1", Status = "Pending", Abstract = "A", TechStack = "T", ResearchArea = "R" });
            db.Proposals.Add(new ProjectProposal { Id = 2, Title = "P2", Status = "Matched", Abstract = "B", TechStack = "T", ResearchArea = "R" });
            await db.SaveChangesAsync();

            var result = await controller.Oversight();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.Model as IEnumerable<ProjectProposal>;

            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }
    }
}