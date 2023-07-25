using SpargoPharmacyApp.controllers;
using SpargoPharmacyApp.exceptions;
using SpargoPharmacyApp.models;
using SpargoPharmacyApp.models.models_view;
using SpargoPharmacyApp.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp
{
    // Класс представления (View) для взаимодействия с пользователем через консоль
    // Реализует интерфейс IConsoleView для обеспечения связи с презентером
    internal class ConsoleView : IConsoleView 
    {
        private const string _TO_BACK_COMMAND = "<";               // команда для возврата
        public string TO_BACK_COMMAND { get => _TO_BACK_COMMAND; } // в главное меню программы

        private string TO_BACK_COMMAND_PROMPT = $" /для возврата в Меню введите '{_TO_BACK_COMMAND}'/"; // подсказка для возврата в главное меню


        private MenuPresenter menuPresenter; // экземпляр класса MenuPresenter для обработки пользовательских команд меню

        public ConsoleView()
        {
            // инициализируем экземпляры репозиториев для работы с БД
            var productRepository = new ProductRepository();
            var pharmacyRepository = new PharmacyRepository();
            var warehouseRepository = new WarehouseRepository();
            var batchRepository = new BatchRepository();
            var productInPharmacyRepository = new ProductInPharmacyRepository();

            // инициализируем экземпляр класса MenuPresenter для обработки пользовательских команд
            menuPresenter = new MenuPresenter(
                productRepository, pharmacyRepository, warehouseRepository, batchRepository, productInPharmacyRepository, this);
        }

        // Метод для отображения меню и обработки команд пользователя
        public void StartMenu()
        {            
            while (true)
            {
                // выводим меню команд в консоль
                displayMenu();

                string choice = Console.ReadLine(); // получаем команду, введенную пользователем
                switch (choice)
                {
                    case "0":   // Выход из программы                            
                        return;
                    case "1":   // Добавить товар
                        {   
                            menuPresenter.AddProduct();                            
                            break;
                        }
                    case "2":   // Удалить товар
                        {   
                            menuPresenter.RemoveProduct();                            
                            break;
                        }
                    case "3":   // Добавить аптеку
                        {   
                            menuPresenter.AddPharmacy();                            
                            break;
                        }
                    case "4":   // Удалить аптеку
                        {   
                            menuPresenter.RemovePharmacy();
                            break;
                        }
                    case "5":   // Добавить склад для аптеки
                        {   
                            menuPresenter.AddWarehouse();
                            break;
                        }
                    case "6":   // Удалить склад
                        {   
                            menuPresenter.RemoveWarehouse();
                            break;
                        }
                    case "7":   // Добавить партию товара
                        {   
                            menuPresenter.AddBatch();
                            break;
                        }
                    case "8":   // Удалить партию товара
                        {   
                            menuPresenter.RemoveBatch();
                            break;
                        }
                    case "9":   // Получить информацию о товарах на складах аптеки
                        {   
                            menuPresenter.GetProductsAndQuantitiesInPharmacy();
                            break;
                        }
                    default:    // Неизвестная команда
                        {   
                            unknownCommand();
                            break;
                        }
                }
            }
        }

        // Выводит меню команд в консоль
        private void displayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Введите одну из команд:");
            Console.WriteLine("1 - Добавить товар");
            Console.WriteLine("2 - Удалить товар");
            Console.WriteLine("3 - Добавить аптеку");
            Console.WriteLine("4 - Удалить аптеку");
            Console.WriteLine("5 - Добавить склад");
            Console.WriteLine("6 - Удалить склад");
            Console.WriteLine("7 - Добавить партию товара");
            Console.WriteLine("8 - Удалить партию товара");
            Console.WriteLine("9 - Вывести информацию о наличии товаров в аптеке");
            Console.WriteLine("0 - Выйти");
            Console.WriteLine();
        }

        // Выводит сообщение об ошибке из-за ввода пользователем неизвестной команды
        private void unknownCommand()
        {
            Console.WriteLine("Неизвестная команда");
        }

        // Выводит информацию о товарах на складах аптеки
        public void DisplayProductsInPharmacy(Pharmacy pharmacy, List<ProductInPharmacyView> productsInPharmacy)
        {
            Console.WriteLine();
            Console.WriteLine($"Список товаров на складах аптеки '{pharmacy.Name}:'");
            //Console.WriteLine();
            foreach (var productInPharmacy in productsInPharmacy)
            {
                Console.WriteLine($"Товар ID: {productInPharmacy.ProductId} | " +
                                  $"Наименование: {productInPharmacy.ProductName} | " +
                                  $"Количество: {productInPharmacy.Quantity}");
            }
            // ожидаем подтверждения от пользователя после отображения информации
            confirmationFromUser();
        }

        // Получает строковый пользовательский ввод с заданным сообщением prompt
        public string GetStringUserInput(string prompt)
        {
            displayPromptString(prompt);
            return Console.ReadLine();
        }

        // Получает целочисленный пользовательский ввод с заданным сообщением prompt
        public int GetIntUserInput(string prompt)
        {
            int result = 0;
            while (true)
            {
                displayPromptString(prompt);
                try
                {
                    string request = Console.ReadLine();
                    if (request == "<")
                        break;
                    result = int.Parse(request);
                    if (result > 0)
                        break;
                    else
                        Console.WriteLine("Значение должно быть положительным числом");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            return result;
        }

        // Выводит подсказку с сообщением prompt для пользовательского ввода, 
        // включая дополнительную инструкцию о возврате в меню с помощью TO_BACK_COMMAND_PROMPT
        private void displayPromptString(string prompt)
        {
            Console.WriteLine($"{prompt} {TO_BACK_COMMAND_PROMPT}");
        }

        // Выводит сообщение notification после выполнения операции и ожидает подтверждения от пользователя
        public void AfterExecute(string notification)
        {
            Console.WriteLine(notification);
            confirmationFromUser();
        }

        // Выводит сообщение для ожидания подтверждения от пользователя после выполнения операции
        private void confirmationFromUser()
        {            
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter, чтобы продолжить");
            Console.ReadLine();
        }
    }
}
