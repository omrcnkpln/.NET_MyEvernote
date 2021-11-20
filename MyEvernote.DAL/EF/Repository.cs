using MyEvernote.DAL;
using MyEvernote.DAL.Abstract;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DAL.EF
{
    //gidip de generik parametre yi int yapmasın diye where
    //Repository pattern
    //Singleton pattern

    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        //bunu RepositoryBase deki static metodum ile new ledim
        //static metodum ile - singleton patttern var bu new e gerek yok artık
        //private DatabaseContext db = new DatabaseContext();
        //private DatabaseContext db;
        private DbSet<T> _objectSet;

        public Repository()
        {
            //db = RepositoryBase.CreateContext();
            _objectSet = context.Set<T>();
        }

        //OrderedParallelQuery olmayan list
        public List<T> List()
        {
            return _objectSet.ToList();
        }

        //slq order olan list
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            //bunlar zaten bütün girişlerde aynı olacak yerler bu tür için
            if(obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUserName = "system";
            }

            return Save();
        }

        public int Update(T obj)
        {
            //bunlar zaten bütün girişlerde aynı olacak yerler bu tür için
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUserName = "system";
            }

            return Save();
        }

        public int Delete(T obj)
        {
            //bunlar zaten bütün girişlerde aynı olacak yerler bu tür için
            // eğer silme işleminde pasife alma yapılacaksa bu modified için uygundur

            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;

            //    o.ModifiedOn = DateTime.Now;
            //    o.ModifiedUserName = "system";
            //}

            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
