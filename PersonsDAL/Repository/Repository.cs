using Microsoft.EntityFrameworkCore;
using PersonsDAL.Data;
using PersonsDAL.Entities;
using PersonsDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Repository
{
    //public class Repository<TEntity> : AbstractRepository, IRepository<TEntity> where TEntity : BaseEntity
    //{
    //    protected readonly DbSet<TEntity> dbSet;

    //    public Repository(AppDbContext context) : base(context)
    //    {
    //        dbSet = context.Set<TEntity>();
    //    }

    //    public IEnumerable<TEntity> GetAll()
    //    {
    //        return dbSet.ToList();
    //    }

    //    public IEnumerable<TEntity> GetAll(int pageNumber, int rowCount)
    //    {
    //        return dbSet.Skip((pageNumber - 1) * rowCount).Take(rowCount).ToList();
    //    }

    //    public TEntity GetById(int id)
    //    {
    //        return dbSet.Find(id);
    //    }

    //    public void Add(TEntity entity)
    //    {
    //        dbSet.Add(entity);
    //        context.SaveChanges();
    //    }

    //    public void Update(TEntity entity)
    //    {
    //        dbSet.Update(entity);
    //        context.SaveChanges();
    //    }

    //    public void Delete(TEntity entity)
    //    {
    //        dbSet.Remove(entity);
    //        context.SaveChanges();
    //    }

    //    public virtual void DeleteById(int id)
    //    {
    //        var entity = dbSet.Find(id);
    //        if (entity != null)
    //        {
    //            dbSet.Remove(entity);
    //            context.SaveChanges();
    //        }
    //    }
    //}
}
