using System.Collections.Generic;
using Yo_Yo_App.Entity.Athlete;

namespace Yo_Yo_App.DAL.FitnessTest
{
    public interface IFitnessTestDAL
    {
        List<FitnessTestDataEntity> GetAllFitnessRatingData();
    }
}
