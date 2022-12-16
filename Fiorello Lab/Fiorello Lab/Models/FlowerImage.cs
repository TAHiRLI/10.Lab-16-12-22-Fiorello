namespace Fiorello_Lab.Models
{
	public class FlowerImage
	{
		public int Id { get; set; }
		public int FlowerId { get; set; }
		public bool? IsMain { get; set; }
		public string? ImgUrl { get; set; }
		public Flower Flower { get; set; }

	}
}
