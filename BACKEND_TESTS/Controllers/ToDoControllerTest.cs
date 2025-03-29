using BACKEND;
using BACKEND.Controllers;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND_TESTS.Controllers
{
    [TestFixture]
    public class ToDoControllerTest
    {
        private AppDbContext _context;
        private ToDoController _toDoController;

        private readonly ToDo _toDoOne = new() { Id = 1, Title = "Meeseeks", Text = "Existence is pain", IsCompleted = false };
        private readonly ToDo _toDoTwo = new() { Id = 2, Title = "Sanchez", Text = "Wubba Lubba Dub Dub", IsCompleted = true };
        private readonly ToDo _toDoThree = new() { Id = 3, Title = "Smith", Text = "Human music, I like it", IsCompleted = true };
        private readonly ToDo _toDoFour = new() { Id = 4, Title = "Smith Jr.", Text = "Okay, alright", IsCompleted = false };

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);

            _context.ToDo.Add(_toDoOne);
            _context.ToDo.Add(_toDoTwo);
            _context.SaveChanges();

            _toDoController = new ToDoController(_context);
        }

        [Test]
        public async Task ShouldReturnOkResultWithTwoEntriesWhenGetToDoIsExecutedAndDatabaseHasTwoEntries()
        {
            // Given
            var expectedCount = 2;
            
            // When
            var result = await _toDoController.GetToDo();

            // Then
            var actionResult = result ;
            Assert.NotNull(actionResult);
            var resultingToDos = actionResult.Value as List<ToDo>;
            Assert.NotNull(resultingToDos);
            Assert.AreEqual(expectedCount, resultingToDos.Count);
        }

        [Test]
        public async Task ShouldReturnCreatedWhenPostToDoCreatedOneEntryInDatabase()
        {
            // Given
            // When
            var result = await _toDoController.PostToDo(_toDoThree);

            // Then
            var actionResult = result;
            Assert.NotNull(actionResult);
            var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
            Assert.NotNull(createdAtActionResult);
            var returnValue = createdAtActionResult.Value as ToDo;
            Assert.NotNull(returnValue);
            Assert.AreEqual(_toDoThree.Id, returnValue.Id);
        }
        
        [Test]
        public async Task ShouldReturnBadRequestWhenPutToDoIsExecutedWithNoMatchInDatabase()
        {
            // Given
            var id = 2;

            // When
            var result = await _toDoController.PutToDo(id, _toDoOne);

            // Then
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task ShouldReturnNoContentWhenPutToDoUpdatedOneEntryInDatabase()
        {
            // Given
            var id = 1;

            // When
            var result = await _toDoController.PutToDo(id, _toDoOne);

            // Then
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task ShouldReturnNotFoundWhenDeleteToDoIsExecutedWithNotExistingIdInDatabase()
        {
            // Given
            // When
            var result = await _toDoController.DeleteToDo(_toDoFour.Id);

            // Then
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task ShouldReturnNoContentWhenDeleteToDoIsExecutedWithExistingIdInDatabase()
        {
            // Given
            var id = 1;

            // When
            var result = await _toDoController.DeleteToDo(id);

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