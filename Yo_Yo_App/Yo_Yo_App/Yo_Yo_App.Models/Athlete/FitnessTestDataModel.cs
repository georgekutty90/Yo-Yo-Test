namespace Yo_Yo_App.Models.Athlete
{
    public class FitnessTestDataModel
    {
        public int PreviousLevel { get; set; }
        public int PreviousShuttle { get; set; }
        public int CurentLevel { get; set; }
        public int CurrentShuttle { get; set; }
        public string Speed { get; set; }
        public int NextLevelStartTime { get; set; }
        public int TotaTime { get; set; }
        public int TotalDistance { get; set; }
        public string TotalTimeString { get; set; }
        public decimal DistancePerSecond { get; set; }
    }
}
