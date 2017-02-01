using QSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ProjectA.Core;
using QSeed.Repository;
using Microsoft.Practices.ServiceLocation;

namespace ProjectA.Database
{
    public class Repository<T> : QSeed.Repository.IRepository<T> where T : Entity
    {
        private Core.IRepository<T> _repo => ServiceLocator.Current.GetInstance<Core.IRepository<T>>();

        public void Save(T obj)
        {
            _repo.Save(obj);
        }

        public void ClearData()
        {
            _repo.Truncate();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> expression)
        {
            return _repo.Query(expression);
        }

        public void Remove(T obj)
        {
            _repo.Delete(obj);
        }
    }
}
