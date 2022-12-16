using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello_Lab.Models
{
    public class Flower
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string? Name { get; set; }
        public double Price { get; set; }

        [Required]
        public int? CategorieId { get; set; }

        public Categorie? Categorie { get; set; }
        [NotMapped]
        public IFormFile? MainImgFile { get; set; }

        [NotMapped]
        public List<IFormFile>? FlowerImgFiles { get; set; } = new List<IFormFile>();

        public List<FlowerImage> FlowerImages { get; set; } = new List<FlowerImage>();

        [NotMapped]
        public List<int> FlowerImageIds { get; set; } = new List<int>();

    }
}
