using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Cleaning robot interface
    /// </summary>
    public interface ICleaningRobot
    {
        /// <summary>
        /// Robot state
        /// </summary>
        CleaningRobotState State { get; set; }

        /// <summary>
        /// Robots turns left
        /// </summary>
        CleaningRobotActionResult TurnLeft();

        /// <summary>
        /// Robot turns right
        /// </summary>
        CleaningRobotActionResult TurnRight();

        /// <summary>
        /// Robot advances forward
        /// </summary>
        CleaningRobotActionResult Advance();

        /// <summary>
        /// Robot moves back
        /// </summary>
        CleaningRobotActionResult Back();

        /// <summary>
        /// Robot cleans
        /// </summary>
        CleaningRobotActionResult Clean();

        /// <summary>
        /// Loading robot state from file
        /// </summary>
        void Load(string inputFileName);

        /// <summary>
        /// Save robot state to file
        /// </summary>
        void Save(string outputFileName);
    }
}
