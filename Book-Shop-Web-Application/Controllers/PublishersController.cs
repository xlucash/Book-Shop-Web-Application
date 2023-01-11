﻿using Book_Shop_Web_Application.Data.Services;
using Book_Shop_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Book_Shop_Web_Application.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublishersService _service;

        public PublishersController(IPublishersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allPublishers = await _service.GetAllAsync();
            return View(allPublishers);
        }

        // GET: publishers/details/id
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        // GET: publishers/create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: publishers/create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("LogoPictureURL,Name,Description")] Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);
            
            await _service.AddAsync(publisher);
            return RedirectToAction(nameof(Index));
        }

        // GET publishers/edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        // POST: publishers/edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,LogoPictureURL,Name,Description")] Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);
            if (id == publisher.Id)
            {
                await _service.UpdateAsync(id, publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }


        // GET publishers/delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");
            return View(publisherDetails);
        }

        // POST: publishers/delete/id
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}