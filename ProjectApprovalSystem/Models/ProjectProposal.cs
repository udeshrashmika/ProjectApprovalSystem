using System.ComponentModel.DataAnnotations;

namespace BlindMatchPAS.Models
{
    public class ProjectProposal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Abstract { get; set; }

        [Required]
        public string TechStack { get; set; }

        [Required]
        public string ResearchArea { get; set; }

        public string Status { get; set; } = "Pending";

        public string? StudentId { get; set; }
        public string? StudentEmail { get; set; }

        public string? SupervisorId { get; set; }
        public string? SupervisorName { get; set; }
        public string? SupervisorEmail { get; set; }
    }
}