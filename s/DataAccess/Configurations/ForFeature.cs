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
	public class ForFeature : IEntityTypeConfiguration<Feature>
	{
		public void Configure(EntityTypeBuilder<Feature> builder)
		{
			builder.Property(f=>f.Title).IsRequired().HasMaxLength(40);
			builder.Property(f=>f.Description).IsRequired().HasMaxLength(150);
		}
	}
}
