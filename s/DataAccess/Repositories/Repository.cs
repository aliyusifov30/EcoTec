using Business.Repositories;
using Core.Entities.Common;
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
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly DataContext _context;

		public Repository(DataContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		

		public async Task<bool> AddAsync(T entity)
		{
			EntityEntry<T> entry=await Table.AddAsync(entity);
			return entry.State == EntityState.Added;
		}

		public IQueryable<T> GetAll(bool tracking = true)
		{
			var query = Table.AsQueryable();
			if(!tracking)
				query=query.AsNoTracking();
			return query;
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = true)
		{
			throw new NotImplementedException();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = true, params string[] includes)
		{
			throw new NotImplementedException();
		}

		public async Task<T> GetByIdAsync(int Id, bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = query.AsNoTracking();
			return await query.FirstOrDefaultAsync(x => x.Id == Id);
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = query.AsNoTracking();
			return await query.FirstOrDefaultAsync(filter);
		}

		public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
		{
			var query = Table.Where(expression);
			if (!tracking) query.AsNoTracking();
			return query;
		}

		public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true, params string[] includes)
		{
			var query = Table.Where(expression);
			if (!tracking) query.AsNoTracking();
			for (int i = 0; i < includes.Length; i++)
			{
				query = query.Include(includes[i]);
			}
			return query;
		}
		public bool Update(T entity)
		{
			EntityEntry entry = Table.Update(entity);
			return entry.State == EntityState.Modified;
		}
		public async Task<bool> Remove(int id)
		{
			var data = await GetByIdAsync(id);
			EntityEntry entry=Table.Remove(data);
			return entry.State == EntityState.Deleted;
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}	

		
	}
}
