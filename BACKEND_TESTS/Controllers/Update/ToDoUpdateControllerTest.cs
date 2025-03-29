using BACKEND;
using BACKEND.Controllers.Update;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND_TESTS.Controllers.Update
{
    [TestFixture]
    public class ToDoUpdateControllerTest
    {
        private AppDbContext _context;
        private ToDoUpdateController _toDoUpdateController;

        private readonly ToDo _toDoOne = new() { Id = 1, Title = "Meeseeks", Text = "Existence is pain", IsCompleted = false };
        private readonly ToDo _toDoTwo = new() { Id = 2, Title = "Sanchez", Text = "Wubba Lubba Dub Dub", IsCompleted = true };

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestUpdateDatabase")
                .Options;

            _context = new AppDbContext(options);

            _context.ToDo.Add(_toDoOne);
            _context.ToDo.Add(_toDoTwo);
            _context.SaveChanges();

            _toDoUpdateController = new ToDoUpdateController(_context);
        }
        
        [Test]
        public async Task ShouldReturnBadRequestWhenPutToDoIsExecutedWithNoMatchInDatabase()
        {
            // Given
            var id = 2;

            // When
            var result = await _toDoUpdateController.PutToDo(id, _toDoOne);

            // Then
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task ShouldReturnNoContentWhenPutToDoUpdatedOneEntryInDatabase()
        {
            // Given
            var id = 1;

            // When
            var result = await _toDoUpdateController.PutToDo(id, _toDoOne);

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