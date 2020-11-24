using System.Collections.Generic;
using Yo_Yo_App.Entity.Athlete;

namespace Yo_Yo_App.DAL.Athlete
{
    public interface IAthleteDAL
    {
        List<AthleteEntity> GetAthletesList();
        bool UpdateAthleteList(List<AthleteEntity> athletesList);
    }
}
