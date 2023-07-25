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
    // Репозиторий для работы с сущностью "Batch" (партия товара) в БД
    internal class BatchRepository : BaseRepository, IRepositoryReader<Batch>, IRepositoryWriter<Batch>
    {
        public Batch GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Batch WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", Id);                

                try
                {                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = (int)reader["Id"];
                            int productId = (int)reader["ProductId"];
                            int warehouseId = (int)reader["WarehouseId"];                            
                            int quantity = (int)reader["Quantity"];                            
                            return new Batch { Id = id, ProductId = productId, WarehouseId = warehouseId, Quantity = quantity };
                        }
                    }                                  
                }
                catch (Exception e)
                {
                    // Выбрасываем исключение с помощью метода базового класса, если произошла ошибка при чтении из БД 
                    throw getReadErrorException(e);
                }
                // Выбрасываем исключение, если объект с указанным Id не найден  
                throw new DatabaseOperationException("Несуществующий ID партии товара");
            }
        }
        public int Add(Batch batch)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Batch (ProductId, WarehouseId, Quantity) VALUES (@ProductId, @WarehouseId, @Quantity); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@ProductId", batch.ProductId);
                command.Parameters.AddWithValue("@WarehouseId", batch.WarehouseId);
                command.Parameters.AddWithValue("@Quantity", batch.Quantity);                
                int newId = Convert.ToInt32(command.ExecuteScalar());
                return newId;            
            } 
        }

        public void Remove(Batch batch)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Batch WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", batch.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
