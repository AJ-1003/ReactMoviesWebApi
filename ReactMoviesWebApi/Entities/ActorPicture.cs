namespace ReactMoviesWebApi.Entities
{
    public class ActorPicture
    {
        public Guid PictureId { get; set; }
        public Guid ActorId { get; set; }
        public File Picture { get; set; }
        public Actor Actor { get; set; }
    }
}
