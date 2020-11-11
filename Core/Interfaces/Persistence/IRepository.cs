﻿using Ardalis.Specification;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        ///     Get items given a string SQL query directly.
        ///     Likely in production, you may want to use alternatives like Parameterized Query or LINQ to avoid SQL Injection and avoid having to work with strings directly.
        ///     This is kept here for demonstration purpose.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetItemsAsync(string query);
        /// <summary>
        ///     Get items given a specification.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetItemsAsync(ISpecification<T> specification);
        Task<T> GetItemAsync(Guid id);
        Task AddItemAsync(T item);
        Task UpdateItemAsync(Guid id, T item);
        Task DeleteItemAsync(Guid id);
    }
}
