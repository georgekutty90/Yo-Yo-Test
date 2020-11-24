using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Yo_Yo_App.BAL.FitnessTest;
using Yo_Yo_App.DAL.Athlete;
using Yo_Yo_App.Entity.Athlete;
using Yo_Yo_App.Models.Athlete;

namespace Yo_Yo_App.BAL.Athlete
{
    public class AthleteBAL : IAthleteBAL
    {
        #region Declarations
        private readonly IAthleteDAL _iAthleteDAL;
        private readonly IFitnessTestBAL _iFitnessTestBAL;
        private readonly IMapper _iMapper;
        private readonly ILogger<AthleteBAL> _ilogger;
        #endregion

        #region Constructor
        public AthleteBAL(IAthleteDAL iAthleteDAL, IFitnessTestBAL iFitnessTestBAL, IMapper iMapper, ILogger<AthleteBAL> ilogger)
        {
            _iAthleteDAL = iAthleteDAL;
            _iFitnessTestBAL = iFitnessTestBAL;
            _iMapper = iMapper;
            _ilogger = ilogger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get All Athletes
        /// </summary>
        /// <returns></returns>
        public List<AthleteModel> GetAllAthletes()
        {
            _ilogger.LogInformation("Inside AthleteBAL, menthod name GetAllAthletes");
            var athletesList = GetAthletesList(); //Get all athletes list.
            var levelShuttleList = GetLevelShuttleList(); //Get all level-shuttle combination from the fitness data
            athletesList.ForEach(x => x.TraineeResult = levelShuttleList); //map the levelShuttleList to athletesList.
            return _iMapper.Map<List<AthleteModel>>(athletesList); // map entity to model.
        }

        /// <summary>
        /// Update Athlete Warn Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateAthleteWarnStatus(int id)
        {
            _ilogger.LogInformation("Inside AthleteBAL, menthod name UpdateAthleteWarnStatus");
            var athletesList = GetAthletesList(); //Get all athletes list.
            var athletetData = athletesList.Find(d => d.Id == id);
            if (athletetData != null)
            {
                athletetData.IsWarned = true;
            }
            return _iAthleteDAL.UpdateAthleteList(athletesList); //Update warn status to memoery cache.
        }

        /// <summary>
        /// Update Athlete Stop Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public bool UpdateAthleteStopStatus(int id, string testResult)
        {
            _ilogger.LogInformation("Inside AthleteBAL, menthod name UpdateAthleteStopStatus");
            var athletesList = GetAthletesList(); //Get all athletes list.
            var athletetData = athletesList.Find(d => d.Id == id);
            if (athletetData != null)
            {
                athletetData.IsStopped = true;
                athletetData.Result = testResult;
            }
            _iAthleteDAL.UpdateAthleteList(athletesList); //Update stop status to memoery cache.
            return CheckAthleteCompleteStatus(athletesList); //Check if all the atheletes stoped the test, if so returns true status indicatig the completeion.
        }

        /// <summary>
        /// Get All Athletes Complete Result when fitness test finishes.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="shuttle"></param>
        /// <returns></returns>
        public List<AthleteModel> GetAllAthletesCompleteResult(int level, int shuttle)
        {
            _ilogger.LogInformation("Inside AthleteBAL, menthod name GetAllAthletesCompleteResult");
            var athletesList = GetAthletesList();//Get all athletes list.
            athletesList.Where(x => !x.IsStopped).ToList().ForEach(x => x.Result = level + "-" + shuttle);//updateatheletes result of whoum still running.
            var levelShuttleList = GetLevelShuttleList(); //Get all level-shuttle combination from the fitness data
            athletesList.ForEach(x => { x.TraineeResult = levelShuttleList; x.IsStopped = true; }); //Update stop status of all athletes as test finished.
            return _iMapper.Map<List<AthleteModel>>(athletesList);// map entity to model.
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get Athletes List. Common method since used in many places.
        /// </summary>
        /// <returns></returns>
        private List<AthleteEntity> GetAthletesList()
        {
            _ilogger.LogInformation("Inside AthleteBAL, menthod name GetAthletesList");
            return _iAthleteDAL.GetAthletesList();
        }
        /// <summary>
        /// Get Level Shuttle List. Common method since used in many places.
        /// </summary>
        /// <returns></returns>
        private List<string> GetLevelShuttleList()
        {
            _ilogger.LogInformation("Inside AthleteBAL, menthod name GetLevelShuttleList");
            var athletesttestDataList = _iFitnessTestBAL.GetAllFitnessRatingData();
            var speedLevelList = athletesttestDataList.Select(o => o.SpeedLevel + "-" + o.ShuttleNo).Distinct();
            speedLevelList = speedLevelList.Concat(new[] { "0-0" });
            return speedLevelList.ToList();
        }
        /// <summary>
        /// Check if all athletes stoped there running.
        /// </summary>
        /// <param name="athletesList"></param>
        /// <returns></returns>
        private bool CheckAthleteCompleteStatus(List<AthleteEntity> athletesList)
        {
            _ilogger.LogInformation("Inside AthleteBAL, menthod name CheckAthleteCompleteStatus");
            return athletesList.FindAll(x => !x.IsStopped).Count == 0;
        }
        #endregion
    }
}
