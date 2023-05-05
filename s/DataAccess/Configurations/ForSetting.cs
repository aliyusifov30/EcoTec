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
	public class ForSetting : IEntityTypeConfiguration<Setting>
	{
		public void Configure(EntityTypeBuilder<Setting> builder)
		{
			builder.Property(x => x.Key).IsRequired().HasMaxLength(25);
			builder.Property(x=>x.Value).IsRequired().HasMaxLength(300);
		}
	}
}
