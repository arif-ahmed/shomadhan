using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using Somadhan.API.Controllers;
using Somadhan.API.Models;
using Somadhan.Application.Commands.Shops;

namespace Somadhan.API.Tests;
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
