using SpargoPharmacyApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.repositories
{
    // Интерфейс для чтения из БД, параметризованный типом T, который должен быть производным от класса BaseModel
    interface IRepositoryReader<T> where T : BaseModel
    {
        // Метод для получения объекта типа T по его уникальному идентификатору (Id) из БД
        T GetById(int Id);
    }
}
