using System;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.July2021.Domain
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetItemsList(); // получение всех объектов
        T GetItem(long id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(long id); // удаление объекта по id
    }
}
