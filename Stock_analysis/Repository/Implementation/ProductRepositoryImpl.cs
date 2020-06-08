using Npgsql;
using Stock_analysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_analysis.Repository.Implementation
{
    class ProductRepositoryImpl : IProductRepository
    {
        public Product Create(Product product)
        {
            String SQL = "insert into products (code , name,purchase_amount, purchase_price) values ({0},'{1}',{2},{3})";

            SQL = String.Format(SQL, product.code, product.name, product.purcheseAmount, product.purchasePrice);

            var con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();

            return product;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            String SQL = "SELECT * FROM products";


            List<Product> products = new List<Product>();

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Product product = new Product(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2),
                        dr.GetInt32(3), dr.GetDouble(4));

                    products.Add(product);
                }
                return products;
            }
        }

        public Product GetByCode(int code)
        {
            String SQL = "SELECT * FROM products where code = {0}";

            SQL = String.Format(SQL,code);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                Product product = new Product(); ;
                while (dr.Read())
                {
                        product = new Product(dr.GetInt32(0), dr.GetInt32(1),
                        dr.GetString(2), dr.GetInt32(3), dr.GetDouble(4));
                }
                return product;
            }
        }

        public Product GetByName(string name)
        {
            String SQL = "SELECT * FROM products where name = '{0}'";

            SQL = String.Format(SQL, name);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                Product product = new Product(); ;
                while (dr.Read())
                {
                    product = new Product(dr.GetInt32(0), dr.GetInt32(1),
                    dr.GetString(2), dr.GetInt32(3), dr.GetDouble(4));
                }
                return product;
            }
        }

        public int GetCodeByName(string name)
        {
            String SQL = "SELECT code FROM products where name = '{0}'";

            SQL = String.Format(SQL, name);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            int code = 0;
            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    code =  dr.GetInt32(0);
                }
                return code;
            }
        }

        public int GetLastCode()
        {
            String SQL = "select MAX(code) from products";

            List<Product> products = new List<Product>();

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                int code = 0;
                while (dr.Read())
                {
                    try
                    {
                        code = dr.GetInt32(0);
                    }
                    catch
                    {

                    }
                }

                return code;
            }
        }

        public int GetLastId()
        {
            String SQL = "select MAX(id) from products";

            List<Product> products = new List<Product>();

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                int id = 0;
                while (dr.Read())
                {
                    id = dr.GetInt32(0);
                }

                return id;
            }

        }

        public string GetNameByCode(int code)
        {
            String SQL = "SELECT name FROM products where code = '{0}'";

            SQL = String.Format(SQL, code);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            String name = "";
            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    name = dr.GetString(0);
                }
                return name;
            }
        }

        public void Update(Product product)
        {

            String SQL = "Update products set purchase_amount = {0} , purchase_price = {1} where id = {2}";

            SQL = String.Format(SQL, product.purcheseAmount, product.purchasePrice, product.id);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}
