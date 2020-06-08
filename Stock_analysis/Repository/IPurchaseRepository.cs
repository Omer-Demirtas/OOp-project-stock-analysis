using Stock_analysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository
{
    //Yapılan alımların tutulması içni 
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
    }
}
