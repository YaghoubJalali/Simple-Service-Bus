using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.DbContexts;
using Sample.UserManagement.Service.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Repository
{
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly UserManagementContext _userManagementContext;
        private DbSet<TEntity> _entities;
        public Repository(UserManagementContext dbContext)
        {
            _userManagementContext = dbContext ?? throw new ArgumentNullException($"UserManagementContext should not be null!");

            _entities = dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{entity} must not be null");
            }

            try
            {
                await _entities.AddAsync(entity);
                //await _userManagementContext.AddAsync(entity);
                await _userManagementContext.SaveChangesAsync();
                _userManagementContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"entity could not be saved: {ex.Message}");
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"entity must not be null");
            }

            try
            {
                _entities.Update(entity);

                //_userManagementContext.Update(entity);
                await _userManagementContext.SaveChangesAsync();
                _userManagementContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"entity could not be updated: {ex.Message}");
            }
        }

        //public virtual async Task Delete(Guid id)
        //{
        //    TEntity entity = await _entities.FindAsync(id);
        //    Delete(entity);
        //}

        //public virtual void Delete(TEntity entity)
        //{
        //    if (_userManagementContext.Entry(entity).State == EntityState.Detached)
        //        _entities.Attach(entity);

        //    _entities.Remove(entity);
        //}

        public virtual IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public bool Disposed
        {
            get
            {
                if (!this.disposed)
                    return this.disposed;
                throw new ObjectDisposedException("CanBeDisposed");
            }
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _userManagementContext.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
