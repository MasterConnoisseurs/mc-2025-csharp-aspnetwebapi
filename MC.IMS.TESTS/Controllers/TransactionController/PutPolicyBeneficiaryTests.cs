using System.Security.Claims;
using MC.IMS.API.Models;
using MC.IMS.API.Models.Request.Put;
using MC.IMS.API.Service.Interface.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MC.IMS.TESTS.Controllers.TransactionController;

public class PutPolicyBeneficiaryTests
{
    private readonly Mock<ITransactionV1Service> _mockTransactionV1Service;
    private readonly API.Controllers.TransactionController _controller;

    public PutPolicyBeneficiaryTests()
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
                ActionName = "PutPolicyBeneficiary"
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
    public async Task PutPolicyBeneficiary_ReturnsOk()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPolicyBeneficiary()
        {
            Id = 10,
            FirstName = "Maria",
            MiddleName = "C",
            LastName = "Santos",
            Suffix = null,
            EmailAddress = "maria.santos@email.com",
            ContactNumber = "+639171234567",
            DateOfBirth = new DateTime(1985, 05, 15),
            Relationship = 7,
        };
        _mockTransactionV1Service.Setup(s => s.PutPolicyBeneficiary(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((object _, CancellationToken _) => TransactionResult.Success());

        // Act
        var result = await _controller.PutPolicyBeneficiary(request);

        // Assert
        var statusCodeResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task PutPolicyBeneficiary_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPolicyBeneficiary()
        {
            Id = 10,
            FirstName = "Maria",
            MiddleName = "C",
            LastName = null!,
            Suffix = null,
            EmailAddress = "maria.santos@email.com",
            ContactNumber = "+639171234567",
            DateOfBirth = new DateTime(1985, 05, 15),
            Relationship = 7,
        };

        var resultData = TransactionResult.Failure(new Exception("Invalid values:{ " + Environment.NewLine + "LastName - Value must not be null.;" + Environment.NewLine + "}"));
        _mockTransactionV1Service.Setup(s => s.PutPolicyBeneficiary(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultData);

        // Act
        var result = await _controller.PutPolicyBeneficiary(request);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
        Assert.Contains("LastName - Value must not be null.;", (string?)statusCodeResult.Value);
    }

    [Fact]
    public async Task PutPolicyBeneficiary_ReturnsBadRequest_WhenCustomSQLExceptionIsFound()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPolicyBeneficiary()
        {
            Id = 10,
            FirstName = "Maria",
            MiddleName = "C",
            LastName = "Santos",
            Suffix = null,
            EmailAddress = "maria.santos@email.com",
            ContactNumber = "+639171234567",
            DateOfBirth = new DateTime(1985, 05, 15),
            Relationship = 7,
        };

        var resultData = TransactionResult.Failure(new Exception("CustomSQLException: Beneficiary not found."));
        _mockTransactionV1Service.Setup(s => s.PutPolicyBeneficiary(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultData);

        // Act
        var result = await _controller.PutPolicyBeneficiary(request);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
        Assert.Equal("Beneficiary not found.", statusCodeResult.Value);
    }

    [Fact]
    public async Task PutPolicyBeneficiary_ReturnsInternalServerError()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPolicyBeneficiary()
        {
            Id = 10,
            FirstName = "Maria",
            MiddleName = "C",
            LastName = "Santos",
            Suffix = null,
            EmailAddress = "maria.santos@email.com",
            ContactNumber = "+639171234567",
            DateOfBirth = new DateTime(1985, 05, 15),
            Relationship = 7,
        };
        var resultData = TransactionResult.Failure(new Exception("Service failed"));
        _mockTransactionV1Service.Setup(s => s.PutPolicyBeneficiary(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultData);

        // Act
        var result = await _controller.PutPolicyBeneficiary(request);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task PutPolicyBeneficiary_ReturnsRequestTimeout()
    {
        // Arrange
        SetUserClaims();
        var request = new PutPolicyBeneficiary()
        {
            Id = 10,
            FirstName = "Maria",
            MiddleName = "C",
            LastName = "Santos",
            Suffix = null,
            EmailAddress = "maria.santos@email.com",
            ContactNumber = "+639171234567",
            DateOfBirth = new DateTime(1985, 05, 15),
            Relationship = 7,
        };
        _mockTransactionV1Service.Setup(s => s.PutPolicyBeneficiary(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new OperationCanceledException());

        // Act
        var result = await _controller.PutPolicyBeneficiary(request);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status408RequestTimeout, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task PutPolicyBeneficiary_ReturnsUnauthorized()
    {
        // Arrange
        _controller.ControllerContext.HttpContext.User = null!;
        var request = new PutPolicyBeneficiary()
        {
            Id = 10,
            FirstName = "Maria",
            MiddleName = "C",
            LastName = "Santos",
            Suffix = null,
            EmailAddress = "maria.santos@email.com",
            ContactNumber = "+639171234567",
            DateOfBirth = new DateTime(1985, 05, 15),
            Relationship = 7,
        };

        // Act
        var result = await _controller.PutPolicyBeneficiary(request);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status401Unauthorized, statusCodeResult.StatusCode);
    }
}