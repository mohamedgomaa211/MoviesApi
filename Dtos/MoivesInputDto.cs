namespace MoviesApi.Dtos
{
    public class MoivesInputDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public  IFormFile ? Poster { get; set; }

        public double Rate { get; set; }

        public int year { get; set; }
        [MaxLength(2500)]

        public string StoryLine { get; set; }


        public byte GenreId { get; set; }
    }
}
