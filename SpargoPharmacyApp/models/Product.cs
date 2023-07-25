using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.models
{
    // Класс представляет модель сущности Product (товар) в БД
    internal class Product : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }                
    }
}
