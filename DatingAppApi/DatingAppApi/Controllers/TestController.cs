using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DataContext _context;

        #region Constructor

        public TestController(DataContext context)
        {
            _context = context;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetValuesAsync() 
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValuesByIdAsync(int id)
        {
            var value = await  _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }
    }
}