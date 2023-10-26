using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Repositories.BaseInterfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IList<T>> List();
        Task<bool> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<IList<T>> Pagination(int page, int pageSize);
    }
}
