using Business.Repositories;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class FeatureRepository : Repository<Feature>, IFeatureRepository
	{
		public FeatureRepository(DataContext context) : base(context)
		{
		}
	}
}
