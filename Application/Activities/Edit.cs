using Domain;
using MediatR;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public string City { get; set; }
            public DateTime? Date { get; set; }
            public string Description { get; set; }
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Venue { get; set; }
            public string Category { get; set; }
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
                    throw new Exception($"Activity of {request.Id} not found");
                }

                activity.City = request.City ?? activity.City;
                activity.Date = request.Date ?? activity.Date;
                activity.Description = request.Description ?? activity.Description;
                activity.Title = request.Title ?? activity.Title;
                activity.Venue = request.Venue ?? activity.Venue;
                activity.Category = request.Category ?? activity.Category;

                var result = await context.SaveChangesAsync();

                return result > 0 ?
                        Unit.Value :
                        throw new Exception("Problem saving changes");
            }
        }
    }
}
