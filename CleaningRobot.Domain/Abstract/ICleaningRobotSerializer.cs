using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Cleaning robot serializer interface
    /// </summary>
    public interface ICleaningRobotSerializer
    {
        /// <summary>
        /// Serializing cleaning robot state
        /// </summary>
        void Serialize(CleaningRobotState cleaningRobotState, string outputFileName);

        /// <summary>
        /// Deserializing cleaning robot
        /// </summary>
        CleaningRobotState Deserialize(string inputFileName);
    }
}
