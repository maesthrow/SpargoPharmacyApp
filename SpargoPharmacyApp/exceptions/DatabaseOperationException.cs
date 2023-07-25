using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpargoPharmacyApp.exceptions
{
    // Пользовательское исключение для обработки ошибок при работе с базой данных
    internal class DatabaseOperationException : Exception
    {
        public DatabaseOperationException() : base() { }

        public DatabaseOperationException(string message) : base(message) { }

        public DatabaseOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
