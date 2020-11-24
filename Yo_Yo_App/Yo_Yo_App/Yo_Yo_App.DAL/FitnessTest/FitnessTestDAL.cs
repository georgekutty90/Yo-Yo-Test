using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Yo_Yo_App.Entity.Athlete;
namespace Yo_Yo_App.DAL.FitnessTest
{
    public class FitnessTestDAL : IFitnessTestDAL
    {
        #region Declarations
        private const string fitnessTestDataPath = "/JsonData/fitnessrating_beeptest.json";
        private const string key = "FitnessData";
        private readonly IMemoryCache _cache;
        #endregion

        #region Constructor
        public FitnessTestDAL(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        #endregion

        #region GetAllFitnessRatingData
        /// <summary>
        /// Get All Athletes Fitness Rating Test data list from cache, if empty then get the data from json file.
        /// </summary>
        /// <returns></returns>
        public List<FitnessTestDataEntity> GetAllFitnessRatingData()
        {
            var fitnessTestDataList = _cache.Get<List<FitnessTestDataEntity>>(key);
            if (fitnessTestDataList == null)
            {
                using (StreamReader reader = new StreamReader($"{System.Environment.CurrentDirectory}{fitnessTestDataPath}"))
                {
                    string data = reader.ReadToEnd();
                    List<FitnessTestDataEntity> fitnessTestData = JsonConvert.DeserializeObject<List<FitnessTestDataEntity>>(data);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromSeconds(300000));

                    _cache.Set(key, fitnessTestData, cacheEntryOptions);
                    return fitnessTestData;
                }
            }
            return fitnessTestDataList;
        }
        #endregion
    }
}
