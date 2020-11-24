using AutoMapper;
using Yo_Yo_App.Entity.Athlete;
using Yo_Yo_App.Models.Athlete;

namespace Yo_Yo_App.EntityMapping
{
    public class MappingEntity : Profile
    {
        /// <summary>
        /// Mapper to map Enity to Model
        /// </summary>
        public MappingEntity()
        {
            CreateMap<AthleteEntity, AthleteModel>();
        }
    }
}
