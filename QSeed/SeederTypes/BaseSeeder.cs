using QSeed.Config;
using QSeed.Model;
using QSeed.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.SeederTypes
{
    public abstract class BaseSeeder : HasActuator
    {
        public abstract void Run();

        public IRepository<T> GetRepository<T>() => _actuator.GetRepositoryInstance<T>();

        public ModelFactory<T> GetModelFactory<T>() => _actuator.GetModalFactoryInstance<T>();
    }
}
