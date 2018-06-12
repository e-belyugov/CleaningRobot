using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Commands
{
    /// <summary>
    /// Cleaning robot back command
    /// </summary>
    public class CleaningRobotBackCommand : CleaningRobotCommandBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotBackCommand(ICleaningRobot robot) : base(robot)
        {
        }

        /// <summary>
        /// Run command
        /// </summary>
        public override CleaningRobotActionResult Run()
        {
            return Robot.Back();
        }
    }
}
