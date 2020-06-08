using Npgsql;
using Stock_analysis.Models;
using Stock_analysis.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository.Implementation
{
    class SaleRepositoryImpl : ISaleRepository
    {
        public Sale Create(Sale sale)
        {
            String SQL = "insert into sales (customer_id, product_code, sale_count,sale_date, sale_profit)values ({0},{1},{2},'{3}',{4}) ";

            SQL = String.Format(SQL, sale.customerId, sale.productCode, sale.saleCount, sale.saleDate, sale.salesProfit);

            var con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();

            return sale;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByCustomerId(int customerId)
        {
            String SQL = "DELETE FROM sales where customer_id = {0}";

            SQL = String.Format(SQL, customerId);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public List<Sale> GetAll()
        {
            String SQL = "SELECT * FROM sales";


            List<Sale> sales = new List<Sale>();

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Sale sale  = new Sale(dr.GetInt32(0), dr.GetInt32(1),
                        dr.GetInt32(2), dr.GetDateTime(4), dr.GetInt32(3),dr.GetInt32(5));


                    sales.Add(sale);
                }
                return sales;
            }
        }

        public List<Sale> getAllByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public void Update(Sale item)
        {
            throw new NotImplementedException();
        }
    }
}
