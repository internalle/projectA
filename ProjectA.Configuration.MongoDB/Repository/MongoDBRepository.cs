using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoRepository;
using ProjectA.Configuration.Mongo.Repository;
using ProjectA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.Mongo.Repository
{
    public class MongoRecordWrapper<T> : IEntity<int> where T : ActiveRecord<T>
    {
        private static int ID = 0;

        public MongoRecordWrapper(T data)
        {
            Data = data;
            Id = data.Id != default(int) ? data.Id : ID++;
            data.Id = Id;
        }

        public T Data { get; set; }
        
        public int Id { get; set; }
    }

    public class MongoDBRepository<T> : Core.IRepository<T> where T : ActiveRecord<T>
    {
        protected MongoRepository<MongoRecordWrapper<T>, int> _repository;

        public MongoDBRepository(MongoUrl url)
        {
            _repository = new MongoRepository<MongoRecordWrapper<T>, int>(url, typeof(T).FullName);
        }

        public void Delete(T obj)
        {
            _repository.Delete(new MongoRecordWrapper<T>(obj));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public T Get(int id)
        {
            return _repository.GetById(id).Data;
        }

        public IList<T> Query(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = int.MaxValue)
        {
            return _repository.Where(x => true).Skip(skip).Take(take).Select(x => x.Data).ToList();
        }

        public void Save(T obj)
        {
            var wrappedObj = new MongoRecordWrapper<T>(obj);

            if (wrappedObj.Id == default(int))
            {
                _repository.Add(wrappedObj);
            }
            else
            {
                _repository.Update(wrappedObj);
            }

            wrappedObj.Data.Id = wrappedObj.Id;
        }

        public void Truncate()
        {
            _repository.DeleteAll();
        }
    }
}
