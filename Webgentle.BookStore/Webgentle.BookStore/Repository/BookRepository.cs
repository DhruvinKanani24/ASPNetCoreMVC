using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(a => a.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBook(string title, string authorName) 
        {
            return DataSource().Where(x => x.Title == title & x.Author == authorName).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id = 1, Title = "MVC", Author = "Dhruvin" },
                new BookModel(){Id = 2, Title = "C#", Author = "Jason" },
                new BookModel(){Id = 3, Title = "Core", Author = "John" },
                new BookModel(){Id = 4, Title = "Blazor", Author = "Wick" }
            };
        }
    }
}
