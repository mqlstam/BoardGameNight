using BoardGameNight.Data;
using BoardGameNight.Models;
using BoardGameNight.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BoardGameNight.Controllers;
using BoardGameNight.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace BoardGameNight.Tests
{
    [TestFixture]
    public partial class BordspellenavondControllerTests
    {
        private Mock<UserManager<Persoon>> _userManagerMock;
        private Mock<IBordspellenavondRepository> _repositoryMock;
        private Mock<IBordspelRepository> _bordspelRepositoryMock;
        private BordspellenavondController _controller;

        [SetUp]
        public void Setup()
        {
            var store = new Mock<IUserStore<Persoon>>();
            _userManagerMock = new Mock<UserManager<Persoon>>(store.Object, null, null, null, null, null, null, null, null);
            _repositoryMock = new Mock<IBordspellenavondRepository>();
            _bordspelRepositoryMock = new Mock<IBordspelRepository>();

            _controller = new BordspellenavondController(_repositoryMock.Object, _bordspelRepositoryMock.Object, _userManagerMock.Object);
        }

        // Basic tests for controller actions go here

        [Test]
        public async Task Index_ReturnsViewWithAllGameNights()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Bordspellenavond>());

            // Act
            var result = await _controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task MyOrganizedGameNights_ReturnsViewWithOrganizedGameNights()
        {
            // Arrange
            var userId = "user123";
            var organizedGameNights = new List<Bordspellenavond>
            {
                new Bordspellenavond
                {
                    Id = 1,
                    DatumTijd = DateTime.Now.AddDays(1),
                    MaxAantalSpelers = 10,
                    OrganisatorId = userId,
                    Adres = "456 Game Street",
                    Deelnemers = new List<Persoon>()
                }
            };

            _userManagerMock.Setup(manager => manager.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _repositoryMock.Setup(repo => repo.GetUserSubscriptions(userId)).ReturnsAsync(organizedGameNights);

            // Act
            var result = await _controller.MyBordspellenavonden();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task MyParticipatedGameNights_ReturnsViewWithParticipatedGameNights()
        {
            // Arrange
            var userId = "user123";
            var participatedGameNights = new List<Bordspellenavond>
            {
                new Bordspellenavond
                {
                    Id = 1,
                    DatumTijd = DateTime.Now.AddDays(1),
                    MaxAantalSpelers = 10,
                    OrganisatorId = "organizer123",
                    Adres = "456 Game Street",
                    Deelnemers = new List<Persoon>
                    {
                        new Persoon { Id = userId }
                    }
                }
            };

            _userManagerMock.Setup(manager => manager.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _repositoryMock.Setup(repo => repo.GetUserSubscriptions(userId)).ReturnsAsync(participatedGameNights);
            // Act
            var result = await _controller.MyBordspellenavonden();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        // Additional basic controller tests should be added here

        // Note: Depending on the methods in your controller, you might need to setup _userManagerMock to return specific users or user IDs
    }
}