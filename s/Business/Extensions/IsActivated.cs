using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extentsions
{
	public static class IsActivated
	{
		public static string IsActive(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClass = "active")
		{
			string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
			string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

			IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
			IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');
			var data= acceptedActions.Select(x=>x.ToLower()).Contains(currentAction.ToLower()) && acceptedControllers.Select(x => x.ToLower()).Contains(currentController.ToLower()) ? cssClass : string.Empty;
			return data;
		}
	}
}
