using Stock_analysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        void DeleteByCustomerId(int customerId);
        List<Sale> getAllByProductId(int productId);
    }
}
