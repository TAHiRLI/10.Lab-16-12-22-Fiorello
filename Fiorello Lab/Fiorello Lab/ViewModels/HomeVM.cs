using Fiorello_Lab.Models;

namespace Fiorello_Lab.ViewModels
{
    public class HomeVM
    {
        public List<Categorie> Categories { get; set; }
        public List<Flower> Flowers { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
