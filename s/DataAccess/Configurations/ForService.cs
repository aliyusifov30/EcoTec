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
	public class ForService : IEntityTypeConfiguration<Service>
	{
		public void Configure(EntityTypeBuilder<Service> builder)
		{
			builder.Property(x => x.Image).IsRequired();
			builder.Property(x=>x.Title).IsRequired().HasMaxLength(50);
			builder.Property(x=>x.Description).IsRequired().HasMaxLength(200);		}
	}
}
