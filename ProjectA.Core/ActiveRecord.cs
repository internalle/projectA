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
using System.Reflection;

namespace ProjectA.Core
{
    public abstract class ActiveRecord<T> : Entity where T : ActiveRecord<T>
    {
        private static IRepository<T> Repository => ServiceLocator.Current.GetInstance<IRepository<T>>();
        
        public static IList<T> Query(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = int.MaxValue)
        {
            return Repository.Query(predicate, skip, take);
        }

        public static T Get(int id)
        {
            return Repository.Get(id) as T;
        }

        public static void Delete(int id)
        {
            Repository.Delete(id);
        }

        public static void Truncate()
        {
            Repository.Truncate();
        }
        
        public virtual void Delete()
        {
            Repository.Delete(this as T);
        }

        public virtual void Save()
        {
            Repository.Save(this as T);
        }
    }
}
