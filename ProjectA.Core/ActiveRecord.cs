using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Core.Features.Measurements;
using ProjectA.Core;

namespace ProjectA.Core
{
    public abstract class ActiveRecord<T> : Entity where T : Entity
    {
        protected static IRepository<T> _repo => ServiceLocator.Current.GetInstance<IRepository<T>>();

        public static IList<T> Query(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = int.MaxValue)
        {
            return _repo.Query(predicate, skip, take);
        }

        public static T Get(int id)
        {
            return _repo.Get(id);
        }

        public static void Delete(int id)
        {
            _repo.Delete(id);
        }

        public static void Truncate()
        {
            _repo.Truncate();
        }
        
        public virtual void Delete()
        {
            _repo.Delete(this as T);
        }

        public virtual void Save()
        {
            _repo.Save(this as T);
        }
    }
}
