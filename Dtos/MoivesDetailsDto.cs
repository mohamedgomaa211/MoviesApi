namespace MoviesApi.Dtos
{
    public class MoivesDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] Poster { get; set; }

        public double Rate { get; set; }

        public int year { get; set; }

        public string StoryLine { get; set; }

        public byte GenreId { get; set; }
        public string GenreName { get; set; }

    }
}
