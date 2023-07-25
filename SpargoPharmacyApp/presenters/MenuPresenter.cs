using SpargoPharmacyApp.exceptions;
using SpargoPharmacyApp.models;
using SpargoPharmacyApp.models.models_view;
using SpargoPharmacyApp.presenters;
using SpargoPharmacyApp.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpargoPharmacyApp.controllers
{
    // Презентер главного меню программы, обрабатывающий пользовательские команды
    internal class MenuPresenter : IMenuPresenter
    {
        private ProductRepository _productRepository;
        private PharmacyRepository _pharmacyRepository;
        private WarehouseRepository _warehouseRepository;
        private BatchRepository _batchRepository;
        private ProductInPharmacyRepository _productsInPharmacyRepository;        

        private readonly IConsoleView _consoleView; // экземпляр класса представления для взаимодействия с пользователем через консоль

        public MenuPresenter(
            ProductRepository productRepository, PharmacyRepository pharmacyRepository, 
            WarehouseRepository warehouseRepository, BatchRepository batchRepository, 
            ProductInPharmacyRepository productsInPharmacyRepository, IConsoleView consoleView)
        {
            _productRepository = productRepository;
            _pharmacyRepository = pharmacyRepository;
            _warehouseRepository = warehouseRepository;
            _batchRepository = batchRepository;
            _productsInPharmacyRepository = productsInPharmacyRepository;    
            
            _consoleView = consoleView;                    
        }

        // Добавляет товар в БД
        public void AddProduct()
        {
            try
            { 
                string productName = _consoleView.GetStringUserInput("Введите наименование товара:");
                if (productName.Equals(_consoleView.TO_BACK_COMMAND)) return;
            
                int newId = _productRepository.Add(new Product { Name = productName });
                _consoleView.AfterExecute($"Товар '{productName}' был добален! ID товара: {newId}");
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Удаляет товар из БД
        public void RemoveProduct()
        {
            try
            {
                int productId = _consoleView.GetIntUserInput("Введите ID товара:");
                if (productId == 0) return;
            
                var product = _productRepository.GetById(productId);                
                
                _productRepository.Remove(new Product { Id = productId });
                _consoleView.AfterExecute($"Товар '{product.Name}' удален.");
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Добавляет аптеку в БД
        public void AddPharmacy()
        {
            try
            {
                string name = _consoleView.GetStringUserInput("Введите наименование аптеки:");
                if (name.Equals(_consoleView.TO_BACK_COMMAND)) return;

                string address = _consoleView.GetStringUserInput("Введите адрес аптеки:");
                if (address.Equals(_consoleView.TO_BACK_COMMAND)) return;

                string phoneNumber = _consoleView.GetStringUserInput("Введите номер телефона аптеки:");
                if (phoneNumber.Equals(_consoleView.TO_BACK_COMMAND)) return;
            
                int newId = _pharmacyRepository.Add(new Pharmacy { Name = name, Address = address, PhoneNumber = phoneNumber });
                _consoleView.AfterExecute($"Аптека '{name}' была добалена! ID аптеки: {newId}");
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Удаляет аптеку из БД
        public void RemovePharmacy()
        {
            try
            {
                int pharmacyId = _consoleView.GetIntUserInput("Введите id аптеки:");
                if (pharmacyId == 0) return;
            
                var pharmacy = _pharmacyRepository.GetById(pharmacyId);                
                
                _pharmacyRepository.Remove(new Pharmacy { Id = pharmacyId });
                _consoleView.AfterExecute($"Аптека '{pharmacy.Name}' удалена.");
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Добавляет склад для конкретной аптеки в БД
        public void AddWarehouse()
        {
            try
            {
                int pharmacyId = _consoleView.GetIntUserInput("Введите id аптеки:");
                if (pharmacyId == 0) return;

                var pharmacy = _pharmacyRepository.GetById(pharmacyId);                

                string name = _consoleView.GetStringUserInput($"Введите наименование склада для аптеки '{pharmacy.Name}':");
                if (name.Equals(_consoleView.TO_BACK_COMMAND)) return;
            
                int newId = _warehouseRepository.Add(new Warehouse { PharmacyId = pharmacyId, Name = name });
                _consoleView.AfterExecute($"Склад '{name}' был добален! ID склада: {newId}");
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Удаляет склад из БД
        public void RemoveWarehouse()
        {
            try
            {
                int warehouseId = _consoleView.GetIntUserInput("Введите id склада:");
                if (warehouseId == 0) return;
                        
                var warehouse = _warehouseRepository.GetById(warehouseId);                
                
                _warehouseRepository.Remove(new Warehouse { Id = warehouseId });
                _consoleView.AfterExecute($"Склад '{warehouse.Name}' удален.");
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Добавляет партию товара на конкретном складе конкретной аптеки в БД
        public void AddBatch()
        {
            try
            {
                int productId = _consoleView.GetIntUserInput("Введите ID товара:");
                if (productId == 0) return;

                var product = _productRepository.GetById(productId);                

                int warehouseId = _consoleView.GetIntUserInput("Введите id склада:");
                if (warehouseId == 0) return;

                var warehouse = _warehouseRepository.GetById(warehouseId);                

                int quantity = _consoleView.GetIntUserInput($"Введите количество товара '{product.Name}':");
                if (quantity == 0) return;
            
                int newId = _batchRepository.Add(new Batch { ProductId = productId, WarehouseId = warehouseId, Quantity = quantity });
                _consoleView.AfterExecute($"Партия товара '{product.Name}' была добавлена! ID партии : {newId}");
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Удаляет партию товара из БД
        public void RemoveBatch()
        {
            try
            {
                int batchId = _consoleView.GetIntUserInput("Введите ID партии:");
                if (batchId == 0) return;
            
                var batch = _batchRepository.GetById(batchId);                
                var product = _productRepository.GetById(batch.ProductId);                
                
                _batchRepository.Remove(new Batch { Id = batchId });
                _consoleView.AfterExecute($"Партия ID: {batch.Id} товара '{product.Name}' была удалена.");                
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }

        // Получает информацию о товарах на складах аптеки и вызывает метод экземпляра класса представления для отображения полученных данных
        public void GetProductsAndQuantitiesInPharmacy()
        {
            try
            {
                int pharmacyId = _consoleView.GetIntUserInput("Введите ID аптеки, для которой хотите вывести список товаров и их количество:");
                if (pharmacyId == 0) return;

                var pharmacy = _pharmacyRepository.GetById(pharmacyId);                                            
                var productsInPharmacy = _productsInPharmacyRepository.GetProductsAndQuantitiesInPharmacy(pharmacyId);

                _consoleView.DisplayProductsInPharmacy(pharmacy, productsInPharmacy);
            }
            catch (Exception e)
            {
                _consoleView.AfterExecute(e.Message);
            }
        }
               
    }
}
