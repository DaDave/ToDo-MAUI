using BACKEND;
using BACKEND.Controllers.Create;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND_TESTS.Controllers.Create
{
    [TestFixture]
    public class ToDoCreateControllerTest
    {
        private AppDbContext _context;
        private ToDoCreateController _toDoCreateController;

        private readonly ToDo _toDo = new() { Id = 1, Title = "Meeseeks", Text = "Existence is pain", IsCompleted = false };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);

            _toDoCreateController = new ToDoCreateController(_context);
        }

        [Test]
        public async Task ShouldReturnCreatedWhenPostToDoCreatedOneEntryInDatabase()
        {
            // Given
            // When
            var result = await _toDoCreateController.PostToDo(_toDo);

            // Then
            var actionResult = result;
            Assert.NotNull(actionResult);
            var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
            Assert.NotNull(createdAtActionResult);
            var returnValue = createdAtActionResult.Value as ToDo;
            Assert.NotNull(returnValue);
            Assert.AreEqual(_toDo.Id, returnValue.Id);
        }
        
        [OneTimeTearDown]
        public void OnetimeTearDown()
        {
            _context.ToDo.RemoveRange(_context.ToDo);
            _context.SaveChanges();
            _context.Dispose();
        }
        
    }
}