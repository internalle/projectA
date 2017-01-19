using QSeed.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.Model
{
    public abstract class ModelFactory<T> : HasActuator
    {
        protected abstract T Build();

        public T CreateInstance()
        {
            return Build();
        }

        public IEnumerable<T> CreateInstances(int count)
        {
            List<T> results = new List<T>();
            for (var i = 0; i < count; i++)
            {
                results.Add(CreateInstance());
            }

            return results;
        }

        public T PersistInstance()
        {
            T instance = CreateInstance();
            _actuator.GetRepositoryInstance<T>().Save(instance);
            return instance;
        }

        public IEnumerable<T> PersistInstances(int count)
        {
            List<T> results = new List<T>();
            for (var i = 0; i < count; i++)
            {
                results.Add(PersistInstance());
            }

            return results;
        }
    }
}
