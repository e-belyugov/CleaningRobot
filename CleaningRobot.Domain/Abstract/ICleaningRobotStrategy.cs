using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Cleaning robot strategy interface
    /// </summary>
    public interface ICleaningRobotStrategy
    {
        /// <summary>
        /// Strategy data
        /// </summary>
        string[] Data { get; }

        /// <summary>
        /// Load strategy data from file
        /// </summary>
        void Load(string inputFileName);

        /// <summary>
        /// Cleaning robot algorithm
        /// </summary>
        CleaningRobotActionResult Algorithm();
    }
}
