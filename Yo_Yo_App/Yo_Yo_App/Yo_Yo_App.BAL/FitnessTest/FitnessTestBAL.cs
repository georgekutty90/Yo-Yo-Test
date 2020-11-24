using System;
using System.Collections.Generic;
using System.Linq;
using Yo_Yo_App.DAL.FitnessTest;
using Yo_Yo_App.Entity.Athlete;
using Yo_Yo_App.Models.Athlete;

namespace Yo_Yo_App.BAL.FitnessTest
{
    public class FitnessTestBAL : IFitnessTestBAL
    {
        #region Declarations
        private readonly IFitnessTestDAL _iFitnessTestDAL;
        private const int MAXSHUTTLE = 8;
        #endregion

        #region Constructor
        public FitnessTestBAL(IFitnessTestDAL iFitnessTestDAL)
        {
            _iFitnessTestDAL = iFitnessTestDAL;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get Athletes Test Data by level and shuttle.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="shuttle"></param>
        /// <param name="currentTime"></param>
        /// <param name="currentDistance"></param>
        /// <returns></returns>
        public FitnessTestDataModel GetFitnessRatingTestData(int level, int shuttle, int currentTime, int currentDistance)
        {
            var trackDetails = GetCurrentTrackDetailsByLevelAndShuttle(level, shuttle);
            if (trackDetails == null)
            {
                return null;
            }
            //Time in seconds for the shuttle.
            var commulativeTimeInSec = Utilities.Utilities.ConvertTimeToSecond(trackDetails.CommulativeTime);

            //Mapping entity to model.
            return new FitnessTestDataModel()
            {
                CurentLevel = trackDetails.SpeedLevel,
                CurrentShuttle = trackDetails.ShuttleNo,
                TotalDistance = trackDetails.AccumulatedShuttleDistance,
                TotaTime = commulativeTimeInSec,
                Speed = trackDetails.Speed,
                PreviousLevel = level,
                PreviousShuttle = shuttle,
                NextLevelStartTime = commulativeTimeInSec - currentTime,//Difference of commulativeTime of current test and next test, this will decrement by each second showing the time left for current shuttle.
                TotalTimeString = TimeSpan.FromSeconds(commulativeTimeInSec).ToString(@"mm\:ss"),
                //Distance in meter to be incremented on each second.
                //Calculation Logic - (Difference of AccumulatedShuttleDistance of current test and next test)/
                //                     (Difference of commulativeTime of current test and next test).
                DistancePerSecond = Math.Round(decimal.Divide(
                    trackDetails.AccumulatedShuttleDistance - currentDistance,
                    commulativeTimeInSec - currentTime), 1)
            };
        }

        /// <summary>
        /// Get All Fitness Rating Test Data.
        /// </summary>
        /// <returns></returns>
        public List<FitnessTestDataEntity> GetAllFitnessRatingData()
        {
            return _iFitnessTestDAL.GetAllFitnessRatingData();
        }

        /// <summary>
        /// Get Commulative Total Time. It wil be the CommulativeTime of last record as based on this time the timer hould run.
        /// </summary>
        /// <returns></returns>
        public int GetCommulativeTotalTime()
        {
            return Utilities.Utilities.ConvertTimeToSecond(_iFitnessTestDAL.GetAllFitnessRatingData()?.Last().CommulativeTime);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Algorithm to get the fitness test data based on level and shuttle.This method gets called when one shuttle completes.
        /// if shuttle = 8, then the shuttle gets reset to 1 and level gets incremented by 1.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="currentShuttle"></param>
        /// <returns></returns>
        private FitnessTestDataEntity GetCurrentTrackDetailsByLevelAndShuttle(int level, int currentShuttle)
        {
            var athletesTestDataList = _iFitnessTestDAL.GetAllFitnessRatingData();//Get all fitness test data.
            int maxLevel = athletesTestDataList.Max(x => x.SpeedLevel); // find the max level.
            currentShuttle = currentShuttle == MAXSHUTTLE ? 1 : currentShuttle + 1;// logic to check if shuttle ==8, if so update to 1, else increment it.
            level = currentShuttle == MAXSHUTTLE ? level + 1 : level; //logic to check if shuttle == 8, then increment by 1.
            for (int shuttle = currentShuttle; level <= maxLevel; shuttle++)
            {
                //Get fitness test data based on level and shuttle.
                var resultSet = GetTrackDetailsByLevelAndShuttle(athletesTestDataList, level, shuttle);
                if (resultSet != null)
                {
                    return resultSet;
                }
                if (shuttle == MAXSHUTTLE)
                {
                    level++;
                    shuttle = 0;
                }
            }
            return null;
        }

        /// <summary>
        /// Get fitness test data by level and shutte
        /// </summary>
        /// <param name="fitnessTestDataList"></param>
        /// <param name="level"></param>
        /// <param name="shuttle"></param>
        /// <returns></returns>
        private FitnessTestDataEntity GetTrackDetailsByLevelAndShuttle(List<FitnessTestDataEntity> fitnessTestDataList, int level, int shuttle)
        {
            return fitnessTestDataList.Find(x => x.ShuttleNo == shuttle && x.SpeedLevel == level);
        }
        #endregion
    }
}
