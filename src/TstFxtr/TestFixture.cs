using MediatR;
using Ninject;
using Respawn;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TransactionalDbContext;

namespace TstFxtr
{
    public class TestFixture : IDisposable
    {
        private readonly IKernel _kernel;
        private readonly ReadCommittedDbContext _context;
        private static Checkpoint Checkpoint;

        internal TestFixture(IKernel kernel, ReadCommittedDbContext context)
        {
            _kernel = kernel;
            _context = context;
            var connectionString = context.Database.Connection.ConnectionString;
            Checkpoint.Reset(connectionString);
        }
        public async Task SaveAll(params object[] entities)
        {
            Do(dbContext =>
            {
                foreach (var entity in entities)
                {
                    var entry = dbContext.ChangeTracker.Entries().FirstOrDefault(entityEntry => entityEntry.Entity == entity);
                    if (entry == null)
                    {
                        dbContext.Set(entity.GetType()).Add(entity);
                    }
                }
            });
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Do(dbContext => dbContext.Set<TEntity>().Remove(entity));
        }
        public async Task SendAsync(IRequest message)
        {
            await SendAsync((IRequest<Unit>)message);
        }
        public async Task<TResult> SendAsync<TResult>(IRequest<TResult> message, bool asNonPullStep = false)
        {
            var result = default(TResult);

            var context = _context;

            context.BeginTransaction();

            var mediator = _kernel.Get<IMediator>();

            Exception exc = null;
            try
            {
                result = await mediator.Send(message);
            }
            catch (Exception e)
            {
                exc = e;
            }

            context.CloseTransaction(exc);

            if (exc != null)
            {
                throw new Exception("Failed to send message.", exc);
            }

            return result;
        }
        public void Do(Action action)
        {
            try
            {
                _context.BeginTransaction();
                action();
                _context.CloseTransaction();
            }
            catch (Exception e)
            {
                _context.CloseTransaction(e);
                throw;
            }
        }
        public void Do(Action<DbContext> action)
        {
            try
            {
                _context.BeginTransaction();
                action(_context);
                _context.CloseTransaction();
            }
            catch (Exception e)
            {
                _context.CloseTransaction(e);
                throw;
            }
        }
        public async Task DoClean<T>(Action<T> action) where T : ReadCommittedDbContext, new()
        {
            var dbContext = new T();

            try
            {
                dbContext.BeginTransaction();
                action(dbContext);
                dbContext.CloseTransaction();
            }
            catch (Exception e)
            {
                dbContext.CloseTransaction(e);
                throw;
            }
        }
        public void Dispose()
        {
            //test
            _context.Dispose();
            _kernel.Dispose();
        }
    }
}
