using BusinessLogic.Providers.Interfaces;
using Common.Models.Base;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLogic.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly DatabaseContext context;

        public DatabaseProvider(DatabaseContext databaseContext)
        {
            context = databaseContext;
        }

        public List<T> GetAll<T>() where T : class, IEntity => context.Set<T>().ToList();

        public List<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().Where(predicate).ToList();

        public List<T2> GetAll<T1, T2>(Expression<Func<T1, T2>> selector) where T1 : class, IEntity => context.Set<T1>().Select(selector).ToList();

        public List<T2> GetAll<T1, T2>(Expression<Func<T1, T2>> selector, Expression<Func<T1, bool>> whereExp) where T1 : class, IEntity
            => context.Set<T1>().Where(whereExp).Select(selector).ToList();

        public T GetById<T>(int id) where T : class, IEntity => context.Set<T>().SingleOrDefault(e => e.Id == id);

        public async Task<T> FindPrimary<T>(int id) where T : class, IEntity => await context.Set<T>().FindAsync(id);

        public T Single<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().Single(predicate);

        public Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().SingleAsync(predicate);

        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().SingleOrDefault(predicate);

        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().SingleOrDefaultAsync(predicate);

        public T First<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().First(predicate);

        public Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().FirstAsync(predicate);

        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().FirstOrDefault(predicate);

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().FirstOrDefaultAsync(predicate);

        public T Last<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().Last(predicate);

        public Task<T> LastAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().LastAsync(predicate);

        public T LastOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().LastOrDefault(predicate);

        public Task<T> LastOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().LastOrDefaultAsync(predicate);

        public bool Any<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().Any(predicate);

        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().AnyAsync(predicate);

        public int Count<T>() where T : class, IEntity => context.Set<T>().Count();

        public Task<int> CountAsync<T>() where T : class, IEntity => context.Set<T>().CountAsync();

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().Count(predicate);

        public Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => context.Set<T>().CountAsync(predicate);

        public long LongCount<T>() where T : class, IEntity => context.Set<T>().LongCount();

        public Task<long> LongCountAsync<T>() where T : class, IEntity => context.Set<T>().LongCountAsync();

        public void ClearTable<T>() where T : class, IEntity
        {
            List<T> records = GetAll<T>();

            foreach (T record in records)
            {
                Delete(record);
            }
        }

        public void Create<T>(T entity) where T : class, IEntity
        {
            context.Add(entity);
        }

        public async Task CreateAsync<T>(T entity) where T : class, IEntity
        {
            await context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class, IEntity
        {
            context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class, IEntity => context.Set<T>().Remove(entity);

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
        {
            List<T> entities = GetAll(predicate);
            context.Set<T>().RemoveRange(entities);
        }

        public int Commit() => context.SaveChanges();

        public async Task<int> CommitAsync() => await context.SaveChangesAsync();
    }
}
