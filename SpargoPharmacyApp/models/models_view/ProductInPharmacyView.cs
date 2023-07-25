using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.models.models_view
{    
    // Класс представления товаров на складах конкретной аптеки
    internal class ProductInPharmacyView
    {        
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
