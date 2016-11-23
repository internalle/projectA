using ProjectA.Core.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core
{
    public abstract class ActiveRecord<T> : Entity where T : Entity
    {
        protected static MongoRepository<T> Repository
        {
            get
            {
                return new MongoRepository<T>(ConfigurationManager.AppSettings["MongoDBConnectionString"], typeof(T).Name);
            }
        }

        public static IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return Repository.Where(predicate);
        }

        public static T Get(string id)
        {
            return Repository.GetById(id);
        }

        public void Save()
        {
            CheckPropertyConstrains(this as T);
            if (Repository.Any(x => x.Id == this.Id))
            {
                Repository.Update(this as T);
            }
            else {
                Repository.Add(this as T);
            }
        }

        public void Delete()
        {
            Repository.Delete(this as T);
        }

        public static void Delete(string id)
        {
            Repository.Delete(id);
        }

        private bool CheckPropertyConstrains(T obj)
        {
            var required = typeof(T).GetProperties().Where(x => x.GetCustomAttributes(true).OfType<RequiredAttribute>().Any());

            foreach (var req in required)
            {
                if (req.GetValue(obj) == null)
                {
                    throw new RequeiredPropertyException();
                }
            }
            return true;
        }
    }
}
