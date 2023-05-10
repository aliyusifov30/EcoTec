using Core.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public class Service:BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Icon { get; set; }
		public string? Content { get; set; }
	}
}
