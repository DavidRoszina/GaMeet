using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GaMeet.Controllers
{
    public class GamersController : Controller
    {
        // GET: GamersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GamersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GamersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GamersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GamersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GamersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GamersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GamersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
