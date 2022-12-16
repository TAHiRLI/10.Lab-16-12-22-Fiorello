using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello_Lab.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Position { get; set; }   
        public string? Img { get; set; }
        [NotMapped]
        public IFormFile? ExpertFile { get; set; }
    }
}
