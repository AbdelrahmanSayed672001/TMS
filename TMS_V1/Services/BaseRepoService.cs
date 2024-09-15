
namespace TMS_V1.Services
{
    /// <summary>
    /// A generic repository service for managing CRUD operations with entities of type <typeparamref name="T"/>.
    /// Implements the <see cref="IBaseRepo{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The entity type for the repository, constrained to reference types.</typeparam>
    public class BaseRepoService<T> : IBaseRepo<T> where T : class
    {
        private ApplicationDbContext _dbContext;

        public BaseRepoService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Builds a queryable object that includes related tables for the specified entity type.
        /// </summary>
        /// <param name="include">An optional array of related tables to include in the query.</param>
        /// <returns>An <see cref="IQueryable{T}"/> representing the base query with related tables.</returns>
        private IQueryable<T> includes(string[] include = null)
        {
            IQueryable<T> q = _dbContext.Set<T>();

            if (include != null)
            {
                foreach (var includeValue in include)
                    q = q.Include(includeValue);
            }

            return q;
        }

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> with optional related table inclusion.
        /// </summary>
        /// <param name="include">An optional array of related tables to include in the query.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of entities.</returns>
        public async Task<IEnumerable<T>> GetAll(string[] include = null)
        {
            var q = includes(include);

            return await q.ToListAsync();
        }


        /// <summary>
        /// Retrieves all entities that match the specified condition with optional related table inclusion.
        /// </summary>
        /// <param name="match">An expression that defines the condition to match entities.</param>
        /// <param name="include">An optional array of related tables to include in the query.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of matching entities.</returns>
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> match, string[] include = null)
        {
            var q = includes(include);

            return await q.Where(match).ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
        public async Task<T> GetById(int id) => await _dbContext.Set<T>().FindAsync(id);


        /// <summary>
        /// Retrieves the total count of entities of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A task that represents an asynchronous operation. The task result contains the total count of entities.</returns>
        public async Task<int> GetCount() => await _dbContext.Set<T>().CountAsync();


        /// <summary>
        /// Retrieves the count of entities that match the specified condition.
        /// </summary>
        /// <param name="match">An expression that defines the condition to match entities.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains the count of matching entities.</returns>
        public async Task<int> GetCount(Expression<Func<T, bool>> match) => await _dbContext.Set<T>().Where(match).CountAsync();



        /// <summary>
        /// Retrieves the first entity that matches the specified condition with optional related table inclusion.
        /// </summary>
        /// <param name="match">An expression that defines the condition to match entities.</param>
        /// <param name="include">An optional array of related tables to include in the query.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains the first matching entity if found; otherwise, null.</returns>
        public async Task<T> GetById(Expression<Func<T, bool>> match, string[] include = null)
        {
            var q = includes(include);
            return await q.FirstOrDefaultAsync(match);
        }


        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains the added entity.</returns>
        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }


        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Deletes a range of entities from the database.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        public async Task Delete(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A task that represents an asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of entities.</returns>
        public async Task<IEnumerable<T>> GetAll()
        {

            return await _dbContext.Set<T>().ToListAsync();
        }


        /// <summary>
        /// Retrieves all entities that match the specified condition with optional related table inclusion.
        /// </summary>
        /// <param name="match">An expression that defines the condition to match entities.</param>
        /// <param name="include">An optional array of related tables to include in the query.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of matching entities.</returns>
        public async Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>> match, string[] include = null)
        {
            var q = includes(include);

            return await q.Where(match).ToListAsync();
        }


        /// <summary>
        /// Retrieves the first entity that matches the specified name condition.
        /// </summary>
        /// <param name="match">An expression that defines the condition to match by name.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains the matching entity if found; otherwise, null.</returns>
        public async Task<T> GetByName(Expression<Func<T, bool>> match) =>
            await _dbContext.Set<T>().FirstOrDefaultAsync(match);



        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents an asynchronous operation. The task result contains the updated entity.</returns>
        public async Task<T> Update(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
