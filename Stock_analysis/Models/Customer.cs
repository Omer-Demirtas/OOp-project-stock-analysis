using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Models
{
    public class Customer 
    {
        public int id { get; set; }
        public String name { get; set; }
        public String surName { get; set; }
        public String telNo { get; set; }


        public Customer(int id, String name, String surName, String telNo)
        {
            this.name = name;
            this.surName = surName;
            this.id = id;
            this.telNo = telNo;
        }

        public Customer(String name, String surName, String telNo)
        {
            this.name = name;
            this.surName = surName;
            this.telNo = telNo;
        }
    }
}
