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
    public class ForSupportImage: IEntityTypeConfiguration<SupportImage>
    {
        public void Configure(EntityTypeBuilder<SupportImage> builder)
        {
            builder.Property(x => x.Text).IsRequired().HasMaxLength(50);
            
        }
    }
}
