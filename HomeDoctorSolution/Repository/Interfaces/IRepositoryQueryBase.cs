using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using HomeDoctorSolution.Util.Entities;

namespace HomeDoctorSolution.Repository.Interfaces
{
    /// <summary>
    /// Interface repository base
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="K">Type of id column</typeparam>
    /// <typeparam name="TContext">DbContext</typeparam>
    public interface IRepositoryQueryBase<T, K, TContext> where T : EntityBase<K> where TContext : DbContext
    {
        IQueryable<T> GetAll(bool trackChanges = false);
        Task<List<T>> GetAllAsync(bool trackChanges = false);
        IQueryable<T> GetAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAllAsync(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        Task<int> CountByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        Task<T?> GetByIdAsync(K id);
        Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    }
    /// <summary>
    /// Interface repository base
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="K">Type of id column</typeparam>
    /// <typeparam name="TContext">DbContext</typeparam>
    public interface IRepositoryBaseAsync<T, K, TContext> : IRepositoryQueryBase<T, K, TContext> where T : EntityBase<K> where TContext : DbContext
    {
        Task<K> CreateAsync(T entity);
        Task<IList<K>> CreateListAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateListAsync(IEnumerable<T> entities);
        Task<bool> SoftDeleteAsync(K id);
        Task HardDeleteAsync(K id);
        Task<bool> SoftDeleteListAsync(IEnumerable<K> ids);
        Task HardDeleteListAsync(IEnumerable<K> ids);
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task EndTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
