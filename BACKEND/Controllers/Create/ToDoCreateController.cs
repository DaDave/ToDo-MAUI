using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers.Create
{
    [Route("api/todo/create")]
    [ApiController]
    public class ToDoCreateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoCreateController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        {
            _context.ToDo.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateToDo", new { id = toDo.Id }, toDo);
        }
    }
}
