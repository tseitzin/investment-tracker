using System;
using System.Text.Json;
using System.Threading.Tasks;
using api.Controllers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api.tests.Controllers
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly AuthController _controller;

        public AuthControllerTest()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockEmailService = new Mock<IEmailService>();
            _controller = new AuthController(_mockAuthService.Object, _mockEmailService.Object);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WhenRegistrationSucceeds()
        {
            // Arrange
            var request = new AuthRequest { 
                Email = "test@example.com", 
                Password = "Password123!",
                Name = "Test User"
            };
            var expectedResponse = new AuthResponse 
            { 
                Token = "test-token", 
                Email = "test@example.com", 
                Name = "Test User",
                IsAdmin = false
            };
            
            _mockAuthService.Setup(s => s.RegisterAsync(It.IsAny<AuthRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<AuthResponse>(okResult.Value);
            Assert.Equal(expectedResponse.Email, returnValue.Email);
            Assert.Equal(expectedResponse.Token, returnValue.Token);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var request = new AuthRequest { 
                Email = "test@example.com", 
                Password = "Password123!",
                Name = "Test User"
            };
            var errorMessage = "Email already exists";
            
            _mockAuthService.Setup(s => s.RegisterAsync(It.IsAny<AuthRequest>()))
                .ThrowsAsync(new InvalidOperationException(errorMessage));

            // Act
            var result = await _controller.Register(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult.Value);
            
            // Convert to JSON string and deserialize to extract message field regardless of structure
            var json = JsonSerializer.Serialize(badRequestResult.Value);
            var responseDoc = JsonDocument.Parse(json);
            
            Assert.True(responseDoc.RootElement.TryGetProperty("error", out var errorElement) || 
                        responseDoc.RootElement.TryGetProperty("message", out errorElement));
            Assert.Contains(errorMessage, errorElement.GetString());
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WhenLoginSucceeds()
        {
            // Arrange
            var request = new AuthRequest { 
                Email = "test@example.com", 
                Password = "Password123!",
                Name = "Test User"
            };
            var expectedResponse = new AuthResponse
            {
                Token = "test-token",
                Email = "test@example.com",
                Name = "Test User",
                IsAdmin = false
            };

            _mockAuthService.Setup(s => s.LoginAsync(It.IsAny<AuthRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<AuthResponse>(okResult.Value);
            Assert.Equal(expectedResponse.Email, returnValue.Email);
            Assert.Equal(expectedResponse.Token, returnValue.Token);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenInvalidCredentials()
        {
            // Arrange
            var request = new AuthRequest { 
                Email = "test@example.com", 
                Password = "WrongPassword",
                Name = "Test User"
            };
            
            _mockAuthService.Setup(s => s.LoginAsync(It.IsAny<AuthRequest>()))
                .ThrowsAsync(new InvalidOperationException());

            // Act
            var result = await _controller.Login(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(badRequestResult.Value);
            
            // Convert to JSON and check for error message
            var json = JsonSerializer.Serialize(badRequestResult.Value);
            var responseDoc = JsonDocument.Parse(json);
            
            Assert.True(responseDoc.RootElement.TryGetProperty("error", out var errorElement) || 
                        responseDoc.RootElement.TryGetProperty("message", out errorElement));
            Assert.Contains("Invalid credentials", errorElement.GetString());
        }

        [Fact]
        public async Task ForgotPassword_ReturnsOkResult_Always()
        {
            // Arrange
            var request = new ForgotPasswordRequest { Email = "test@example.com" };
            
            // Act
            var result = await _controller.ForgotPassword(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            // Convert to JSON and check for success message
            var json = JsonSerializer.Serialize(okResult.Value);
            var responseDoc = JsonDocument.Parse(json);
            
            Assert.True(responseDoc.RootElement.TryGetProperty("message", out var messageElement) ||
                        responseDoc.RootElement.TryGetProperty("success", out messageElement));
            var message = messageElement.GetString() ?? string.Empty;
            Assert.Contains("email", message.ToLower());
            Assert.Contains("reset", message.ToLower());
        }
        
        [Fact]
        public async Task ForgotPassword_ReturnsOkResult_EvenWhenExceptionOccurs()
        {
            // Arrange
            var request = new ForgotPasswordRequest { Email = "test@example.com" };
            
            _mockAuthService.Setup(s => s.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()))
                .ThrowsAsync(new Exception("Error occurred"));

            // Act
            var result = await _controller.ForgotPassword(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            // Convert to JSON and check for message
            var json = JsonSerializer.Serialize(okResult.Value);
            var responseDoc = JsonDocument.Parse(json);
            
            Assert.True(responseDoc.RootElement.TryGetProperty("message", out var messageElement) ||
                        responseDoc.RootElement.TryGetProperty("success", out messageElement));
            var message = messageElement.GetString() ?? string.Empty;
            Assert.Contains("email", message.ToLower());
            Assert.Contains("reset", message.ToLower());
        }

        [Fact]
        public async Task ResetPassword_ReturnsOkResult_WhenResetSucceeds()
        {
            // Arrange
            var request = new ResetPasswordRequest 
            { 
                Email = "test@example.com", 
                Token = "reset-token", 
                NewPassword = "NewPassword123!" 
            };
            
            // Act
            var result = await _controller.ResetPassword(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            // Convert to JSON and check for success message
            var json = JsonSerializer.Serialize(okResult.Value);
            var responseDoc = JsonDocument.Parse(json);
            
            Assert.True(responseDoc.RootElement.TryGetProperty("message", out var messageElement) ||
                        responseDoc.RootElement.TryGetProperty("success", out messageElement));
            var message = messageElement.GetString() ?? string.Empty;
            Assert.Contains("reset", message.ToLower());
            Assert.Contains("success", message.ToLower());
        }

        [Fact]
        public async Task ResetPassword_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var request = new ResetPasswordRequest
            {
                Email = "test@example.com",
                Token = "reset-token",
                NewPassword = "NewPassword123!"
            };
            var errorMessage = "Invalid or expired token";

            _mockAuthService.Setup(s => s.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()))
                .ThrowsAsync(new InvalidOperationException(errorMessage));

            // Act
            var result = await _controller.ResetPassword(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);

            // Convert to JSON and check for error message
            var json = JsonSerializer.Serialize(badRequestResult.Value);
            var responseDoc = JsonDocument.Parse(json);
            
            Assert.True(responseDoc.RootElement.TryGetProperty("error", out var errorElement) || 
                        responseDoc.RootElement.TryGetProperty("message", out errorElement));
            Assert.Contains(errorMessage, errorElement.GetString());
        }
    }
}