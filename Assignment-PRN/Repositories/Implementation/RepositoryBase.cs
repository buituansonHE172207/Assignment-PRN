﻿using Assignment_PRN.Repositories;
using Assignment_PRN.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Assignment_PRN.Data;

namespace Assignment_PRN.Repositories.Implementation
{ 
    public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : class
    {
        protected readonly AssignmentPRNContext DbContext;
        protected readonly DbSet<T> DbSet;

        protected RepositoryBase(AssignmentPRNContext appDbContext)
        {
            DbContext = appDbContext;
            DbSet = appDbContext.Set<T>();
        }

        public EntityEntry<T> Add(T entity)
        {
            return DbSet.Add(entity);
        }

        public void AddAll(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity, CancellationToken.None);
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            return DbSet.AddRangeAsync(entities, CancellationToken.None);
        }

        public Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public long Count()
        {
            return DbSet.Count();
        }

        public Page<T> GetAll(PageRequest<T> pageRequest)
        {
            var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
            var data = DbSet.OrderBy(pageRequest.Sort ?? "Id desc").Skip(skip).Take(pageRequest.Size);
            var totalElement = DbSet.Count();
            return new Page<T>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.Size,
                TotalElement = totalElement,
                Data = totalElement == 0 ? new List<T>() : data.ToList()
            };
        }

        public IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public Page<T> GetByExpression(Expression<Func<T, bool>> predicate, PageRequest<T> pageRequest)
        {
            var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
            var data = DbSet.Where(predicate);
            var totalElement = data.Count();
            data = data.OrderBy(pageRequest.Sort ?? "Id desc").Skip(skip).Take(pageRequest.Size);
            return new Page<T>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.Size,
                TotalElement = totalElement,
                Data = totalElement == 0 ? new List<T>() : data.ToList()
            };
        }

        public T? GetById(TId id)
        {
            return DbSet.Find(id);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
