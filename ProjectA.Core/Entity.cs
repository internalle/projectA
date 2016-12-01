using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace ProjectA.Core
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
    }
}
