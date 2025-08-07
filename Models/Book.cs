using System.ComponentModel.DataAnnotations;

namespace perpuss.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public int PublicationYear { get; set; }

        public string ISBN { get; set; }

        public int StockQuantity { get; set; }
    }
}
