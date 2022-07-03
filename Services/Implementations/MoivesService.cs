using MoviesApi.Services.Interfaces;
using System.Linq;

namespace MoviesApi.Services.Implementations
{
    public class MoivesService : IMoivesService
    {
        private readonly ApplicationDbContext _context;

        public MoivesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Moive> Add(Moive moive)
        {
            await _context.moives.AddAsync(moive);

            _context.SaveChanges();
            return moive;
        }

        public Moive Delete(Moive moive)
        {
            _context.Remove(moive);
            _context.SaveChanges();
            return moive;
        }

        public async Task<Moive> GetMoiveByIdAsync(int id)
        {
            return await _context.moives.FindAsync(id);
        }

        public async Task<IEnumerable<Moive>> GetAll(byte genreId)
        {

            return await _context.moives
                .Where(m=>m.GenreId==genreId||genreId==0)
                 .Include(m => m.Genre)
                 .OrderByDescending(m => m.Rate)
                 .ToListAsync();
        }

        public Moive Update(Moive moive)
        {
            _context.Update(moive);
            _context.SaveChanges();
            return moive;
        }
    }
}
