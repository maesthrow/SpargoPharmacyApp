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
    // Репозиторий для работы с сущностью "Pharmacy" (аптека) в БД
    internal class PharmacyRepository : BaseRepository, IRepositoryReader<Pharmacy>, IRepositoryWriter<Pharmacy>
    {
        public Pharmacy GetById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Pharmacy WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", Id);                

                try
                {                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = (int)reader["Id"];
                            string name = (string)reader["Name"];
                            string address = (string)reader["Address"];
                            string phoneNumber = (string)reader["PhoneNumber"];
                            return new Pharmacy { Id = id, Name = name, Address = address, PhoneNumber = phoneNumber };
                        }
                    }                            
                }
                catch (Exception e)
                {
                    // Выбрасываем исключение с помощью метода базового класса, если произошла ошибка при чтении из БД 
                    throw getReadErrorException(e); 
                }
                // Выбрасываем исключение, если объект с указанным Id не найден  
                throw new DatabaseOperationException("Несуществующий ID аптеки");
            }
        }

        public int Add(Pharmacy pharmacy)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Pharmacy (Name, Address, PhoneNumber) VALUES (@Name, @Address, @PhoneNumber); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Name", pharmacy.Name);
                command.Parameters.AddWithValue("@Address", pharmacy.Address);
                command.Parameters.AddWithValue("@PhoneNumber", pharmacy.PhoneNumber);                
                int newId = Convert.ToInt32(command.ExecuteScalar());
                return newId;
            }
        }

        public void Remove(Pharmacy pharmacy)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Pharmacy WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", pharmacy.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
