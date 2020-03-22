using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected RepairSystemContext Context { get; set; }
        public GenericRepository( RepairSystemContext ctx)
        {
            this.Context = ctx;
        }

        public void Add( TEntity entity )
        {
            this.Context.Set<TEntity>().Add( entity );
        }

        public void AddRange( IEnumerable<TEntity> entities )
        {
            this.Context.Set<TEntity>().AddRange( entities );
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>().ToList();
        }

        public void Remove( TEntity entity )
        {
            this.Context.Set<TEntity>().Remove( entity );
        }

        public void RemoveRange( IEnumerable<TEntity> entities )
        {
            this.Context.Set<TEntity>().RemoveRange( entities );
        }
    }
}
