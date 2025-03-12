using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using api.Data;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace api.tests.Controllers
{
    public class UsersControllerTest
    {
        private readonly AppDbContext _context;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _mockAuthService = new Mock<IAuthService>();
            _controller = new UsersController(_context, _mockAuthService.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { 
                    Id = 1, 
                    Email = "user1@example.com", 
                    Name = "User One", 
                    IsAdmin = false, 
                    CreatedDate = DateTime.Now, 
                    LastLoginDate = DateTime.Now, 
                    NumberOfLogins = 5, 
                    FailedLogins = 0, 
                    PasswordHash = "hashedpassword1" 
                },
                new User { 
                    Id = 2, 
                    Email = "user2@example.com", 
                    Name = "User Two", 
                    IsAdmin = false, 
                    CreatedDate = DateTime.Now, 
                    LastLoginDate = DateTime.Now, 
                    NumberOfLogins = 3, 
                    FailedLogins = 1, 
                    PasswordHash = "hashedpassword2" 
                }
            };

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsers = Assert.IsType<List<UserDto>>(okResult.Value);
            Assert.Equal(2, returnUsers.Count);
        }
    }
}