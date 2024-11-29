using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Data;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            newBook.bookGallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Catagory = book.Catagory,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList()
                }).ToListAsync();

            //var books = new List<BookModel>();
            //var allbooks = await _context.Books.ToListAsync();
            //if(allbooks?.Any() == true)
            //{
            //    foreach (var book in allbooks)
            //    {
            //        books.Add(new BookModel()
            //        {
            //            Author = book.Author,
            //            Catagory = book.Catagory,
            //            Description = book.Description,
            //            Id = book.Id,
            //            LanguageId = book.LanguageId,
            //            Language = book.Language.Name,
            //            Title = book.Title,
            //            TotalPages = book.TotalPages
            //        });
            //    }              
            //}
            //return books;
        }


        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Catagory = book.Catagory,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList()
                }).Take(count).ToListAsync();
        }



        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Catagory = book.Catagory,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();


            //var book = await _context.Books.FindAsync(id);
            //if(book != null)
            //{
            //    var bookDetails = new BookModel()
            //    {
            //        Author = book.Author,
            //        Catagory = book.Catagory,
            //        Description = book.Description,
            //        Id = book.Id,
            //        LanguageId = book.LanguageId,
            //        Language = book.Language.Name,
            //        Title = book.Title,
            //        TotalPages = book.TotalPages
            //    };
            //    return bookDetails;
            //}
            //return null;
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){Id = 1, Title = "MVC", Author = "Dhruvin", Description = "This is the description of MVC", Catagory = "Programming", Language = "English", TotalPages = 148 },
        //        new BookModel(){Id = 2, Title = "C#", Author = "Jason", Description = "This is the description of C#", Catagory = "Philosophy", Language = "English", TotalPages = 57 },
        //        new BookModel(){Id = 3, Title = "Core", Author = "John", Description = "This is the description of Core", Catagory = "Maths", Language = "English", TotalPages = 7584 },
        //        new BookModel(){Id = 4, Title = "Blazor", Author = "Wick", Description = "This is the description of Blazor", Catagory = "History", Language = "English", TotalPages = 458 }
        //    };
        //}

        public string GetAppName()
        {
            return "Book Store Application";
        }
    }
}
