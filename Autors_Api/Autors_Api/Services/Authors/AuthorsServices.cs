using Autors_Api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Autors_Api.Services.Authors
{
    public class AuthorsServices : IAuthors
    {
        private readonly ApplicationDBContext _context;

        public AuthorsServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(int id,Author author)
        {
            var user = await _context.Authors.FindAsync(id);
            if(user == null)
            {
                throw new NotImplementedException("Author Not Found");
            }
            user.UserName = author.UserName;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
