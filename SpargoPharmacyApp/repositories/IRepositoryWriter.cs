using SpargoPharmacyApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.repositories
{
    // Интерфейс для записи в БД, параметризованный типом T, который должен быть производным от класса BaseModel
    internal interface IRepositoryWriter<T> where T : BaseModel
    {
        // Метод для добавления объекта типа T в БД
        int Add(T entity);

        // Метод для удаления объекта типа T из БД
        void Remove(T entity);
    }
}
