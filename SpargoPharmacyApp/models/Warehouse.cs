using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.models
{
    // Класс представляет модель сущности Warehouse (склад аптеки) в БД
    internal class Warehouse : BaseModel
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public string Name { get; set; }        
    }
}
