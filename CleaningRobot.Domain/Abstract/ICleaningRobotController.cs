namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Cleaning robot controller interface
    /// </summary>
    public interface ICleaningRobotController
    {
        /// <summary>
        /// Room map
        /// </summary>
        IRoomMap Map { get; set; }

        /// <summary>
        /// Cleaning robot
        /// </summary>
        ICleaningRobot Robot { get; set; }

        /// <summary>
        /// Given strategy
        /// </summary>
        ICleaningRobotStrategy GivenStrategy { get; set; }

        /// <summary>
        /// Back Off strategy
        /// </summary>
        ICleaningRobotStrategy BackOffStrategy { get; set; }

        /// <summary>
        /// Load initial params from file
        /// </summary>
        void Load(string inputFileName);

        /// <summary>
        /// Save results to file
        /// </summary>
        void Save(string outputFileName);

        /// <summary>
        /// Run controller
        /// </summary>
        void Run();
    }
}
