using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers.Update
{
    [Route("api/todo/update")]
    [ApiController]
    public class ToDoUpdateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoUpdateController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }
        
        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }
    }
}
