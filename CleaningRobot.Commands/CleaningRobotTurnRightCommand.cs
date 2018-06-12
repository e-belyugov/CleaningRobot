using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Commands
{
    /// <summary>
    /// Cleaning robot turn right command
    /// </summary>
    public class CleaningRobotTurnRightCommand : CleaningRobotCommandBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotTurnRightCommand(ICleaningRobot robot) : base(robot)
        {
        }

        /// <summary>
        /// Run command
        /// </summary>
        public override CleaningRobotActionResult Run()
        {
            return Robot.TurnRight();
        }
    }
}
