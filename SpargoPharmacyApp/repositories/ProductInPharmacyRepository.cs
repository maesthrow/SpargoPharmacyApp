using SpargoPharmacyApp.models.models_view;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.repositories
{
    // Репозиторий для работы с представлением ProductInPharmacyView (товары на складах конкретной аптеки)
    internal class ProductInPharmacyRepository : BaseRepository
    {
        
        // Метод для получения списка товаров и их количества на складе указанной аптеки по её идентификатору (pharmacyId)
        // Возвращает список объектов типа ProductInPharmacyView,
        // содержащих информацию о товаре (Id и Name) и его количестве на складе (Quantity)
        public List<ProductInPharmacyView> GetProductsAndQuantitiesInPharmacy(int pharmacyId)
        {
            var productsInPharmacy = new List<ProductInPharmacyView>();            

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"
                SELECT p.Id, p.Name, SUM(b.Quantity) as Quantity
                FROM Product p
                LEFT JOIN Batch b ON p.Id = b.ProductId
                LEFT JOIN Warehouse w ON b.WarehouseId = w.Id
                WHERE w.PharmacyId = @PharmacyId
                GROUP BY p.Id, p.Name;", connection);

                command.Parameters.AddWithValue("@PharmacyId", pharmacyId);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {                        
                        while (reader.Read())
                        {
                            int productId = (int)reader["Id"];
                            string productName = (string)reader["Name"];                            
                            int quantity = (reader["Quantity"] == DBNull.Value) ? 0 : (int)reader["Quantity"];

                            productsInPharmacy.Add(
                                new ProductInPharmacyView { ProductId = productId, ProductName = productName, Quantity = quantity });                            
                        }
                        return productsInPharmacy;
                    }
                }
                catch (Exception e)
                {
                    // Выбрасываем исключение с помощью метода базового класса, если произошла ошибка при чтении из БД 
                    throw getReadErrorException(e);
                }
            }
        }
    }
}
