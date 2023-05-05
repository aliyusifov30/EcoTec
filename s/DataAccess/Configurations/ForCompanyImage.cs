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
	public class ForCompanyImage : IEntityTypeConfiguration<CompanyImage>
	{
		public void Configure(EntityTypeBuilder<CompanyImage> builder)
		{
			builder.Property(x => x.Image).IsRequired();
		}
	}
}
