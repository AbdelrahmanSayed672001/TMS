
using Microsoft.EntityFrameworkCore;

namespace TMS_V1.Interfaces
{
    /// <summary>
    /// Provides a generic repository interface for performing CRUD operations and querying entities 
    /// of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the entity to be managed by this repository.</typeparam>
    public interface IBaseRepo<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains a collection of entities.
        /// </returns>
        Task<IEnumerable<T>> GetAll();


        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> with related entities specified 
        /// in the <paramref name="include"/> array.
        /// </summary>
        /// <param name="include">An optional array of related entities to include in the query.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains a collection of entities.
        /// </returns>
        Task<IEnumerable<T>> GetAll(string[] include = null);


        /// <summary>
        /// Retrieves entities of type <typeparamref name="T"/> that match the given 
        /// <paramref name="match"/> expression and optionally includes related entities.
        /// </summary>
        /// <param name="match">The expression used to filter the entities.</param>
        /// <param name="include">An optional array of related entities to include in the query.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains a collection of filtered entities.
        /// </returns>
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> match, string[] include = null);


        /// <summary>
        /// Retrieves filtered entities of type <typeparamref name="T"/> based on the provided 
        /// <paramref name="match"/> expression and optionally includes related entities.
        /// </summary>
        /// <param name="match">The expression used to filter the entities.</param>
        /// <param name="include">An optional array of related entities to include in the query.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. The task result contains 
        ///     a collection of filtered entities.
        /// </returns>
        Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>> match, string[] include = null);


        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains the entity if found.
        /// </returns>
        Task<T> GetById(int id);


        /// <summary>
        /// Retrieves the total count of entities of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains the total number of entities.
        /// </returns>
        Task<int> GetCount();


        /// <summary>
        /// Retrieves the count of entities of type <typeparamref name="T"/> 
        /// that match the given <paramref name="match"/> expression.
        /// </summary>
        /// <param name="match">The expression used to filter the entities.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains the number of matching entities.
        /// </returns>
        Task<int> GetCount(Expression<Func<T, bool>> match);


        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> that matches the given 
        /// <paramref name="match"/> expression and optionally includes related entities.
        /// </summary>
        /// <param name="match">The expression used to filter the entity.</param>
        /// <param name="include">An optional array of related entities to include in the query.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains the entity if found.
        /// </returns>
        Task<T> GetById(Expression<Func<T, bool>> match, string[] include = null);


        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> by a specific expression, 
        /// such as its name or other unique property.
        /// </summary>
        /// <param name="match">The expression used to filter the entity by a specific attribute.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains the entity if found.
        /// </returns>
        Task<T> GetByName(Expression<Func<T, bool>> match);


        /// <summary>
        /// Adds a new entity of type <typeparamref name="T"/> to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains the added entity.
        /// </returns>
        Task<T> Add(T entity);


        /// <summary>
        /// Updates an existing entity of type <typeparamref name="T"/> in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>
        ///     A task representing the asynchronous operation. 
        ///     The task result contains the updated entity.
        /// </returns>
        Task<T> Update(T entity);


        /// <summary>
        /// Deletes an entity of type <typeparamref name="T"/> from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Delete(T entity);


        /// <summary>
        /// Deletes a collection of entities of type <typeparamref name="T"/> from the database.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Delete(IEnumerable<T> entities);

    }

}
