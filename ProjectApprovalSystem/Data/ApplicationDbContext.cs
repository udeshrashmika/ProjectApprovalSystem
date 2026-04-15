using BlindMatchPAS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectApprovalSystem.Models; 

namespace ProjectApprovalSystem.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<ProjectProposal> Proposals { get; set; }
    }
}