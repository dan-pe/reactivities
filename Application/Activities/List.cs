﻿using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
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

            public Handler(DataContext dataContext)
            {
                context = dataContext;
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken) =>
                await context.Activities.ToListAsync();
        }
    }
}
