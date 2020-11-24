using System;

namespace Yo_Yo_App.Utilities
{
    public static class Utilities
    {
        #region ConvertTimeToSecond
        /// <summary>
        /// Method to convert Time in (00:00) format to seconds
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertTimeToSecond(string time)
        {
            var stringTime = time.Split(':');
            var minutes = Convert.ToInt32(stringTime[0]);
            var seconds = Convert.ToInt32(stringTime[1]);
            return (minutes * 60) + seconds;
        }
        #endregion

        #region CalculateDistancePerSecond
        /// <summary>
        /// Method to caculate distance in meters to be added per second.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int CalculateDistancePerSecond(string time)
        {
            var stringTime = time.Split(':');
            var minutes = Convert.ToInt32(stringTime[0]);
            var seconds = Convert.ToInt32(stringTime[1]);
            return (minutes * 60) + seconds;
        }
        #endregion
    }
}
