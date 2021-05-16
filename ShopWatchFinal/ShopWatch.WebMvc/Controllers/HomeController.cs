using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.Model.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Model;

namespace ShopWatch.WebMvc.Controllers
{
	public class HomeController : Controller
	{
		private IWatchService _watchServices;
		private ShopWatchDataContext _context = new ShopWatchDataContext();

		public HomeController(IWatchService watchServices)
		{
			_watchServices = watchServices;
		}
		public ActionResult Index(string search, int? page)
		{
			Func<IQueryable<Watch>, IOrderedQueryable<Watch>> orderBy = null;
			orderBy = b => b.OrderBy(s => s.Quantity);
			if (search==null || search=="")
			{
				var watches = _watchServices.GetAsync(orderBy: orderBy,page: page ?? 1, pageSize: 10);
				page = 1;
				return View(watches);
			}
			else
			{
				Expression<Func<Watch, bool>> filter = null;
				filter = a => a.WatchName.Contains(search);
				var watches = _watchServices.GetAsync(filter: filter, orderBy: orderBy, page: page ?? 1, pageSize: 10);
				return View(watches);
				
			}	
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}