using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using Shomadhan.API.Controllers;
using Shomadhan.API.Models;
using Shomadhan.Application.Commands.Shops;

namespace Shomadhan.API.Tests;
public class ShopControllerTests
{
    [Fact]
    public async Task Post_ReturnsCreatedResult_WithShopId()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateShopCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("123"); // Assuming ShopId is a string

        var loggerMock = new Mock<ILogger<ShopsController>>();

        var controller = new ShopsController(loggerMock.Object, mediatorMock.Object);

        var command = new CreateShopCommand
        {
            // Fill with required properties for command if any
        };

        // Act
        var result = await controller.Post(command);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var value = Assert.IsType<CreateShopResponse>(createdResult.Value);

        Assert.Equal("123", value.ShopId);
        Assert.Equal("Shop created successfully", value.Message);

    }
}
