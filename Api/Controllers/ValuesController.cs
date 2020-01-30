using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext context;

        public ValuesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var values = await context.Values.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var value = await context.Values.FirstOrDefaultAsync(v => v.Id.Equals(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            await context.Values.AddAsync(new Domain.Value { Name = value });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
