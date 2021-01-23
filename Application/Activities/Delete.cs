using Infrastructure.Errors;
using MediatR;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.FindAsync(request.Id);

                if (activity == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"Activity of {request.Id} not found");
                }

                context.Activities.Remove(activity);

                var result = await context.SaveChangesAsync();

                return result > 0 ?
                        Unit.Value :
                        throw new Exception("Problem saving changes");
            }
        }
    }
}
