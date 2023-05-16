using Core.Entities;
using Core.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class DataContext : IdentityDbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		public DbSet<CompanyImage> CompanyImages { get; set; }
		public DbSet<Feature> Features { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<WorkProcess> WorkProcesses { get; set; }
		public new DbSet<AppUser> Users { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<SupportImage> SupportImages { get; set; }
		public DbSet<SocialMedia> SocialMedias { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{	
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());			
			modelBuilder.Model.GetEntityTypes()
				.Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType))
				.ToList()
				.ForEach(e => {
					var parameter = Expression.Parameter(e.ClrType, "p");
					var body = Expression.Equal(
						Expression.Property(parameter, "IsDeleted"),
						Expression.Constant(false));
					var lambda = Expression.Lambda(body, parameter);
					modelBuilder.Entity(e.ClrType).HasQueryFilter(lambda);
				});
			base.OnModelCreating(modelBuilder);
		}
	}
}
