using Business.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class SettingService : ISettingService
	{
		private readonly DataContext _context;

		public SettingService(DataContext context)
		{
			_context = context;
		}

		public DbSet<Setting> Table => _context.Set<Setting>();
		public async Task<bool> AddAsync(Setting entity)
		{
			EntityEntry<Setting> entry = await Table.AddAsync(entity);
			return entry.State == EntityState.Added;
		}

		public IQueryable<Setting> GetAll(bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = query.AsNoTracking();
			return query;
		}

		public IQueryable<Setting> GetAsync(Expression<Func<Setting, bool>> expression, bool tracking = true, params string[] includes)
		{
			var query = Table.Where(expression);
			if (!tracking) query.AsNoTracking();
			for (int i = 0; i < includes.Length; i++)
			{
				query = query.Include(includes[i]);
			}
			return query;
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		

		public bool Update(Setting entity)
		{
			EntityEntry entry = Table.Update(entity);
			return entry.State == EntityState.Modified;
		}

		Task<Setting> ISettingService.GetAsync(Expression<Func<Setting, bool>> expression, bool tracking, params string[] includes)
		{
			var query = Table.AsQueryable();
			if (!tracking) query.AsNoTracking();
			for (int i = 0; i < includes.Length; i++)
			{
				query = query.Include(includes[i]);
			}
			return query.FirstOrDefaultAsync(expression);
		}
	}
}
