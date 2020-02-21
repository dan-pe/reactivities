using Domain;
using MediatR;
using Persistance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public string City { get; set; }
            public DateTime Date { get; set; }
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
                var activity = new Activity
                {
                    City = request.City,
                    Date = request.Date,
                    Description = request.Description,
                    Id = request.Id,
                    Title = request.Title,
                    Venue = request.Venue,
                    Category = request.Category,
                };

                context.Activities.Add(activity);
                var result = await context.SaveChangesAsync();

                return result > 0 ? 
                        Unit.Value :
                        throw new Exception("Problem saving changes");
            }
        }
    }
}
