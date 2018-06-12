using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Commands
{
    /// <summary>
    /// Cleaning robot advance command
    /// </summary>
    public class CleaningRobotAdvanceCommand : CleaningRobotCommandBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotAdvanceCommand(ICleaningRobot robot) : base(robot)
        {
        }

        /// <summary>
        /// Run command
        /// </summary>
        public override CleaningRobotActionResult Run()
        {
            return Robot.Advance();
        }
    }
}
