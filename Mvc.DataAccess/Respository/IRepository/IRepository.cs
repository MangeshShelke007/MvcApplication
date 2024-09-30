using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.DataAccess.Respository.IRepository
{
    public interface IRepository<T> where T: class
    {
        //T-Category
        IEnumerable<T> GetAll(string? includeProperties);
        T Get(Expression<Func<T, bool>> filter,string? includeProperties);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
}
