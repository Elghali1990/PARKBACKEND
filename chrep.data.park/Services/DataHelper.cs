using chrep.core.park.Interfaces;
using chrep.data.park.SqlServer;
using chrep.helpers.park.Constants;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace chrep.data.park.Services
{
    public class DataHelper<Table> : IDataHelper<Table> where Table : class
    {
        private readonly AppDbContext _appDbContext;
        public DataHelper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Table> AddAsync(Table tabele)
        {
            await _appDbContext.Set<Table>().AddAsync(tabele);
            return tabele;
        }

        public async Task<IEnumerable<Table>> AddRangeAsync(IEnumerable<Table> table)
        {
            _appDbContext.Set<Table>().AddRangeAsync(table);
            return table;
        }

        public async Task<Table> DeleteAsync(Table tabele)
        {
            await Task.Run(() => _appDbContext.Set<Table>().Remove(tabele));
            return tabele;
        }

        public async Task<Table> FindAsync(Expression<Func<Table, bool>> predicate)
        {
            return await _appDbContext.Set<Table>().FirstOrDefaultAsync(predicate);
        }

        public async Task<Table> FindAsync(Expression<Func<Table, bool>> predicate, string[] includes = null)
        {
            IQueryable<Table> query = _appDbContext.Set<Table>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Table>> FindAsyncAll(Expression<Func<Table, bool>> predicate, string[] includes = null)
        {
            IQueryable<Table> query = _appDbContext.Set<Table>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Table>> FindAsyncAll(Expression<Func<Table, bool>> predicate, int take, int skip, string[] includes = null)
        {
            IQueryable<Table> query = _appDbContext.Set<Table>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Table>> FindAsyncAll(Expression<Func<Table, bool>> predicate, int? take, int? skip, Expression<Func<Table, object>> orderBy, string orderByDirection = "DSEC")
        {
            IQueryable<Table> query = _appDbContext.Set<Table>().Where(predicate);
            if (take.HasValue) query = query.Take(take.Value);
            if (skip.HasValue) query = query.Skip(take.Value);
            if (orderByDirection is not null)
            {
                if (orderByDirection.Equals(OrderBy.Ascending))
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

        public async Task<List<Table>> GetAllAsync()
        {
            return await _appDbContext.Set<Table>().ToListAsync();
        }

        public async Task<List<Table>> GetAllAsync(string[] includes = null)
        {
            IQueryable<Table> query = _appDbContext.Set<Table>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<List<Table>> GetAllWithOptionAsync(Expression<Func<Table, bool>> predicate)
        {
            return await _appDbContext.Set<Table>().Where(predicate).ToListAsync();
        }

        public async Task<Table> getByIdAsync(int Id)
        {
            return await _appDbContext.Set<Table>().FindAsync(Id);
        }

        public async Task<Table> Update(Table entitie)
        {
            await Task.Run(() => _appDbContext.Set<Table>().Update(entitie));
            return entitie;
        }
    }
}
