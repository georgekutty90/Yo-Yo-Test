using System.Collections.Generic;

namespace Yo_Yo_App.Entity.Athlete
{
    public class AthleteEntity
    {
        public int Id { get; set; }
        public string TraineeName { get; set; }
        public bool IsWarned { get; set; }
        public bool IsStopped { get; set; }
        public string Result { get; set; }
        public List<string> TraineeResult { get; set; }
    }
}
