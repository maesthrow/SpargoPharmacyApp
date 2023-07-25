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
    // Репозиторий для работы с сущностью "Warehouse" (склад аптеки) в БД
    internal class WarehouseRepository : BaseRepository, IRepositoryReader<Warehouse>, IRepositoryWriter<Warehouse>
    {
        public Warehouse GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Warehouse WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", Id);                

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = (int)reader["Id"];
                            int pharmacyId = (int)reader["PharmacyId"];
                            string name = (string)reader["Name"];                            
                            return new Warehouse { Id = id, PharmacyId = pharmacyId, Name = name };
                        }
                    }                    
                }
                catch (Exception e)
                {
                    // Выбрасываем исключение с помощью метода базового класса, если произошла ошибка при чтении из БД 
                    throw getReadErrorException(e);
                }
                // Выбрасываем исключение, если объект с указанным Id не найден  
                throw new DatabaseOperationException("Несуществующий ID склада");
            }
        }

        public int Add(Warehouse warehouse)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Warehouse (PharmacyId, Name) VALUES (@PharmacyId, @Name); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@PharmacyId", warehouse.PharmacyId);
                command.Parameters.AddWithValue("@Name", warehouse.Name);                
                int newId = Convert.ToInt32(command.ExecuteScalar());
                return newId;
            }
        }

        public void Remove(Warehouse warehouse)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Warehouse WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", warehouse.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
