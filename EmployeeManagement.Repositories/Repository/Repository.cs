using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeManagement.DatabaseContext.DatabaseContext;
using EmployeeManagement.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext db = new ApplicationDbContext();


        public DbSet<T> Table
        {
            get { return db.Set<T>(); }
        }



        public virtual bool Add(T entity)
        {
            Table.Add(entity);
            return db.SaveChanges() > 0;
        }

        public virtual T GetById(int id)
        {
            return Table.Find(id);

        }
        public virtual ICollection<T> GetAll()
        {
            return Table
                .ToList();
        }
        public virtual bool Remove(T entity)
        {
            throw new NotImplementedException();
        }



        public virtual bool Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

    }
}
