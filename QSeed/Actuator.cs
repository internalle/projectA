using QSeed.Config;
using QSeed.Model;
using QSeed.Repository;
using QSeed.SeederTypes;
using QSeed.SeederTypes.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSeed
{
    public class Actuator
    {
        internal Type RepositoryType { get; set; }

        internal Type MasterSeederType { get; set; }

        internal IEnumerable<Type> SeederTypes { get; set; }

        internal IEnumerable<Type> ModelFactoryTypes { get; set; }

        public IRepository<T> GetRepositoryInstance<T>()
        {
            var genericType = RepositoryType.MakeGenericType(typeof(T));
            return Activator.CreateInstance(genericType) as IRepository<T>;
        }

        public ModelFactory<T> GetModalFactoryInstance<T>()
        {
            var genericType = ModelFactoryTypes.FirstOrDefault(x=>x.IsSubclassOf(typeof(ModelFactory<T>)));
            var instance = Activator.CreateInstance(genericType) as ModelFactory<T>;
            instance.SetActuator(this);
            return instance;
        }

        public MasterSeeder GetMasterSeederInstance()
        {
            var instance = Activator.CreateInstance(MasterSeederType) as MasterSeeder;
            instance.SetActuator(this);
            return instance;
        }

        public IEnumerable<BaseSeeder> GetAllSeederInstances()
        {
            foreach(var seederType in SeederTypes)
            {
                var instance = Activator.CreateInstance(seederType) as BaseSeeder;
                instance.SetActuator(this);
                yield return instance;
            }
        }

        internal static Actuator FromConfiguration(Configuration conf)
        {
            var actuator = new Actuator
            {
                RepositoryType = conf.Repository,
                MasterSeederType = conf.MasterSeeder,
                SeederTypes = conf.BaseSeeders,
                ModelFactoryTypes = conf.ModalFactories

            };

            if (actuator.RepositoryType == default(Type))
            {
                throw new NullReferenceException("Repository implementation expected");
            }
            else if (actuator.RepositoryType.GetInterface(typeof(IRepository<>).Name) == default(Type))
            {
                throw new InvalidCastException("IRepository<T> implementation expected");
            }

            if (actuator.MasterSeederType == default(Type))
            {
                actuator.MasterSeederType = typeof(DefaultMasterSeeder);
            }
            else if (actuator.MasterSeederType.BaseType != typeof(MasterSeeder))
            {
                throw new InvalidCastException("MasterSeeder implementation expected");
            }

            return actuator;
        }
    }
}
