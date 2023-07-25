using SpargoPharmacyApp.exceptions;
using SpargoPharmacyApp.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.repositories
{
    // BaseRepository - базовый класс для всех репозиториев
    // содержит строку подключения к БД, которая будет использована в наследниках для работы с базой данных
    // содержит метод, который генерирует пользовательское исключение в случае, если произошла ошибка при чтении из БД     
    internal abstract class BaseRepository
    {
        protected string ConnectionString { get; private set; }
        protected BaseRepository()
        {            
            ConnectionString = Settings.Default.SpargoPharmacyConnectionString;
        }
                         
        protected virtual DatabaseOperationException getReadErrorException(Exception e)
        {            
            return new DatabaseOperationException("Произошла ошибка при чтении из базы данных", e);
        }
    }
}
