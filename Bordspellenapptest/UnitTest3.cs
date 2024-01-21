using BoardGameNight.Controllers;
using BoardGameNight.Data;
using BoardGameNight.Models;
using BoardGameNight.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BoardGameNight.Repositories.Interfaces;
using Moq;

namespace BoardGameNight.Tests
{
    [TestFixture]
    public partial class BordspellenavondControllerTests2
    {
        private BordspellenavondController _controller;
        private Mock<UserManager<Persoon>> _userManagerMock;
        private Mock<IBordspellenavondRepository> _repositoryMock;
        private Mock<IBordspelRepository> _bordspelRepositoryMock;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            var store = new Mock<IUserStore<Persoon>>();
            _userManagerMock = new Mock<UserManager<Persoon>>(store.Object, null, null, null, null, null, null, null, null);
            _repositoryMock = new Mock<IBordspellenavondRepository>();
            _bordspelRepositoryMock = new Mock<IBordspelRepository>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new BordspellenavondController(_repositoryMock.Object, _bordspelRepositoryMock.Object, _userManagerMock.Object);
        }

        [Test]
        public async Task Create_WhenModelIsValid_ReturnsRedirectToActionResult()
        {
            // Arrange
            var userId = "user123";
            _userManagerMock.Setup(manager => manager.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);

            var bordspellenavond = new Bordspellenavond
            {
                Adres = "123 Main Street",
                MaxAantalSpelers = 10,
                DatumTijd = DateTime.Now.AddDays(1),
                Is18Plus = false,
                Dieetwensen = Dieetwensen.Geen,
                DrankVoorkeur = DrankVoorkeur.GeenVoorkeur
            };

            // Act
            var result = await _controller.Create();

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public async Task Create_WhenModelIsInvalid_ReturnsViewResult()
        {
            // Arrange
            var bordspellenavond = new Bordspellenavond(); // Invalid model

            // Act
            var result = await _controller.Create();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}