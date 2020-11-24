using System.Collections.Generic;
using Yo_Yo_App.Entity.Athlete;
using Yo_Yo_App.Models.Athlete;

namespace Yo_Yo_App.BAL.FitnessTest
{
    public interface IFitnessTestBAL
    {
        FitnessTestDataModel GetFitnessRatingTestData(int level, int shuttle, int time, int distance);
        List<FitnessTestDataEntity> GetAllFitnessRatingData();
        int GetCommulativeTotalTime();
    }
}
