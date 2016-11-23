using ProjectA.Core.Attributes;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Features.Microservice
{
    public class Microservice : ActiveRecord<Microservice>
    {
        public Microservice(string name)
        {
            Name = name;

            Instances = new List<Instance>();
            Middlewares = new List<Middleware>();
            Configurables = new List<Configurable>();
        }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Instance> Instances { get; set; }

        public IEnumerable<Middleware> Middlewares { get; set; }

        public IEnumerable<Configurable> Configurables { get; set; }
    }
}
