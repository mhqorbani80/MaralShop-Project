﻿using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IRepository<TKey,T> where T : class
    {
        T GetBy(TKey id);
        List<T> GetAll();
        void Create(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
        void Save();
    }
}