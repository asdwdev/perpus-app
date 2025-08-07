using System;
using System.ComponentModel.DataAnnotations;

namespace perpuss.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Faculty { get; set; }

        [Required]
        public string ProgramStudy { get; set; }

        [Required]
        public int EnrollmentYear { get; set; }
    }
}
