using Stock_analysis.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Models
{
    public class Sale
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public int productCode { get; set; }
        public int saleCount { get; set; }
        public DateTime saleDate { get; set; }
        public int salesProfit { get; set; }


        public Sale(int id, int customerId, int productCode, DateTime SaleDate, int saleCount, int salesProfit)
        {
            this.id = id;
            this.customerId = customerId;
            this.productCode = productCode;
            this.saleDate = saleDate;
            this.saleCount = saleCount;
            this.salesProfit = salesProfit;
        }
        public Sale(int customerId, int productCode, DateTime SaleDate, int saleCount, int salesProfit)
        {
            this.customerId = customerId;   
            this.productCode = productCode;
            this.saleDate = saleDate;
            this.saleCount = saleCount;
            this.salesProfit = salesProfit;
        }

    }
}
