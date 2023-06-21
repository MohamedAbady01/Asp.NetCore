namespace Authors_MVC.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<NewsModel> News { get; set; }
    }
}
