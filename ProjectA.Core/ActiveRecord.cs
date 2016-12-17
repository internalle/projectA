using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace ProjectA.Core
{
    public abstract class ActiveRecord<T> : Entity where T : Entity
    {
        protected static ISession _session => ServiceLocator.Current.GetInstance<ISession>();

        public static IList<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _session.QueryOver<T>().Where(predicate).List<T>();
        }

        public static IList<T> List()
        {
            return _session.QueryOver<T>().List<T>();
        }

        public static T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public virtual void Save()
        {
            _session.SaveOrUpdate(this);
        }

        public virtual void Delete()
        {
            _session.Delete(this as T);
        }

        public static void Delete(int id)
        {
            _session.Delete(id);
        }
    }
}
