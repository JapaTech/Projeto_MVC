using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudoMVC.Services
{
    public class TouristAttractionService : ITouristAttractionService
    {
        public readonly MVC_DbContext _context;

        public TouristAttractionService(MVC_DbContext context) 
        {
            _context = context;
        }

        public bool Add(TouristAttraction item)
        {
            _context.Add(item);
            return Save();
        }

        public bool Delete(TouristAttraction item)
        {
            _context.Remove(item);
            return Save();
        }

        public async Task<IEnumerable<TouristAttraction>> GetAll()
        {
            return await _context.TouristAttractions.ToListAsync();
        }

        public async Task<TouristAttraction> GetByIdAsync(int id)
        {
            return await _context.TouristAttractions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TouristAttraction> GetByIdAsyncNoTracking(int id)
        {
            return await _context.TouristAttractions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TouristAttraction> GetByName(string name)
        {
            return await _context.TouristAttractions.FirstOrDefaultAsync(x => x.Name == name);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false ;
        }

        public bool Update(TouristAttraction item)
        {
            _context.Update(item);
            return Save();
        }
    }
}
