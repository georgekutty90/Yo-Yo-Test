using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Yo_Yo_App.BAL.FitnessTest;
using Yo_Yo_App.DAL.FitnessTest;
using Yo_Yo_App.Models.Athlete;

namespace Yo_Yo_Ap.UnitTestApp.BALTest
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(IFitnessTestDAL), typeof(FitnessTestDAL));
            serviceCollection.AddSingleton(typeof(IFitnessTestBAL), typeof(FitnessTestBAL));


            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class FitnessTestBALTest : IClassFixture<DbFixture>
    {
        private FitnessTestBAL _iAthleteBAL;

        private ServiceProvider _serviceProvide;

        public FitnessTestBALTest(DbFixture fixture)
        {
            _serviceProvide = fixture.ServiceProvider;
        }

        [Fact]
        public void GetFitnessRatingTestDataTest()
        {
            // Arrange
            _iAthleteBAL = new FitnessTestBAL(_serviceProvide.GetService<IFitnessTestDAL>());

            // Act
            int level = 0;
            int shuttle = 0;
            int currentTime = 0;
            int currentDistance = 0;
            var result = _iAthleteBAL.GetFitnessRatingTestData(level, shuttle, currentTime, currentDistance);
            // Assert
            Assert.Equal(AssertFitnessRatingTestResultData(), result);
        }

        [Fact]
        public void GetCommulativeTotalTimeTest()
        {
            // Arrange
            _iAthleteBAL = new FitnessTestBAL(_serviceProvide.GetService<IFitnessTestDAL>());
            // Act
            var result = _iAthleteBAL.GetCommulativeTotalTime();
            // Assert
            Assert.Equal(1725, result);
        }

        private FitnessTestDataModel AssertFitnessRatingTestResultData()
        {
            return new FitnessTestDataModel()
            {
                CurentLevel = 5,
                CurrentShuttle = 1,
                DistancePerSecond = (decimal)1.7,
                NextLevelStartTime = 24,
                PreviousLevel = 0,
                PreviousShuttle = 0,
                Speed = "10",
                TotaTime = 24,
                TotalDistance = 40,
                TotalTimeString = "00:24",
            };
        }
    }


}
