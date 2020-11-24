using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Yo_Yo_App.Entity.Athlete;

namespace Yo_Yo_App.DAL.Athlete
{
    public class AthleteDAL : IAthleteDAL
    {
        #region Declarations
        private readonly IMemoryCache _cache;
        private const string key = "AthleteDetails";
        private const string athletetDataFilePath = "/JsonData/athletes.json";
        #endregion 

        #region Constructor
        public AthleteDAL(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        #endregion

        #region Public Menthods
        /// <summary>
        /// Get Athetest list from cache and if cache is empty then get the data from json file
        /// </summary>
        /// <returns></returns>
        public List<AthleteEntity> GetAthletesList()
        {
            var athletesList = _cache.Get<List<AthleteEntity>>(key);
            if (athletesList == null)
            {
                using (StreamReader reader = new StreamReader($"{System.Environment.CurrentDirectory}{athletetDataFilePath}"))
                {
                    string jsonData = reader.ReadToEnd();
                    List<AthleteEntity> athletesData = JsonConvert.DeserializeObject<List<AthleteEntity>>(jsonData);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                      .SetSlidingExpiration(TimeSpan.FromSeconds(300000));

                    _cache.Set(key, athletesData, cacheEntryOptions);
                    return athletesData;
                }
            }
            return athletesList;
        }

        /// <summary>
        /// Update the Athletes list to cache.
        /// </summary>
        /// <param name="athletesList"></param>
        /// <returns></returns>
        public bool UpdateAthleteList(List<AthleteEntity> athletesList)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromSeconds(300000));
            _cache.Set(key, athletesList, cacheEntryOptions);
            return true;
        }
        #endregion 
    }
}
