using System.ComponentModel.DataAnnotations;

namespace BookStore.Book.Models
{
    public class BookModel
    {
        public string BookName { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Description { get; set; }
        public float Ratings { get; set; }
        public int Reviews { get; set; }
        [Required]
        public float DiscountedPrice { get; set; }
        [Required]
        public float OriginalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
