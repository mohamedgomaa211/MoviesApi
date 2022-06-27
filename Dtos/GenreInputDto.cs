namespace MoviesApi.Dtos
{
    public class GenreInputDto
    {
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
