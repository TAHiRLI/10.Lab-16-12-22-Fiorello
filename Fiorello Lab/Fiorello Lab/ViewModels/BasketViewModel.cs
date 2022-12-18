using Fiorello_Lab.Models;

namespace Fiorello_Lab.ViewModels
{
    public class BasketViewModel
    {
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public double TotalPrice { get; set; }
    }
}
