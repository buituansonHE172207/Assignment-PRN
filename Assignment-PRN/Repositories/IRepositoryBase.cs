using System.Linq.Expressions;
using Assignment_PRN.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Assignment_PRN.Repositories;

public interface IRepositoryBase<T, in TId> where T : class
{
    EntityEntry<T> Add(T entity);
    void AddAll(IEnumerable<T> entities);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    Task UpdateAsync(T entity);
    void Remove(T entity);
    IEnumerable<T> GetAll();
    long Count();
    Page<T> GetAll(PageRequest<T> pageRequest);
    IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate);
    Page<T> GetByExpression(Expression<Func<T, bool>> predicate, PageRequest<T> pageRequest);
    T? GetById(TId id);
}