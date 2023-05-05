using Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
	public interface IRepository<T> where T : BaseEntity
	{
		DbSet<T> Table { get; }
		Task<bool> AddAsync(T entity);
		bool Update(T entity);
		Task<bool> Remove(int id);

		Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = true);
		Task<T> GetAsync(Expression<Func<T, bool>> expression, bool tracking = true, params string[] includes);
		IQueryable<T> GetAll(bool tracking = true);
		IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true);
		IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true, params string[] includes);
		Task<T> GetByIdAsync(int Id, bool tracking = true);
		Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true);


		Task<int> SaveAsync();

	}
}
