using QSeed.Config;
using QSeed.Model;
using QSeed.Extensions;
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
    public class FactoryAndSeederFactory
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
            var genericType = ModelFactoryTypes.FirstOrDefault(x => x.IsModelFactory<T>());
            var instance = Activator.CreateInstance(genericType) as ModelFactory<T>;
            instance.SetFactory(this);
            return instance;
        }

        public MasterSeeder GetMasterSeederInstance()
        {
            var instance = Activator.CreateInstance(MasterSeederType) as MasterSeeder;
            instance.SetFactory(this);
            return instance;
        }

        public IEnumerable<BaseSeeder> GetAllSeederInstances()
        {
            foreach (var seederType in SeederTypes)
            {
                var instance = Activator.CreateInstance(seederType) as BaseSeeder;
                instance.SetFactory(this);
                yield return instance;
            }
        }

        internal static FactoryAndSeederFactory FromConfiguration(Configuration conf)
        {
            var factory = new FactoryAndSeederFactory
            {
                RepositoryType = conf.Repository,
                MasterSeederType = conf.MasterSeeder,
                SeederTypes = conf.BaseSeeders,
                ModelFactoryTypes = conf.ModalFactories

            };

            factory.RepositoryType = ForceImplementation(factory.RepositoryType, default(Type), () => {
                return factory.RepositoryType.IsRepository();
            });

            factory.MasterSeederType = ForceImplementation(factory.MasterSeederType, typeof(DefaultMasterSeeder), () => {
                return factory.MasterSeederType.IsMasterSeeder();
            });

            return factory;
        }

        private static Type ForceImplementation(Type registeredType, Type defaultAssign, Func<bool> expectedImplementationCheck)
        {
            if (registeredType == default(Type))
            {
                if (defaultAssign != default(Type))
                {
                    registeredType = defaultAssign;
                }
                else
                {
                    throw new NullReferenceException($"{defaultAssign.FullName} implementation expected");
                }
            }
            else if (!expectedImplementationCheck.Invoke())
            {
                throw new NullReferenceException($"{defaultAssign.FullName} implementation expected");
            }

            return registeredType;
        }
    }
}
