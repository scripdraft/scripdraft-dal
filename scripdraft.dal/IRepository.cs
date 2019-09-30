using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ScripDraft.Data
{
    public interface IRepository<T> where T : class
    {
        IMongoDatabase Database { get; set; }
        
        /// <summary>
        /// Load all entities
        /// </summary>
        /// <returns>return a list of entities</returns>
        Task<List<T>> Load();
        /// <summary>
        /// Load one entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Matching entity</returns>
        Task<T> Load(Guid id);
        /// <summary>
        /// Insert new entity or update existing one
        /// </summary>
        /// <param name="entity">New entity</param>
        Task Insert(T entity);
        /// <summary>
        /// Delete entity by id
        /// </summary>
        /// <param name="id">id of the entity to delete</param>
        Task Delete(Guid id);
        /// <summary>
        /// Update existing entity
        /// </summary>
        /// <param name="id">Id of the entity to update</param>
        /// <param name="entity">New values of the entity to update</param>
        Task Update(Guid id, T entity);
    }
}
