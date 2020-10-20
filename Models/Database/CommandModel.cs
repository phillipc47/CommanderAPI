using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commander.Models.Database
{
	public class CommandModel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("CommandCategory")]
		public int CategoryId { get; set; }
		public CommandCategoryModel Category { get; set; }

		[Required]
		[MaxLength(250)]
		public string HowTo { get; set; }

		[Required]
		public string Line { get; set; }
	}
}
