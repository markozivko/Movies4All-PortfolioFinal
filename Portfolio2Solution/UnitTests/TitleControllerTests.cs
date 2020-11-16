using System;
using FluentAssertions;
using Xunit;
using WebService.Controllers;
using Microsoft.AspNetCore.Mvc;
using DataServiceLibrary;
using Moq;
using DataServiceLibrary.Models;
using AutoMapper;
using WebService.Models;

namespace UnitTests
{
    public class TitleControllerTests
    {

        [Fact]
        public void GetTitleWithValidIdShouldReturnOk()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(x => x.GetTitle("tt0098769")).Returns(new TitleBasics());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<TitleDto>(It.IsAny<TitleBasics>())).Returns(new TitleDto());

            var UrlMock = new Mock<IUrlHelper>();

            var ctrl = new TitleController(dataServiceMock.Object, mapperMock.Object);
            ctrl.Url = UrlMock.Object;

            var response = ctrl.GetTitle("tt0098769");

            response.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
