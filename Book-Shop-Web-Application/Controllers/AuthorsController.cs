using Book_Shop_Web_Application.Data;
using Book_Shop_Web_Application.Data.Services;
using Book_Shop_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Book_Shop_Web_Application.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _service;

        public AuthorsController(IAuthorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allAuthors = await _service.GetAllAsync();
            return View(allAuthors);
        }

        // GET: authors/create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: authors/create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Author author)
        {
            if(!ModelState.IsValid) return View(author);

            await _service.AddAsync(author);
            return RedirectToAction(nameof(Index));
        }

        //GET: authors/details/id
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }

        // GET: authors/edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }

        // POST: authors/edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Author author)
        {
            if (!ModelState.IsValid) return View(author);

            await _service.UpdateAsync(id, author);
            return RedirectToAction(nameof(Index));
        }

        // GET: authors/delete/id
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }

        // POST: authors/delete/1
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
