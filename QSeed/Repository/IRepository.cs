using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.Repository
{
    public interface IRepository<T>
    {
        void ClearData();

        IEnumerable<T> Query(Expression<Func<T, bool>> expression);

        void Save(T obj);
        
        void Remove(T obj);
    }
}
