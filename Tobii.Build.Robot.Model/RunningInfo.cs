namespace Tobii.Build.Robot.Model
{
    public class RunningInfo
    {
        public string PercentageComplete { get; set; }
        public string ElapsedSeconds { get; set; }
        public string EstimatedTotalSeconds { get; set; }
        public string CurrentStageText { get; set; }
        public bool ProbablyHanging { get; set; }
    }
}