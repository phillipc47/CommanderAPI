namespace Commander.Models.External.Output
{
	public class CommandReadModel
	{
		public int Id { get; set; }
		public string HowTo { get; set; }
		public string Line { get; set; }
		// Eventually can do entity link
		public string Category { get; set; }
	}
}
