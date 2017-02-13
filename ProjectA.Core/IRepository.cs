using MongoRepository;
using ProjectA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core
{
    public interface IRepository<T> where T : Entity
    {
        void Truncate();

        IList<T> Query(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = int.MaxValue);

        T Get(int id);

        void Save(T obj);

        void Delete(int id);

        void Delete(T obj);
    }
}
