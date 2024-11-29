using System.Collections.Generic;
using System.Threading.Tasks;
using Webgentle.BookStore.Data;

namespace Webgentle.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<Language>> GetLanguages();
    }
}