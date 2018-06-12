using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Cleaning robot command interface
    /// </summary>
    public interface ICleaningRobotCommand
    {
        /// <summary>
        /// Run command
        /// </summary>
        CleaningRobotActionResult Run();
    }
}
