using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository
{
    public interface IGenericRepository<T>
    {
        void Delete(int id);
        void Update(T item);
        T Create(T item);

        List<T> GetAll();
    }
}
