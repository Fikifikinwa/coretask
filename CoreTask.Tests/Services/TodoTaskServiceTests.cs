using CoreTask.Api.Data;
using CoreTask.Api.Dto;
using CoreTask.Api.Model;
using CoreTask.Api.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class TodoTaskServiceTests
{
    private readonly CoreTasksDbContext _context;
    private readonly TaskService _service;

    public TodoTaskServiceTests()
    {
        var options = new DbContextOptionsBuilder<CoreTasksDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CoreTasksDbContext(options);
        _service = new TaskService(_context);

        SeedData();
    }

    private void SeedData()
    {
        _context.TodoTasks.AddRange(
            new TodoTask { Title = "Task 1", IsCompleted = false },
            new TodoTask { Title = "Task 2", IsCompleted = true },
            new TodoTask { Title = "Task 3", IsCompleted = false }
        );

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetById_ShouldReturnTask_WhenTaskExists()
    {
        var result = await _service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Task 1");
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Create_ShouldAddNewTask()
    {
        var createDto = new CreateTaskDto
        {
            Title = "New Task"
        };

        var result = await _service.CreateAsync(createDto);

        result.Should().NotBeNull();
        result.Title.Should().Be("New Task");

        _context.TodoTasks.Count().Should().Be(4);
    }

    [Fact]
    public async Task Delete_ShouldRemoveTask_WhenExists()
    {
        var success = await _service.DeleteAsync(1);

        success.Should().BeTrue();
        _context.TodoTasks.Count().Should().Be(2);
    }

    [Fact]
    public async Task GetAll_ShouldReturnPagedResult()
    {
        var result = await _service.GetAllAsync(new Pagination { PageNumber = 1, PageSize = 2 }, null, "duedate", "desc");

        result.Items.Count().Should().Be(2);
        result.TotalCount.Should().Be(3);
        result.TotalPages.Should().Be(2);
    }
}