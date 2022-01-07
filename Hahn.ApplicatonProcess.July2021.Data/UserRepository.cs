using Hahn.ApplicatonProcess.July2021.Domain;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Data
{
    /// <summary>
    /// RepositoryPattern
    /// </summary>
    public class UserRepository : IRepository<User>, IDisposable
    {
        private AppContext context;

        public UserRepository(AppContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<User> GetItemsList()
        {
            return context.Users.ToList();
        }

        public User GetItem(long id)
        {
            return context.Users.Find(id);
        }

        public void Create(User item)
        {
            context.Users.Add(item);
        }

        public void Update(User item)
        {
            var attachedEntity = context.Users.Local.FirstOrDefault(x => x.Id == item.Id);
            if (attachedEntity != null)
            {
                var attachedEntry = context.Entry(attachedEntity);
                attachedEntry.CurrentValues.SetValues(item);
            }


            /*          if (context.Entry(item).State == EntityState.Detached)
                      {
                          context.Users.Attach(item);
                      }

                      var x = context.Entry(item);
                      //context.Users.Attach(item);
                      //context.Entry(item).State = EntityState.Modified;
                      x.State = EntityState.Modified; */
        }

        public void Delete(long id)
        {
            var item = context.Users.Find(id);
            if (item != null)
                context.Users.Remove(item);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
