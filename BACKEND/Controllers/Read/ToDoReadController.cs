using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers.Read
{
    [Route("api/todo/read")]
    [ApiController]
    public class ToDoReadController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoReadController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDo()
        {
            return await _context.ToDo.ToListAsync();
        }
    }
}
