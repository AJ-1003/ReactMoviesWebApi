namespace ReactMoviesWebApi.Entities
{
    public class ActorPicture
    {
        public Guid PictureId { get; set; }
        public Guid ActorId { get; set; }
        public IFormFile Picture { get; set; }
        public Actor Actor { get; set; }
    }
}
