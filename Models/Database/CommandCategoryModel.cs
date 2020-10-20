using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models.Database
{
	public class CommandCategoryModel
	{
      [Key]
      public int Id { get; set; }

      [Required]
      [MaxLength(250)]
      public string Description { get; set; }

      public List<CommandModel> Commands { get; set; }
   }
}
