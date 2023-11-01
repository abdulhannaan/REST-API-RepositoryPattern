using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMS.Data.Repositories
{
	/// <summary>
	/// Generic base repository for database operations.
	/// </summary>
	/// <typeparam name="T">The entity type.</typeparam>
	public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly EMSContext _dbContext;

        public BaseRepository(EMSContext dbContext)
        {
            _dbContext = dbContext;
        }

		/// <summary>
		/// Gets an entity by its ID.
		/// </summary>
		/// <param name="id">The ID of the entity.</param>
		/// <returns>The entity with the specified ID.</returns>
		public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

		/// <summary>
		/// Gets all entities optionally filtered by a condition.
		/// </summary>
		/// <param name="filter">The condition to filter entities.</param>
		/// <returns>A list of entities.</returns>
		public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            var result = _dbContext.Set<T>().AsQueryable();
            if (filter != null) result = result.Where(filter);
            return await result.ToListAsync();
        }

		/// <summary>
		/// Adds a new entity to the database.
		/// </summary>
		/// <param name="entity">The entity to be added.</param>
		/// <returns>The added entity.</returns>
		public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

		/// <summary>
		/// Adds a range of entities to the database.
		/// </summary>
		/// <param name="entities">The list of entities to be added.</param>
		/// <returns>True if the entities were added successfully; otherwise, false.</returns>
		public virtual async Task<bool> AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }

		/// <summary>
		/// Updates an existing entity in the database.
		/// </summary>
		/// <param name="entity">The entity to be updated.</param>
		/// <returns>The updated entity.</returns>
		public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

		/// <summary>
		/// Marks an entity as deleted in a soft delete scenario.
		/// </summary>
		/// <param name="id">The ID of the entity to delete.</param>
		/// <param name="loggedInUser">The ID of the user performing the delete action.</param>
		public virtual async Task DeleteAsync(int id, int loggedInUser)
        {

            var result = await _dbContext.Set<T>().FindAsync(id);
            
            if(result != null)
            { 
                var deleteProperty = result.GetType().GetProperty("IsDeleted");
                deleteProperty.SetValue(result, true);

                var deletedDateProperty = result.GetType().GetProperty("DeletedOn");
                deletedDateProperty.SetValue(result, DateTime.Now);

                var deletedByProperty = result.GetType().GetProperty("DeletedBy");
                deletedByProperty.SetValue(result, loggedInUser);

            }
            _dbContext.Set<T>().Update(result);
            await _dbContext.SaveChangesAsync();
        }

		/// <summary>
		/// Performs a hard delete of a range of entities from the database.
		/// </summary>
		/// <param name="entities">The list of entities to be hard deleted.</param>
		public virtual void HardDeleteRange(List<T> entities)
        {
            _dbContext.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

		/// <summary>
		/// Performs a hard delete of an entity from the database.
		/// </summary>
		/// <param name="entity">The entity to be hard deleted.</param>
		public virtual void HardDelete(T entity)
        {
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }
    }

}
