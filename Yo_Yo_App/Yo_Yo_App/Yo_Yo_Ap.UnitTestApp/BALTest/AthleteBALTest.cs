using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Yo_Yo_App.BAL.Athlete;
using Yo_Yo_App.BAL.FitnessTest;
using Yo_Yo_App.DAL.Athlete;

namespace Yo_Yo_Ap.UnitTestApp.BALTest
{
    public class AthleteBALTest
    {
        private IAthleteBAL _iAthleteBAL;
        private Mock<IAthleteDAL> _iAthleteDAL;
        private Mock<IFitnessTestBAL> _iFitnessTestBAL;
        private Mock<IMapper> _iMapper;
        private Mock<ILogger<AthleteBAL>> _ilogger;

        [Fact]
        public void UpdateAthleteWarnStatusTest()
        {
            // Arrange
            _iAthleteBAL = new AthleteBAL(_iAthleteDAL.Object, _iFitnessTestBAL.Object, _iMapper.Object, _ilogger.Object);

            // Act
            var mockId = 1;
            var result = _iAthleteBAL.UpdateAthleteWarnStatus(mockId);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateAthleteStopStatus()
        {
            // Arrange
            _iAthleteBAL = new AthleteBAL(_iAthleteDAL.Object, _iFitnessTestBAL.Object, _iMapper.Object, _ilogger.Object);

            // Act
            var mockId = 1;
            var result = _iAthleteBAL.UpdateAthleteStopStatus(mockId, "0-0");
            // Assert
            Assert.True(result);
        }
    }
}
