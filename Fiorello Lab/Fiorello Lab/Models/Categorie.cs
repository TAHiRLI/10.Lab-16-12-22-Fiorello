using System.ComponentModel.DataAnnotations;

namespace Fiorello_Lab.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string? Name { get; set; }

    }
}
