using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;
using Webgentle.BookStore.Repository;

namespace Webgentle.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }
       
        public async Task<ActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("book-details/{id}", Name="bookDetailsRoute")]
        public async Task<ActionResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName,authorName);
        }

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            //var model = new BookModel()
            //{
            //    LanguageId = 1
            //};

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text="Hindi", Value="1"},
            //    new SelectListItem(){ Text="English", Value="2"},
            //    new SelectListItem(){ Text="French", Value="3"},
            //    new SelectListItem(){ Text="Spanish", Value="4"}
            //};

            //ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "Books/Cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
                }              

                if (bookModel.GalleryFiles != null)
                {
                    string folder = "Books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.Name,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    };
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "Books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            //ViewBag.Language = new SelectList( await _languageRepository.GetLanguages(), "Id", "Name");
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text="Hindi", Value="1"},
            //    new SelectListItem(){ Text="English", Value="2"},
            //    new SelectListItem(){ Text="French", Value="3"},
            //    new SelectListItem(){ Text="Spanish", Value="4"}
            //};
            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");

            //ModelState.AddModelError("", "This is my custom error message");  
            return View();
        }
         
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
       
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //    {
        //        new LanguageModel(){ Id=1, Text="Hindi" },
        //        new LanguageModel(){ Id=2, Text="English"},
        //        new LanguageModel(){ Id=3, Text="French"}
        //    };
        //}
    }
}
