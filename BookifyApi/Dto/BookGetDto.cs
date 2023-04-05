namespace Bookify.Dto
{
    public class BookGetDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
