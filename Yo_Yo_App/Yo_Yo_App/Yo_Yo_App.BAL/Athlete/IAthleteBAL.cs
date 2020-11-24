using System.Collections.Generic;
using Yo_Yo_App.Models.Athlete;

namespace Yo_Yo_App.BAL.Athlete
{
    public interface IAthleteBAL
    {
        List<AthleteModel> GetAllAthletes();
        List<AthleteModel> GetAllAthletesCompleteResult(int level, int shuttle);
        bool UpdateAthleteStopStatus(int id, string testResult);
        bool UpdateAthleteWarnStatus(int id);
    }
}
