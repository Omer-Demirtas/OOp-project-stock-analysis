using Npgsql;
using Stock_analysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_analysis.Repository
{
    class CustomerRepositoryImpl : ICustomerRepository
    {
        public Customer Create(Customer customer)
        {
            String SQL = "insert into customers (name, surname, tel_no) values ('{0}','{1}','{2}')";
            
            SQL = String.Format(SQL, customer.name, customer.surName, customer.telNo);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();

            return customer;
        }

        public void Delete(int id)
        {
            String SQL = "DELETE FROM customers where id = {0}";

            SQL = String.Format(SQL, id);

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public List<Customer> GetAll()
        {
            String SQL = "SELECT * FROM customers";

            List<Customer> customers = new List<Customer>();

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Customer customer = new Customer(dr.GetInt32(0), dr.GetString(1), dr.GetString(2),
                        dr.GetString(3)
                        );


                    customers.Add(customer);
                }
                con.Close();
                return customers;
            }
        }

        public List<Customer> getAllByName()
        {
            return null;
        }

        public int GetIdByName(string name)
        {
            String SQL = "SELECT id FROM customers where name = '{0}'";

            SQL = String.Format(SQL, name);

            int id = 0;

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    id = dr.GetInt32(0);
                }
                con.Close();

                return id;
            }
        }

        public string GetNameById(int id)
        {
            String SQL = "SELECT name FROM customers where id = '{0}'";

            SQL = String.Format(SQL, id);

            String name = "";

            NpgsqlConnection con = new NpgsqlConnection(Settings.ConnectionString);

            con.Open();

            using (var cmd = new NpgsqlCommand(SQL, con))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    name = dr.GetString(0);
                }
                con.Close();

                return name;
            }
        }

        public void Update(Customer customer)
        {
            String SQL = "Update customers set name = '{1}' , surname = '{2}' , tel_no = '{3}'where id = {0}";

            SQL = String.Format(SQL, customer.id, customer.name, customer.surName, customer.telNo);

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
