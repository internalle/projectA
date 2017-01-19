using QSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ProjectA.Core;
using QSeed.Repository;

namespace ProjectA.Database
{
    public class Repository<T> : IRepository<T> where T : ActiveRecord<T>
    {
        public void Save(T obj)
        {
            obj.Save();
        }

        public void ClearData()
        {
            ActiveRecord<T>.Truncate();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> expression)
        {
            return ActiveRecord<T>.Query(expression);
        }

        public void Remove(T obj)
        {
            obj.Delete();
        }
    }
}
