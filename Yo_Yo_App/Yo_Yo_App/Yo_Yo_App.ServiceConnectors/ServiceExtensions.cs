using Microsoft.Extensions.DependencyInjection;
using Yo_Yo_App.BAL.Athlete;
using Yo_Yo_App.BAL.FitnessTest;
using Yo_Yo_App.DAL.Athlete;
using Yo_Yo_App.DAL.FitnessTest;

namespace Yo_Yo_App.ServiceConnectors
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// register interface with concrete type.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IAthleteDAL), typeof(AthleteDAL));
            services.AddSingleton(typeof(IAthleteBAL), typeof(AthleteBAL));
            services.AddSingleton(typeof(IFitnessTestDAL), typeof(FitnessTestDAL));
            services.AddSingleton(typeof(IFitnessTestBAL), typeof(FitnessTestBAL));
            return services;
        }
    }
}
