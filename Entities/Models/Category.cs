using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    public class Category
    {
        [Column("CategoryId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Name is a required field")]
        [MaxLength(20,ErrorMessage ="Maximum length for a category is 20 characters.")]
        public string? Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
