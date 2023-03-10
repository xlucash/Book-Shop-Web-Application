using Book_Shop_Web_Application.Data;
using Book_Shop_Web_Application.Data.Interfaces;
using Book_Shop_Web_Application.Data.Static;
using Book_Shop_Web_Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop_Web_Application.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class BooksController : Controller
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service=service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync(n => n.Publisher, n => n.Author);
            return View(allBooks);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await _service.GetAllAsync(n => n.Publisher, n => n.Author);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allBooks.Where(n => n.Title.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allBooks);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            return View(bookDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var bookDropdownData = await _service.GetBookDropdownValues();
            ViewBag.PublisherId = new SelectList(bookDropdownData.Publishers, "Id", "Name");
            ViewBag.AuthorId = new SelectList(bookDropdownData.Authors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if(!ModelState.IsValid)
            {
                var bookDropdownData = await _service.GetBookDropdownValues();
                ViewBag.PublisherId = new SelectList(bookDropdownData.Publishers, "Id", "Name");
                ViewBag.AuthorId = new SelectList(bookDropdownData.Authors, "Id", "FullName");

                return View(book);
            }
            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var respone = new BookViewModel()
            {
                Id = bookDetails.Id,
                Title = bookDetails.Title,
                Description = bookDetails.Description,
                Price = bookDetails.Price,
                YearOfPublication = bookDetails.YearOfPublication,
                ImageURL = bookDetails.ImageURL,
                PublisherId = bookDetails.PublisherId,
                AuthorId = bookDetails.AuthorId
            };


            var bookDropdownData = await _service.GetBookDropdownValues();
            ViewBag.PublisherId = new SelectList(bookDropdownData.Publishers, "Id", "Name");
            ViewBag.AuthorId = new SelectList(bookDropdownData.Authors, "Id", "FullName");

            return View(respone);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id,BookViewModel book)
        {
            if (id != book.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var bookDropdownData = await _service.GetBookDropdownValues();
                ViewBag.PublisherId = new SelectList(bookDropdownData.Publishers, "Id", "Name");
                ViewBag.AuthorId = new SelectList(bookDropdownData.Authors, "Id", "FullName");

                return View(book);
            }
            await _service.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }
    }
}
