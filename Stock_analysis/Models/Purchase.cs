using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Models
{
    public class Purchase
    {
        public int id;
        public int productCode;
        public int PurchaseAmount;
        public double purchasePrice;
        public DateTime purchaseDate;

        public Purchase(int id, int productCode, int purchaseAmount,
            double purchasePrice, DateTime purchaseDate)
        {
            this.id = id;
            this.productCode = productCode;
            this.PurchaseAmount = purchaseAmount;
            this.purchasePrice = purchasePrice;
            this.purchaseDate = purchaseDate;
        }
        public Purchase(int productCode, int purchaseAmount,
            double purchasePrice, DateTime purchaseDate)
        {
            this.productCode = productCode;
            this.PurchaseAmount = purchaseAmount;
            this.purchasePrice = purchasePrice;
            this.purchaseDate = purchaseDate;
        }


    }
}
