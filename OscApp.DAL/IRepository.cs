using System.Collections.Generic;

namespace OscApp.DAL
{
    public interface IRepository<T>
    {
        int Count(string searchTerm = "");
        ICollection<T> GetAll(int skip = 0, int take = 50, string searchTerm = "");
        List<T> GetAllNotPaged();
        T GetById(int id);
        T Create(T model);
        void Update(T model);
        void Delete(int id);
    }
}