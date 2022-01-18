using InventoryTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InventoryTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly InventoryTrackerDbContext _context;

        public HomeController(ILogger<HomeController> logger, InventoryTrackerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View(_context.Items.ToList().Where(item => !item.Deleted));
        }

        // GET: Home/DeletedItems
        public IActionResult DeletedItems()
        {
            return View(_context.Items.ToList().Where(item => item.Deleted));
        }

        // GET: Home/Details/{id}
        public IActionResult Details(Guid? id)
        {
            return GetItem(id);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View(new Item());
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item todo)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(todo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Home/Edit/{id}
        public IActionResult Edit(Guid? id)
        {
            return GetItem(id);
        }

        // POST: Home/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Home/Delete/{id}
        public IActionResult Delete(Guid? id)
        {
            return GetItem(id);
        }

        // POST: Home/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Item item)
        {
            if (ModelState.IsValid)
            {
                item.Deleted = true;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Home/Undelete/{id}
        public IActionResult Undelete(Guid? id)
        {
            return GetItem(id);
        }

        // POST: Home/Undelete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Undelete(Guid id)
        {
            Item item = _context.Items.Find(id);
            item.Deleted = false;
            item.DeleteComment = string.Empty;
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private IActionResult GetItem(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Item item = _context.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
