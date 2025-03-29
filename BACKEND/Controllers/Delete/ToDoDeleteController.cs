using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers.Delete
{
    [Route("api/todo/delete")]
    [ApiController]
    public class ToDoDeleteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoDeleteController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDo.Remove(toDo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
