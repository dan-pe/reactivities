using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistance;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext context;
            private readonly ILogger<List> logger;


            public Handler(DataContext dataContext, ILogger<List> log )
            {
                context = dataContext;
                logger = log;
            }


            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(1000, cancellationToken);
                        logger.LogInformation($"Task {i} has completed");
                    }

                }
                catch (Exception ex) when (ex is TaskCanceledException)
                {
                    logger.LogInformation("$Task was cancelled");
                }

                return await context.Activities.ToListAsync(cancellationToken);
            }
        }
    }
}
