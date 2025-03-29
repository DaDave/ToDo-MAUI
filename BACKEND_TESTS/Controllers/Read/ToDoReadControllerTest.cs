using BACKEND;
using BACKEND.Controllers.Read;
using BACKEND.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND_TESTS.Controllers.Read
{
    [TestFixture]
    public class ToDoReadControllerTest
    {
        private AppDbContext _context;
        private ToDoReadController _toDoReadController;

        private readonly ToDo _toDoOne = new() { Id = 1, Title = "Meeseeks", Text = "Existence is pain", IsCompleted = false };
        private readonly ToDo _toDoTwo = new() { Id = 2, Title = "Sanchez", Text = "Wubba Lubba Dub Dub", IsCompleted = true };

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestReadDatabase")
                .Options;

            _context = new AppDbContext(options);

            _toDoReadController = new ToDoReadController(_context);
        }
        
        [Test]
        public async Task ShouldReturnOkResultWithNoEntryWhenGetToDoIsExecutedAndDatabaseHasNoEntry()
        {
            // Given
            var expectedCount = 0;
            
            // When
            var result = await _toDoReadController.GetToDo();

            // Then
            var actionResult = result ;
            Assert.NotNull(actionResult);
            var resultingToDos = actionResult.Value as List<ToDo>;
            Assert.NotNull(resultingToDos);
            Assert.AreEqual(expectedCount, resultingToDos.Count);
        }
        
        [Test]
        public async Task ShouldReturnOkResultWithOneEntryWhenGetToDoIsExecutedAndDatabaseHasOneEntry()
        {
            // Given
            _context.ToDo.Add(_toDoOne);
            _context.SaveChanges();
            var expectedCount = 1;
            
            // When
            var result = await _toDoReadController.GetToDo();

            // Then
            var actionResult = result ;
            Assert.NotNull(actionResult);
            var resultingToDos = actionResult.Value as List<ToDo>;
            Assert.NotNull(resultingToDos);
            Assert.AreEqual(expectedCount, resultingToDos.Count);
        }

        [Test]
        public async Task ShouldReturnOkResultWithTwoEntriesWhenGetToDoIsExecutedAndDatabaseHasTwoEntries()
        {
            // Given
            _context.ToDo.Add(_toDoOne);
            _context.ToDo.Add(_toDoTwo);
            _context.SaveChanges();
            var expectedCount = 2;
            
            // When
            var result = await _toDoReadController.GetToDo();

            // Then
            var actionResult = result ;
            Assert.NotNull(actionResult);
            var resultingToDos = actionResult.Value as List<ToDo>;
            Assert.NotNull(resultingToDos);
            Assert.AreEqual(expectedCount, resultingToDos.Count);
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