using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var Endpoint = "https://ukweli.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = Endpoint
            });
            var sut = new UsersService(httpClient, config);

            // Act
            await sut.GetAllUsers();

            // Assert
            handlerMock.Protected()
                .Verify("SendAsync", 
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());

        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var Endpoint = "https://ukweli.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = Endpoint
            });
            var sut = new UsersService(httpClient);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var Endpoint = "https://ukweli.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = Endpoint
            });
            var sut = new UsersService(httpClient);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endPoint = "http://ukweli.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endPoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endPoint
            });

            var sut = new UsersService(httpClient, config);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            handlerMock.Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get && req.RequestUri.ToString() == endPoint),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
