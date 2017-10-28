using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsIntro.Repository
{
    public interface IWriteRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void DetectUpdate(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Save();
    }
}
