using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Domain.Abstract
{
    /// <summary>
    /// Interface for room map
    /// </summary>
    public interface IRoomMap
    {
        /// <summary>
        /// Room cells array
        /// </summary>
        RoomMapCellState[,] Cells { get; }

        /// <summary>
        /// Load map from file
        /// </summary>
        void Load(string inputFileName);
    }
}
