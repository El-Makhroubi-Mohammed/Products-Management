using Activity2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Activity2.Services
{
    public class ProductsDAO : IProductDataService
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int Delete(ProductModel product)
        {
            int newIdNumber = -1;

            string sqlStatment = "DELETE FROM dbo.Products WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.AddWithValue("@Id", product.Id);

                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return newIdNumber;
        }

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string sqlStatment = "SELECT * FROM dbo.Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return foundProducts;
        }

        public ProductModel GetProductById(int id)
        {
            ProductModel foundProduct = null;

            string sqlStatment = "SELECT * FROM dbo.Products WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundProduct = new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return foundProduct;
        }

        public int Insert(ProductModel product)
        {
            Int32 rowAffected = 0;
            string sqlStatement = "INSERT INTO dbo.Products (Name, Price, Description) VALUES (@Name, @Price, @Description)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 50).Value = product.Name;
                command.Parameters.Add("@Price", System.Data.SqlDbType.Decimal, 18).Value = product.Price;
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 50).Value = product.Description;

                try
                {
                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message); ;
                }
            }

            return rowAffected;
        }

        public List<ProductModel> SearchProducts(string searchTerm)
        {
            List<ProductModel> foundProducts = new List<ProductModel>();

            string sqlStatment = "SELECT * FROM dbo.Products WHERE Name LIKE @Name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.AddWithValue("@Name", "%" +searchTerm+ "%");

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return foundProducts;
        }

        public int Update(ProductModel product)
        {
            int newIdNumber = -1;

            string sqlStatment = "UPDATE dbo.Products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Id", product.Id);

                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return newIdNumber;
        }
    }
}
