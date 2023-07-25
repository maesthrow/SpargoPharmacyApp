using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.presenters
{
    // интерфейс презентера главного меню программы, обрабатывающего пользовательские команды 
    interface IMenuPresenter
    {
        void AddProduct();
        void RemoveProduct();
        void AddPharmacy();
        void RemovePharmacy();
        void AddWarehouse();
        void RemoveWarehouse();
        void AddBatch();
        void RemoveBatch();
        void GetProductsAndQuantitiesInPharmacy();
    }
}
