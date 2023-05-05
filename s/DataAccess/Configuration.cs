using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class Configuration
	{
		public static string GetConnectionString { get{
				ConfigurationManager configurationManager = new();
				configurationManager.SetBasePath(Directory.GetCurrentDirectory());
				configurationManager.AddJsonFile("appsettings.json");
				return configurationManager.GetConnectionString("Default");			
			} }
	}
}
