namespace Fiorello_Lab.Models
{
    public class BasketItem
    {
        public  int Id { get; set; }
        public int FlowerId { get; set; }
        public Flower Flower { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Count { get; set; }
    }
}
