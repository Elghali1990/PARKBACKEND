using chrep.helpers.park.Constants;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace chrep.core.park.Interfaces
{
    public interface IDataHelper<Tabele> where Tabele : class
    {
        Task<Tabele> getByIdAsync(int Id);
        Task<List<Tabele>> GetAllAsync();
        Task<List<Tabele>> GetAllWithOptionAsync(Expression<Func<Tabele, bool>> predicate);
        Task<List<Tabele>> GetAllAsync(string[] includes = null);
        Task<Tabele> FindAsync(Expression<Func<Tabele, bool>> predicate);
        Task<Tabele> FindAsync(Expression<Func<Tabele, bool>> predicate, string[] includes = null);
        Task<IEnumerable<Tabele>> FindAsyncAll(Expression<Func<Tabele, bool>> predicate, string[] includes = null);
        Task<IEnumerable<Tabele>> FindAsyncAll(Expression<Func<Tabele, bool>> predicate, int take, int skip, string[] includes = null);
        Task<IEnumerable<Tabele>> FindAsyncAll(Expression<Func<Tabele, bool>> predicate, int? take, int? skip, Expression<Func<Tabele, object>> orderBy, string orderByDirection = OrderBy.Descending);
        Task<Tabele> Update(Tabele entitie);
        Task<Tabele> AddAsync(Tabele tabele);
        Task<Tabele> DeleteAsync(Tabele tabele);
        Task<IEnumerable<Tabele>> AddRangeAsync(IEnumerable<Tabele> table);
    }
}
