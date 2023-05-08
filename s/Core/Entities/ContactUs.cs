using Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public class ContactUs:BaseEntity
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Text { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
		public bool Status { get; set; }

	}
}
