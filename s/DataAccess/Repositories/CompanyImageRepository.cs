using Business.Repositories;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class CompanyImageRepository : Repository<CompanyImage>, ICompanyImageRepository
	{
		public CompanyImageRepository(DataContext context) : base(context)
		{
		}
	}
}
