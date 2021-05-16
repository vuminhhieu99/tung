﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;

namespace ShopWatch.WebMvc.Areas.Admin.Controllers
{
	public class OrdersController : Controller
	{
		private ShopWatchDataContext db = new ShopWatchDataContext();

		// GET: Admin/Orders
		public ActionResult Index()
		{
			var orders = db.Orders.Include(o => o.User);
			return View(orders.ToList());
		}

		// GET: Admin/Orders/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			return View(order);
		}

		// GET: Admin/Orders/Create
		public ActionResult Create()
		{
			ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
			return View();
		}

		// POST: Admin/Orders/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "OrderId,CreatedDate,ModifiedDate,ShippedDate,Status,ShipAddress,PhoneNumber,UserId")] Order order)
		{
			if (ModelState.IsValid)
			{
				db.Orders.Add(order);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", order.UserId);
			return View(order);
		}

		// GET: Admin/Orders/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", order.UserId);
			return View(order);
		}

		// POST: Admin/Orders/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "OrderId,CreatedDate,ModifiedDate,ShippedDate,Status,ShipAddress,PhoneNumber,UserId")] Order order)
		{
			if (ModelState.IsValid)
			{
				db.Entry(order).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", order.UserId);
			return View(order);
		}

		// GET: Admin/Orders/Delete/5
		public ActionResult Delete(int? id)
		{
			//if (id == null)
			//{
			//	return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			//}
			Order order = db.Orders.Find(id);
			//if (order == null)
			//{
			//    return HttpNotFound();
			//}
			return View(order);
		}

		// POST: Admin/Orders/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Order order = db.Orders.Find(id);
			db.Orders.Remove(order);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
