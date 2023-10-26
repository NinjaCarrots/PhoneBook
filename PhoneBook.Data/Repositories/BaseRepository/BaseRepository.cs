using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Domain;
using PhoneBook.Core.Models;
using PhoneBook.Data.Context;
using PhoneBook.Data.Repositories.BaseInterfaces;

namespace PhoneBook.Data.Repositories.BaseRepository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dataContext;

        public BaseRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> Get(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dataContext.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<bool> Insert(T entity)
        {
            _dataContext.Set<T>().Add(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<T>> List()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> Pagination(int page, int pageSize)
        {
            return await _dataContext.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<bool> Update(T entity)
        {
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
