using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Commands
{
    /// <summary>
    /// Cleaning robot turn left command
    /// </summary>
    public class CleaningRobotTurnLeftCommand : CleaningRobotCommandBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotTurnLeftCommand(ICleaningRobot robot) : base(robot)
        {
        }

        /// <summary>
        /// Run command
        /// </summary>
        public override CleaningRobotActionResult Run()
        {
            return Robot.TurnLeft();
        }
    }
}
