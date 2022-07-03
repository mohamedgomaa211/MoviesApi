namespace MoviesApi.Services.Interfaces
{
    public interface IMoivesService
    {
        Task<IEnumerable<Moive>> GetAll(byte genreId=0);
        Task<Moive>GetMoiveByIdAsync(int id);
        Task<Moive> Add(Moive moive);
        Moive Update(Moive moive);

        Moive Delete(Moive moive);
    }
}
