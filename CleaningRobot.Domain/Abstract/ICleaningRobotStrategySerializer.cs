namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Cleaning robot strategy serializer interface
    /// </summary>
    public interface ICleaningRobotStrategySerializer
    {
        /// <summary>
        /// Deserializing strategy data
        /// </summary>
        string[] Deserialize(string inputFileName);
    }
}
