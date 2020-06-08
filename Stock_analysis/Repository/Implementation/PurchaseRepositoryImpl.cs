using Npgsql;
using Stock_analysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository.Implementation
{
    class PurchaseRepositoryImpl : IPurchaseRepository
    {
        public Purchase Create(Purchase purchase)
        {
            String SQL = "insert into purchase (product_code , purchase_amount , purchase_price, purchase_date) values ({0},{1},{2},'{3}')";


            SQL = String.Format(SQL, purchase.productCode, purchase.PurchaseAmount, purchase.purchasePrice, purchase.purchaseDate);

            var con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();

            return purchase;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Purchase> GetAll()
        {
            String SQL = "SELECT * FROM purchase";


            List<Purchase> purchases = new List<Purchase>();

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Purchase purchase = new Purchase(dr.GetInt32(0),
                        dr.GetInt32(1),dr.GetInt32(2),dr.GetDouble(3) ,dr.GetDateTime(4));
                    
                    purchases.Add(purchase);
                }
                return purchases;
            }
        }

        public void Update(Purchase item)
        {
            throw new NotImplementedException();
        }
    }
}
