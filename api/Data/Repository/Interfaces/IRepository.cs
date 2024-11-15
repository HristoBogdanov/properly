using System.Linq.Expressions;

namespace api.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);
        TType FirstOrDefault(Func<TType, bool> predicate);
        Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);
        IEnumerable<TType> GetAll();
        Task<IEnumerable<TType>> GetAllAsync();
        IQueryable<TType> GetAllAttached();
        bool Contains(Expression<Func<TType, bool>> predicate);
        Task<bool> ContainsAsync(Expression<Func<TType, bool>> predicate);
        void Add(TType item);
        Task AddAsync(TType item);
        void AddRange(List<TType> items);
        Task AddRangeAsync(List<TType> items);
        bool Delete(TType entity);
        Task<bool> DeleteAsync(TType entity);
        bool SoftDelete(TType entity);
        Task<bool> SoftDeleteAsync(TType entity);
        bool Update(TType item);
        Task<bool> UpdateAsync(TType item);
        int Count();
        Task<int> CountAsync();
    }
}