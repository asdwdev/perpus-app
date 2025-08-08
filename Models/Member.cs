using System;
using System.ComponentModel.DataAnnotations;

namespace perpuss.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama mahasiswa wajib diisi")]
        public string Name { get; set; }

        [Required(ErrorMessage = "NIM wajib diisi")]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fakultas wajib diisi")]
        public string Faculty { get; set; }

        [Required(ErrorMessage = "Program studi wajib diisi")]
        public string ProgramStudy { get; set; }

        [Required(ErrorMessage = "Angkatan wajib diisi")]

        public int? EnrollmentYear { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
