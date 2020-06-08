using Stock_analysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        List<Customer> getAllByName();

        String GetNameById(int id);

        int GetIdByName(String name);

    }
}
