using SpargoPharmacyApp.controllers;
using SpargoPharmacyApp.exceptions;
using SpargoPharmacyApp.models;
using SpargoPharmacyApp.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp
{
    class Program
    {              
        static void Main(string[] args)
        {
            // инициализируем экземпляр класса ConsoleView для взаимодействия с пользователем через консоль  
            var consoleView = new ConsoleView();

            // вызываем метод для отображения меню и обработки команд пользователя
            consoleView.StartMenu(); 
        }                                        
    }
}
