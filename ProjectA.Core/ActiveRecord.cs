using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Microsoft.Practices.ServiceLocation;
using ProjectA.Core.Features.Measurements;

namespace ProjectA.Core
{
    public abstract class ActiveRecord<T> : Entity where T : Entity
    {
        protected static ISession _session => ServiceLocator.Current.GetInstance<ISession>();

        public static IList<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _session.QueryOver<T>().Where(predicate).List<T>();
        }

        public static IList<T> List(int skip = 0, int take = int.MaxValue)
        {
            return _session.QueryOver<T>().Skip(skip).Take(take).List<T>();
        }

        public static T Get(int id)
        {
            return _session.Get<T>(id);
        }

        public static void Delete(int id)
        {
            var obj = Get(id) as ActiveRecord<T>;
            obj.Delete();
        }

        public static void Truncate()
        {
            var session = _session;

            var results = session.QueryOver<T>().Take(100).List();
            while (results.Any())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var result in results)
                    {
                        session.Delete(result);
                    }
                    transaction.Commit();
                }
                results = session.QueryOver<T>().Take(100).List();
            }
        }



        public virtual void Delete()
        {
            var session = _session;
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(this);
                transaction.Commit();
            }
        }

        public virtual void Save()
        {
            var session = _session;
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(this);
                transaction.Commit();
            }
        }
    }
}
