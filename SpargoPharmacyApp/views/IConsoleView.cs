using SpargoPharmacyApp.models;
using SpargoPharmacyApp.models.models_view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.controllers
{    
    // Интерфейс для представления взаимодействия с пользовательским интерфейсом через консоль
    internal interface IConsoleView
    {
        // команда для возврата в главное меню программы
        string TO_BACK_COMMAND { get; }

        // Вывод информации о товарах на складах аптеки
        void DisplayProductsInPharmacy(Pharmacy pharmacy, List<ProductInPharmacyView> productsInPharmacy);

        // Получение строкового пользовательского ввода с заданным сообщением prompt
        string GetStringUserInput(string prompt);

        // Получение целочисленного пользовательского ввода с заданным сообщением prompt
        int GetIntUserInput(string prompt);

        // Действия после выполнения команды пользователя
        void AfterExecute(string notification);        
    }
}
