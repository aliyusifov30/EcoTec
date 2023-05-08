﻿using Business.Repositories;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
	{
		public ContactUsRepository(DataContext context) : base(context)
		{
		}
	}
}
