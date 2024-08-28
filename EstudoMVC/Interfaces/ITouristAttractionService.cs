﻿using EstudoMVC.Models;

namespace EstudoMVC.Interfaces
{
    public interface ITouristAttractionService
    {
        Task<IEnumerable<TouristAttraction>> GetAll();
        Task<TouristAttraction> GetByIdAsync(string id);
        Task<IEnumerable <TouristAttraction>> GetByName(string name);
        bool Add(TouristAttraction item);
        bool Delete(TouristAttraction item);
        bool Update(TouristAttraction item);
        bool Save();
    }
}
