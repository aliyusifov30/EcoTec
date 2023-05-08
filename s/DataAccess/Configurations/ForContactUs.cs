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
	public class ForContactUs : IEntityTypeConfiguration<ContactUs>
	{
		public void Configure(EntityTypeBuilder<ContactUs> builder)
		{
			builder.Property(c=>c.FullName).IsRequired().HasMaxLength(50);
			builder.Property(c=>c.Email).IsRequired().HasMaxLength(60);
			builder.Property(c=>c.Text).IsRequired().HasMaxLength(800);
			builder.Property(c => c.Status).IsRequired();
		}
	}
}
