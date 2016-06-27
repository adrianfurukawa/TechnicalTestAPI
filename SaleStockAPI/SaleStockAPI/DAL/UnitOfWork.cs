using System;
using SaleStockAPI.Models;

namespace SaleStockAPI.DAL
{
    public class UnitOfWork : IDisposable
    {
        private SaleStockEntities context = new SaleStockEntities();
        private GenericRepository<MsProduct> productRepository;
        private GenericRepository<MsCategory> categoryRepository;

        public GenericRepository<MsProduct> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<MsProduct>(context);
                }
                return productRepository;
            }
        }

        public GenericRepository<MsCategory> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<MsCategory>(context);
                }
                return categoryRepository;
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