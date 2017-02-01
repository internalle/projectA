using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NHibernate;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Core;

namespace ProjectA.Configuration.MySQL.Repository
{
    public class MySQLRepository<T> : IRepository<T> where T : Entity
    {
        protected static ISession _session => ServiceLocator.Current.GetInstance<ISession>();

        public IList<T> Query(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = int.MaxValue)
        {
            if (predicate != null)
                return _session.QueryOver<T>().Where(predicate).Skip(skip).Take(take).List<T>();
            else
                return _session.QueryOver<T>().List<T>();
        }

        public T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public void Save(T obj)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(obj);
                transaction.Commit();
            }
        }

        public void Truncate()
        {
            var results = _session.QueryOver<T>().Take(100).List();
            while (results.Any())
            {
                using (var transaction = _session.BeginTransaction())
                {
                    foreach (var result in results)
                    {
                        _session.Delete(result);
                    }
                    transaction.Commit();
                }
                results = _session.QueryOver<T>().Take(100).List();
            }
        }

        public void Delete(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var obj = _session.Get<T>(id);
                _session.Delete(obj);
                transaction.Commit();
            }
        }

        public void Delete(T obj)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(obj);
                transaction.Commit();
            }
        }
    }
}
