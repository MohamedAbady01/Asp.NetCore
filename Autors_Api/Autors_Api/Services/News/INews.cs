using Autors_Api.Models;

namespace Autors_Api.Services.News
{
    public interface INews
    {
        Task<IEnumerable<NewsModel>> GetAllNewsAsync();
        Task<NewsModel> GetNewsByIdAsync(int id);

        Task<List<NewsModel>> GetNewsByAuthorName(string AuthorName);
        Task AddNewsAsync(NewsDto News, IFormFile image);
        Task UpdateNewsAsync(int id, NewsDto news, IFormFile image);
        Task DeleteNewsAsync(int id);
    }
}
