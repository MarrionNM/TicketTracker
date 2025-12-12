using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskTracker.Api.Controllers;
using TaskTracker.Api.Contracts;
using TaskTracker.Api.Data.DTO;
using TaskTracker.Api.Data.Enums;

namespace TaskTracker.Tests.Controllers;

public class TicketControllerTests
{
    private readonly Mock<ITicketRepository> _repo = new();

    private TicketController BuildController()
    {
        return new TicketController(_repo.Object);
    }

    // GET mocked data
    [Fact]
    public async Task GetTasks_ReturnsSeededList()
    {
        // Arrange
        var fakeList = new List<TicketDTO>
            {
                new TicketDTO
                {
                    Id = 1,
                    Title = "Test 1",
                    Description = "D1",
                    Status = EStatus.New,
                    Priority = EPriority.Low
                },
                new TicketDTO
                {
                    Id = 2,
                    Title = "Test 2",
                    Description = "D2",
                    Status = EStatus.InProgress,
                    Priority = EPriority.High
                }
            };

        _repo
            .Setup(r => r.GetAllAsync(It.IsAny<string?>(), It.IsAny<string?>()))
            .ReturnsAsync(fakeList);

        var controller = BuildController();

        // Act
        var result = await controller.GetTasks(null, "dueDate:asc");

        // Assert
        var ok = result.Result as OkObjectResult;
        ok.Should().NotBeNull();

        var returned = ok!.Value as IEnumerable<TicketDTO>;
        returned.Should().NotBeNull();
        returned!.Count().Should().Be(2);
    }

    // VALIDATION Test
    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        // Arrange
        var controller = BuildController();
        controller.ModelState.AddModelError("Title", "Required");

        // invalid model
        var invalidDto = new TicketDTO
        {
            Title = "",
            Description = "desc",
            Status = EStatus.New,
            Priority = EPriority.Low
        };

        // Act
        var result = await controller.Create(invalidDto);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }
}
