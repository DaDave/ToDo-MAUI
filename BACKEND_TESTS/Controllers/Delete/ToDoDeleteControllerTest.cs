using BACKEND;
using BACKEND.Controllers.Delete;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND_TESTS.Controllers.Delete
{
    [TestFixture]
    public class ToDoDeleteControllerTest
    {
        private AppDbContext _context;
        private ToDoDeleteController _toDoDeleteController;

        private readonly ToDo _toDoOne = new() { Id = 1, Title = "Meeseeks", Text = "Existence is pain", IsCompleted = false };
        private readonly ToDo _toDoTwo = new() { Id = 2, Title = "Sanchez", Text = "Wubba Lubba Dub Dub", IsCompleted = true };

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeleteDatabase")
                .Options;

            _context = new AppDbContext(options);

            _context.ToDo.Add(_toDoOne);
            _context.SaveChanges();

            _toDoDeleteController = new ToDoDeleteController(_context);
        }

        [Test]
        public async Task ShouldReturnNotFoundWhenDeleteToDoIsExecutedWithNotExistingIdInDatabase()
        {
            // Given
            // When
            var result = await _toDoDeleteController.DeleteToDo(_toDoTwo.Id);

            // Then
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task ShouldReturnNoContentWhenDeleteToDoIsExecutedWithExistingIdInDatabase()
        {
            // Given
            var id = 1;

            // When
            var result = await _toDoDeleteController.DeleteToDo(id);

            // Then
            Assert.IsInstanceOf<NoContentResult>(result);
        }
        
        [TearDown]
        public void TearDown()
        {
            _context.ToDo.RemoveRange(_context.ToDo);
            _context.SaveChanges();
            _context.Dispose();
        }
        
    }
}