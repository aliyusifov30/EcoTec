using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories;
using Core.Entities;

namespace DataAccess.Repositories
{
    public class SupportImageRepository : Repository<SupportImage>, ISupportImageRepository
    {
        public SupportImageRepository(DataContext context) : base(context)
        {
        }
    }
}
