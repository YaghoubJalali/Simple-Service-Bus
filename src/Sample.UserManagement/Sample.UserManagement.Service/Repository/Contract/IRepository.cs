using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Repository.Contract
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        //Task Delete(Guid id);
        //void Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
    }
}
