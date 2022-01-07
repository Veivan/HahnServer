using Hahn.ApplicatonProcess.July2021.Domain;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System;

namespace Hahn.ApplicatonProcess.July2021.Data
{
    public class UnitOfWork : IDisposable
    {
        private AppContext context = new AppContext();
        private IRepository<User> userRepository;

        //        private GenericRepository<Asset> assetRepository;
        //        private GenericRepository<User> userRepository;

        public UnitOfWork()
        {
        }

/*        public GenericRepository<Asset> AssetRepository
        {
            get
            {
                if (this.assetRepository == null)
                {
                    this.assetRepository = new GenericRepository<Asset>(context);
                }
                return assetRepository;
            }
        } */

        public IRepository<User> UserRepository
        //public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                    //this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
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
