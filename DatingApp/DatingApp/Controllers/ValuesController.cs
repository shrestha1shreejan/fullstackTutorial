using System.Threading.Tasks;
using DatingApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        #region Constructor

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        #endregion


        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValuesAsync()
        {
            var values = await _context.Values.ToListAsync();           
            return Ok(values);  
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValueById(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

            if (value != null)
            {
                return Ok(value);
            }

            return BadRequest();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
