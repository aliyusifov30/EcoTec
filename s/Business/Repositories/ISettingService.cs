using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{

	public interface ISettingService {
		Task<bool> AddAsync(Setting setting);
		bool Update(Setting setting);
		IQueryable<Setting> GetAll(bool tracking = true);
		Task<int> SaveAsync();
		Task<Setting> GetAsync(Expression<Func<Setting, bool>> expression, bool tracking = true,params string[] includes);
	}
}
