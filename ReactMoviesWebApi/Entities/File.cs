namespace ReactMoviesWebApi.Entities
{
    public class File
    {
        public Guid Id { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
