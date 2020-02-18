using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Persistance
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.Activities.Any())
            {
                var activities = new List<Activity>()
                {

                };

                context.Activities.AddRange(activities);
                context.SaveChanges();
            }
        }
    }
}
