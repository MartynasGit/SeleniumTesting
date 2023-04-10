using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NetworkUtility.Ping;
using FluentAssertions;
using FluentAssertions.Extensions;
using System.Net.NetworkInformation;

namespace NetworkUtilityTest.PingTests
{
    public class NetworkServiceTest
    {
        //Using Fluent Assertions.
        private readonly NetworkService _pingService;
        public NetworkServiceTest() 
        {
            //SUT
            _pingService = new NetworkService();
        }
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //Arrange - varriables, classes, mocks.
            var PingService = new NetworkService();

            //Act
            var result = PingService.SendPing();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Successs Ping Sent");
            result.Should().Contain("Successs", Exactly.Once());
        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(1, 1, 2)]

        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            //Arrange
            var pingService = new NetworkService();

            //Act
            var result = pingService.PingTimeOut(a, b);

            //Assert 

            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-100, 0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            //Arrange - varriables, classes, mocks.

            //Act
            var result = _pingService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2025));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnsObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _pingService.GetPingOptions();


            //Assert Warrning: be carefull

            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);

        }

        [Fact]
        public void NetworkService_MostRecentPings_ReturnsObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _pingService.MostRecentPings();


            //Assert Warrning: be carefull

            //result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);

        }


    }
}
