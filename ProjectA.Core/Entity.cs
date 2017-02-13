using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using MongoRepository;

namespace ProjectA.Core
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
    }
}
