using System.ComponentModel.DataAnnotations;

namespace Commander.Models.External.Input.Category
{
	public abstract class CategoryModifyModelBase
	{
		[Required]
		[MaxLength(250)]
		public string Description { get; set; }
	}
}
