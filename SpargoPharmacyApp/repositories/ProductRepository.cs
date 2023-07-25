using SpargoPharmacyApp.exceptions;
using SpargoPharmacyApp.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.repositories
{
    // Репозиторий для работы с сущностью "Product" (товар) в БД
    internal class ProductRepository : BaseRepository, IRepositoryReader<Product>, IRepositoryWriter<Product>
    {
        public Product GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", Id);                

                try
                {                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {                        
                        if (reader.Read())
                        {
                            int id = (int)reader["Id"];
                            string name = (string)reader["Name"];
                            return new Product { Id = id, Name = name };                            
                        }                        
                    }                    
                }
                catch (Exception e)
                {
                    // Выбрасываем исключение с помощью метода базового класса, если произошла ошибка при чтении из БД 
                    throw getReadErrorException(e);
                }
                // Выбрасываем исключение, если объект с указанным Id не найден  
                throw new DatabaseOperationException("Несуществующий ID товара");
            }
        }

        public int Add(Product product)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Product (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Name", product.Name);                
                int newId = Convert.ToInt32(command.ExecuteScalar());
                return newId;
            }
        }        

        public void Remove(Product product)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Product WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.ExecuteNonQuery();
            }
        }        

    }
}
