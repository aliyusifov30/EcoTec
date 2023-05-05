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
	public class ForSlider : IEntityTypeConfiguration<Slider>
	{
		public void Configure(EntityTypeBuilder<Slider> builder)
		{
			builder.Property(s=>s.Title).IsRequired().HasMaxLength(100);
			builder.Property(s=>s.Description).IsRequired().HasMaxLength(100);
			builder.Property(s=>s.ButtonText1).IsRequired().HasMaxLength(30);
			builder.Property(s=>s.ButtonText2).IsRequired().HasMaxLength(30);
		}
	}
}
