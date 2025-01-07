using MediatR;
using Microsoft.AspNetCore.Http;
using UnclewoodCleanArchitecture.Domain.Common.Events;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IPublisher publisher, UnclewoodDbContext dbContext )
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue("DomainEventQueue", out var value)
                    && value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }
                }
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
            
           
        });
        await next(context);
    }
}