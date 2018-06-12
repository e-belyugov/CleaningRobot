using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Commands
{
    /// <summary>
    /// Cleaning robot command abstract class
    /// </summary>
    public abstract class CleaningRobotCommandBase : ICleaningRobotCommand
    {
        protected ICleaningRobot Robot;

        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotCommandBase(ICleaningRobot robot)
        {
            Robot = robot;
        }

        /// <summary>
        /// Run command
        /// </summary>
        public abstract CleaningRobotActionResult Run();
    }
}
