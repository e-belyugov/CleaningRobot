using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Commands
{
    /// <summary>
    /// Cleaning robot clean command
    /// </summary>
    public class CleaningRobotCleanCommand : CleaningRobotCommandBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotCleanCommand(ICleaningRobot robot) : base(robot)
        {
        }

        /// <summary>
        /// Run command
        /// </summary>
        public override CleaningRobotActionResult Run()
        {
            return Robot.Clean();
        }
    }
}
