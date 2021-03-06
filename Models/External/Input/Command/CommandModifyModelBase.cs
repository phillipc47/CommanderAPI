﻿using System.ComponentModel.DataAnnotations;

namespace Commander.Models.External.Input.Command
{
	public abstract class CommandModifyModelBase
	{
		[Required]
		[MaxLength(250)]
		public string HowTo { get; set; }
		[Required]
		public string Line { get; set; }
		[Required]
		public string Platform { get; set; }
	}
}
