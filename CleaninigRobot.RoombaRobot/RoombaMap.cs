using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaninigRobot.RoombaRobot
{
    public class RoombaMap : IRoomMap
    {
        // Private fields
        private RoomMapCellState[,] _cells;
        private readonly IRoomMapSerializer _serializer;

        /// <summary>
        /// Constructor
        /// </summary>
        public RoombaMap(IRoomMapSerializer serializer)
        {
            _serializer = serializer;
        }

        /// <summary>
        /// Room cells array
        /// </summary>
        public RoomMapCellState[,] Cells
        {
            get
            {
                return _cells;
            }
        } 

        /// <summary>
        /// Load map from file
        /// </summary>
        public void Load(string inputFileName)
        {
            _cells = _serializer.Deserialize(inputFileName);
        }
    }
}
