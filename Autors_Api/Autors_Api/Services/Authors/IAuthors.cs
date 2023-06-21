using Autors_Api.Models;

namespace Autors_Api.Services.Authors
{
    public interface IAuthors
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(int id  , Author author);
        Task DeleteAuthorAsync(int id);
    }
}
