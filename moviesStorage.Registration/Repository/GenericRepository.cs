﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using moviesStorage.Registration.Repository.Abstraction;
using moviesStorage.Registration.ServiceContext;
using moviesStorage.Registration.Data.Identity;

namespace moviesStorage.Registration.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IdentityContext _context;
    private readonly DbSet<T> _db;

    public GenericRepository(IdentityContext context)
    {
        _context = context;
        _db = _context.Set<T>();
    }
    
    public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        List<string> includes = null)
    {
        IQueryable<T> query = _db;

        if (expression != null)
        {
            query = query.Where(expression);
        }
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, includesProperty) => 
                current.Include(includesProperty)); 
        }
        if(orderBy != null)
        {
            query = orderBy(query);
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
    {
        IQueryable<T> query = _db;
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, includesProperty) =>
                current.Include(includesProperty));
        }
        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task Insert(T entity)
    {
        await _db.AddAsync(entity);
    }

    public async Task InsertRange(IEnumerable<T> entities)
    {
        await _db.AddRangeAsync(entities);
    }

    public async Task  Delete(int id)
    {
        var entity = await _db.FindAsync(id);
        if (entity != null) _db.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _db.RemoveRange(entities);
    }
    
    public void Update(T entity)
    {
        _db.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}