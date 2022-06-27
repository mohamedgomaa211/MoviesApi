namespace MoviesApi.Models
{
    public class Moive
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] Poster { get; set; }

        public double Rate { get; set; }

        public int year { get; set; }
        [MaxLength( 2500)]

        public string StoryLine { get; set; }


        //public byte GenreId { get; set; }

        //public Genre Genre { get; set; }


    }
}
