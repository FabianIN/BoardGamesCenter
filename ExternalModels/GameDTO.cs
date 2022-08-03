namespace BoardGamesCenter.ExternalModels
{
    public class GameDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }

        public AuthorDTO Author { get; set; }

        public Guid PublisherId { get; set; }

        public PublisherDTO Publisher { get; set; }
    }
}
