using Stock_analysis.Models;
using Stock_analysis.Repository;
using Stock_analysis.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_analysis
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Repository'leri tanımlayıp ana formumuza yolluyoruz.
            IPurchaseRepository purchaseRepo = new PurchaseRepositoryImpl();
            ICustomerRepository customerRepo = new CustomerRepositoryImpl();
            ISaleRepository salesRepo = new SaleRepositoryImpl();
            IProductRepository productRepo = new ProductRepositoryImpl();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BaseScreen(customerRepo, salesRepo, productRepo, purchaseRepo));
        }
    }
}
