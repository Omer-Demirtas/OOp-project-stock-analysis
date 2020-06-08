using Stock_analysis.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        int GetCodeByName(String name);
        String GetNameByCode(int code);
        int GetLastId();
        Product GetByName(String name);
        Product GetByCode(int code);

        int GetLastCode();
    }
}
