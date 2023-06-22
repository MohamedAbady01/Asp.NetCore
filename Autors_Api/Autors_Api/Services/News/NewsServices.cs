
using Autors_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Autors_Api.Services.News
{
    public class NewsServices : INews
    {
        private readonly ApplicationDBContext _context;

        public NewsServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsModel>> GetAllNewsAsync()
        {

            return await _context.News.ToListAsync();
        }

        public async Task AddNewsAsync(NewsDto News, IFormFile image)
        {
           


            byte[] newsImage = null;

            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    newsImage = memoryStream.ToArray();
                }
            }

           var authors = await _context.Authors.ToListAsync();

            var selectedAuthor = authors.Find(a => a.Id == News.AuthorId);

            if (selectedAuthor == null)
            {
                throw new Exception("Invalid author selected.");
            }


            var newNews = new NewsModel()
            {
                Title = News.Title,
                CreationDate =DateTime.Now,
                PublicationDate = News.PublicationDate,
                AuthorId = News.AuthorId,
                Image = newsImage,
                NewsContent = News.NewsContent
            };

            await _context.News.AddAsync(newNews);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateNewsAsync(int id, NewsDto news, IFormFile? image)
        {
            var existingNews = await _context.News.FindAsync(id);
            if (existingNews == null)
            {
                throw new Exception("News not found");
            }

            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    existingNews.Image = memoryStream.ToArray();
                }
            }
            else
            {
                existingNews.Image = existingNews.Image;  
            }

            existingNews.Title = news.Title;
            existingNews.NewsContent = news.NewsContent;
            existingNews.PublicationDate = news.PublicationDate;
            existingNews.AuthorId = news.AuthorId;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteNewsAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<NewsModel> GetNewsByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }
        public bool IsPublicationDateValid(DateTime publicationDate)
        {
            var currentDate = DateTime.Today;
            var maxPublicationDate = currentDate.AddDays(7);

            return publicationDate >= currentDate && publicationDate <= maxPublicationDate;
        }

        public async Task<List<NewsModel>> GetNewsByAuthorName(string AuthorName)
        {
            var author =  await _context.Authors.FirstOrDefaultAsync(a => a.UserName == AuthorName) ;

            if(author == null)
            {
                throw new Exception("author not found");
            }
            var News = await _context.News.Where(n=> n.AuthorId== author.Id).ToListAsync();
            return (News);
        }
    }
}
