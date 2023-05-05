using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
	public class ForWorkProcess : IEntityTypeConfiguration<WorkProcess>
	{
		public void Configure(EntityTypeBuilder<WorkProcess> builder)
		{
			builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
			builder.Property(x=>x.Description).IsRequired().HasMaxLength(250);
			builder.Property(x=>x.Icon).IsRequired().HasMaxLength(150);
		}
	}
}
