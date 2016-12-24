using ProjectA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Seeders
{
    public class BaseSeeder<T> where T : ActiveRecord<T>
    {
        public SeederBuilder<T> Builder => new SeederBuilder<T>();
    }

    public class SeederBuilder<T> where T : ActiveRecord<T>
    {
        private bool _supressExceptions = false;

        public void ShowExceptions()
        {
            _supressExceptions = false;
        }

        public void HideExceptions()
        {
            _supressExceptions = true;
        }

        public SeederBuilder<T> ClearData()
        {
            ActiveRecord<T>.Truncate();
            return this;
        }

        public SeederBuilder<T> Add(T obj)
        {
            obj.Save();
            return this;
        }

        public SeederBuilder<T> Update(T obj, Expression<Func<T, bool>> expression)
        {
            var old = ActiveRecord<T>.Query(expression).SingleOrDefault();

            if (old == null)
            {
                if (_supressExceptions)
                {
                    return this;
                }

                throw new NullReferenceException();
            }

            old.Delete();
            obj.Save();

            return this;
        }

        public SeederBuilder<T> UpdateOrAdd(T obj, Expression<Func<T, bool>> expression)
        {
            try
            {
                Update(obj, expression);
            }
            catch
            {
                Add(obj);
            }

            return this;
        }

        public SeederBuilder<T> Remove(Expression<Func<T, bool>> expression)
        {
            var old = ActiveRecord<T>.Query(expression).SingleOrDefault();

            if (old == null)
            {
                if (_supressExceptions)
                {
                    return this;
                }
                throw new NullReferenceException();
            }

            old.Delete();

            return this;
        }
    }
}
