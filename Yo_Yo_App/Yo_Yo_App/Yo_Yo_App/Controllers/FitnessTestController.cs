using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yo_Yo_App.BAL.FitnessTest;
using Yo_Yo_App.Models.Athlete;

namespace Yo_Yo_App.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class FitnessTestController : Controller
    {
        #region Declarations
        private readonly IFitnessTestBAL _iFitnessTestBAL;
        private readonly ILogger<FitnessTestController> _ilogger;
        #endregion

        #region Constructor
        public FitnessTestController(IFitnessTestBAL iFitnessTestBAL, ILogger<FitnessTestController> ilogger)
        {
            _iFitnessTestBAL = iFitnessTestBAL;
            _ilogger = ilogger;
        }
        #endregion

        #region Get Athletes Fitness Rating TestData
        /// <summary>
        /// Get Athletes Fitness Rating Test data based on level and shuttle
        /// </summary>
        /// <param name="level"></param>
        /// <param name="shuttle"></param>
        /// <param name="time"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public IActionResult GetFitnessRatingTestData(int level, int shuttle, int time, int distance)
        {
            _ilogger.LogInformation("Inside FitnessTestController, menthod name GetFitnessRatingTestData");
            var athletestTestData = _iFitnessTestBAL.GetFitnessRatingTestData(level, shuttle, time, distance);
            if (athletestTestData == null) //All the track levels completed thats why athletestTestData is null.
            {
                ViewBag.CompleteStatus = "true";
                return PartialView("~/Views/FitnessTest/_partialFitnessTestResult.cshtml", new FitnessTestDataModel());
            }
            return PartialView("~/Views/FitnessTest/_partialFitnessTestResult.cshtml", athletestTestData);
        }
        #endregion'
    }
}