using System.Security.Claims;
using MC.IMS.API.Models;
using MC.IMS.API.Models.Request.Put;
using MC.IMS.API.Service.Interface.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MC.IMS.TESTS.Controllers.TransactionController;

public class PutPartnerPolicyTests
{
    private readonly Mock<ITransactionV1Service> _mockTransactionV1Service;
    private readonly API.Controllers.TransactionController _controller;

    public PutPartnerPolicyTests()
    {
        _mockTransactionV1Service = new Mock<ITransactionV1Service>();
        var mockTransactionV1Repository = SetupHelper.GenerateTransactionRepositoryMock();
        var mockReferenceV1Repository = SetupHelper.GenerateReferenceRepositoryMock();

        _controller = new API.Controllers.TransactionController(_mockTransactionV1Service.Object, mockTransactionV1Repository.Object, mockReferenceV1Repository.Object);

        var httpContext = new DefaultHttpContext();
        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext,
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor
            {
                ActionName = "PutPartnerPolicy"
            }
        };

        _controller.ControllerContext = controllerContext;
    }

    private void SetUserClaims()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "500329"),
            new(ClaimTypes.Name, "John Doe"),
            new(ClaimTypes.Role, "Sales Agent"),
            new(ClaimTypes.Authentication, "http://localhost:7175"),
            new(ClaimTypes.AuthenticationMethod, "mc.ims.api"),
            new(ClaimTypes.Expiration, "1893456000")
        };

        var identity = new ClaimsIdentity(claims, "mock");
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext.HttpContext.User = principal;
    }

    [Fact]
    public async Task PutPartnerPolicy_ReturnsOk()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPartnerPolicy()
        {
            Id = 1,
            AgentsId = 1,
            SubAgentsId = null,
            ApproversId = 3,
            PromoManagersId = 1,
            SalesManagersId = 3,
            PromoOfficersId = 2,
            Remarks = "Remarks",
        };
        _mockTransactionV1Service.Setup(s => s.PutPartnerPolicy(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((object _, CancellationToken _) => TransactionResult.Success());

        // Act
        var result = await _controller.PutPartnerPolicy(request);

        // Assert
        var statusCodeResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task PutPartnerPolicy_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPartnerPolicy()
        {
            Id = 1,
            AgentsId = 100,
            SubAgentsId = null,
            ApproversId = 3,
            PromoManagersId = 1,
            SalesManagersId = 3,
            PromoOfficersId = 2,
            Remarks = "Remarks",
        };

        var resultData = TransactionResult.Failure(new Exception("Invalid values:{ " + Environment.NewLine + "AgentsId - Value is not valid.;" + Environment.NewLine + "}"));
        _mockTransactionV1Service.Setup(s => s.PutPartnerPolicy(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultData);

        // Act
        var result = await _controller.PutPartnerPolicy(request);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
        Assert.Contains("AgentsId - Value is not valid.;", (string?)statusCodeResult.Value);
    }

    [Fact]
    public async Task PutPartnerPolicy_ReturnsBadRequest_WhenCustomSQLExceptionIsFound()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPartnerPolicy()
        {
            Id = 1,
            AgentsId = 1,
            SubAgentsId = null,
            ApproversId = 3,
            PromoManagersId = 1,
            SalesManagersId = 3,
            PromoOfficersId = 2,
            Remarks = "Remarks",
        };

        var resultData = TransactionResult.Failure(new Exception("CustomSQLException: Policy not found."));
        _mockTransactionV1Service.Setup(s => s.PutPartnerPolicy(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultData);

        // Act
        var result = await _controller.PutPartnerPolicy(request);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
        Assert.Equal("Policy not found.", statusCodeResult.Value);
    }

    [Fact]
    public async Task PutPartnerPolicy_ReturnsInternalServerError()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPartnerPolicy()
        {
            Id = 1,
            AgentsId = 1,
            SubAgentsId = null,
            ApproversId = 3,
            PromoManagersId = 1,
            SalesManagersId = 3,
            PromoOfficersId = 2,
            Remarks = "Remarks",
        };
        var resultData = TransactionResult.Failure(new Exception("Service failed"));
        _mockTransactionV1Service.Setup(s => s.PutPartnerPolicy(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultData);

        // Act
        var result = await _controller.PutPartnerPolicy(request);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task PutPartnerPolicy_ReturnsRequestTimeout()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPartnerPolicy()
        {
            Id = 1,
            AgentsId = 1,
            SubAgentsId = null,
            ApproversId = 3,
            PromoManagersId = 1,
            SalesManagersId = 3,
            PromoOfficersId = 2,
            Remarks = "Remarks",
        };
        _mockTransactionV1Service.Setup(s => s.PutPartnerPolicy(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        // Act
        var result = await _controller.PutPartnerPolicy(request);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status408RequestTimeout, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task PutPartnerPolicy_ReturnsUnauthorized()
    {
        // Arrange
        _controller.ControllerContext.HttpContext.User = null!;
        var request = new PutPartnerPolicy()
        {
            Id = 1,
            AgentsId = 1,
            SubAgentsId = null,
            ApproversId = 3,
            PromoManagersId = 1,
            SalesManagersId = 3,
            PromoOfficersId = 2,
            Remarks = "Remarks",
        };

        // Act
        var result = await _controller.PutPartnerPolicy(request);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status401Unauthorized, statusCodeResult.StatusCode);
    }
}