using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Room map serializer interface
    /// </summary>
    public interface IRoomMapSerializer
    {
        /// <summary>
        /// Deserializing map cells
        /// </summary>
        RoomMapCellState[,] Deserialize(string inputFileName);
    }
}
