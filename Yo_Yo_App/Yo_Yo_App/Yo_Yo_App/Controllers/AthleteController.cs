using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Yo_Yo_App.BAL.Athlete;
using Yo_Yo_App.BAL.FitnessTest;

namespace Yo_Yo_App.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class AthleteController : Controller
    {
        #region Declarations
        private readonly IAthleteBAL _iAthleteBAL;
        private readonly IFitnessTestBAL _iFitnessTestBAL;
        private readonly ILogger<AthleteController> _ilogger;
        #endregion

        #region Constructor
        public AthleteController(IAthleteBAL iAthleteBAL, IFitnessTestBAL iFitnessTestBAL, ILogger<AthleteController> ilogger)
        {
            _iAthleteBAL = iAthleteBAL;
            _iFitnessTestBAL = iFitnessTestBAL;
            _ilogger = ilogger;
        }
        #endregion

        #region Yo Yo Test Home Page
        /// <summary>
        /// Initial Load the Yo Yo Test Page with CommulativeTotalTime of last shuttle in the dataset.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            _ilogger.LogInformation("Inside AthleteController, menthod name Index");
            ViewBag.TotalTime = _iFitnessTestBAL.GetCommulativeTotalTime();// Method retrived the last shuttle Commulative Time
            return View();
        }
        #endregion

        #region Athletes List
        /// <summary>
        /// Get all athletes list.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllAthletes()
        {
            _ilogger.LogInformation("Inside AthleteController, menthod name GetAllAthletes");
            var athletesList = _iAthleteBAL.GetAllAthletes();
            return PartialView("~/Views/Athlete/_partialAthleteList.cshtml", athletesList);
        }
        #endregion

        #region Athletes Complete Result
        /// <summary>
        /// Get all Athletes Complete Result List.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="shuttle"></param>
        /// <returns></returns>
        public IActionResult GetAllAthletesCompleteResult(int level, int shuttle)
        {
            _ilogger.LogInformation("Inside AthleteController, menthod name GetAllAthletesCompleteResult");
            var athletesCompletedList = _iAthleteBAL.GetAllAthletesCompleteResult(level, shuttle);
            return PartialView("~/Views/Athlete/_partialAthleteList.cshtml", athletesCompletedList);
        }
        #endregion

        #region [HTTP POST Methods]
        /// <summary>
        /// Update individual athlete stop status and returns the test result for the particular athlete.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="testResult"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAthleteStopStatus(int id, string testResult)
        {
            _ilogger.LogInformation($"Inside AthleteController, menthod name UpdateAthleteStopStatus, id = {id}, testResult = {testResult}");
            bool status = false;
            if (id == 0)
            {
                return Json(new { testResult, status });
            }

            if (id > 0)
            {
                status = _iAthleteBAL.UpdateAthleteStopStatus(id, testResult);
                _ilogger.LogInformation($"UpdateAthleteStopStatus completed");
            }
            return Json(new { testResult, status });
        }

        /// <summary>
        ///  Update individual athlete warn status.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAthleteWarnStatus(int id)
        {
            _ilogger.LogInformation($"Inside AthleteController, menthod name UpdateAthleteWarnStatus, id = {id}");
            if (id == 0)
            {
                return Json(data: false);
            }

            if (id > 0)
            {
                var status = _iAthleteBAL.UpdateAthleteWarnStatus(id);
                _ilogger.LogInformation($"UpdateAthleteWarnStatus completed");
                return Json(data: status);
            }
            return Json(data: false);
        }
        #endregion
    }
}