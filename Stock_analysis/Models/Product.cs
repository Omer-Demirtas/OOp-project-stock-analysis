using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Models
{
	public class Product
	{
		public int id { get; set; }
		public int code { get; set; }
		public String name { get; set; }
		public int purcheseAmount { get; set; }
		
		//public double salesPrice { get; set; }
		public double purchasePrice { get; set; }
		//public DateTime acquisitionDate { get; set; }

		public Product() { }
		public Product(int id, int code, String name, int purcheseAmount, 
		double purchase_price)
		{
			this.id = id;
            this.code = code;
			this.name = name;
			this.purcheseAmount = purcheseAmount;
			this.purchasePrice = purchase_price;

		}
		public Product(int code, String name, int purcheseAmount,
		double purchase_price)
		{
			this.code = code;
			this.name = name;
			this.purcheseAmount = purcheseAmount;
			this.purchasePrice = purchase_price;
		}
	}
}
