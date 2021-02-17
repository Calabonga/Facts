using Calabonga.Facts.Web.Infrastructure.Mappers.Base;

using Xunit;

namespace Calabonga.Facts.Web.Tests
{
    public class AutomapperTests
    {
        [Fact]
        [Trait("Automapper", "Mapper Configuration")]
        public void ItShouldCorrectlyConfigured()
        {
            // arrange

            var config = MapperRegistration.GetMapperConfiguration();

            // act

            // assert
            config.AssertConfigurationIsValid();
        }
    }
}
