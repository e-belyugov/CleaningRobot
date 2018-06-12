using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaninigRobot.RoombaRobot
{
    /// <summary>
    /// Roomba robot class
    /// </summary>
    public class RoombaRobot : ICleaningRobot
    {
        // Private fields
        private readonly IRoomMap _roomMap;
        private readonly ICleaningRobotSerializer _serializer;
        private CleaningRobotState _state = new CleaningRobotState();

        /// <summary>
        /// Constructor
        /// </summary>
        public RoombaRobot(IRoomMap roomMap, ICleaningRobotSerializer serializer)
        {
            _roomMap = roomMap;
            _serializer = serializer;
        }

        /// <summary>
        /// Robot state
        /// </summary>
        public CleaningRobotState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = State;
            }
        }

        /// <summary>
        /// Robots turns left
        /// </summary>
        public CleaningRobotActionResult TurnLeft()
        {
            // Checking battery
            const int batteryUnits = 1;
            if (_state.Battery < batteryUnits)
                return CleaningRobotActionResult.BatteryDrained;

            // Facing change
            switch (_state.Facing)
            {
                case CleaningRobotFacing.North:
                    State.Facing = CleaningRobotFacing.West;
                    break;
                case CleaningRobotFacing.East:
                    State.Facing = CleaningRobotFacing.North;
                    break;
                case CleaningRobotFacing.South:
                    State.Facing = CleaningRobotFacing.East;
                    break;
                case CleaningRobotFacing.West:
                    State.Facing = CleaningRobotFacing.South;
                    break;
            }

            // Battery draining
            _state.Battery = _state.Battery - batteryUnits;

            return CleaningRobotActionResult.Success; // Always success after turning
        }

        /// <summary>
        /// Robots turns right
        /// </summary>
        public CleaningRobotActionResult TurnRight()
        {
            // Checking battery
            const int batteryUnits = 1;
            if (_state.Battery < batteryUnits) return CleaningRobotActionResult.BatteryDrained;

            // Facing change
            switch (_state.Facing)
            {
                case CleaningRobotFacing.North:
                    State.Facing = CleaningRobotFacing.East;
                    break;
                case CleaningRobotFacing.East:
                    State.Facing = CleaningRobotFacing.South;
                    break;
                case CleaningRobotFacing.South:
                    State.Facing = CleaningRobotFacing.West;
                    break;
                case CleaningRobotFacing.West:
                    State.Facing = CleaningRobotFacing.North;
                    break;
            }

            // Battery draining
            _state.Battery = _state.Battery - batteryUnits;

            return CleaningRobotActionResult.Success; // Always success after turning
        }

        /// <summary>
        /// Robot advances forward
        /// </summary>
        public CleaningRobotActionResult Advance()
        {
            // Checking battery
            const int batteryUnits = 2;
            if (_state.Battery < batteryUnits) return CleaningRobotActionResult.BatteryDrained;

            // Movement by coordinates 
            int xMove = 0;
            int yMove = 0;

            // Facing change
            switch (_state.Facing)
            {
                case CleaningRobotFacing.North:
                    yMove--;
                    break;
                case CleaningRobotFacing.East:
                    xMove++;
                    break;
                case CleaningRobotFacing.South:
                    yMove++;
                    break;
                case CleaningRobotFacing.West:
                    xMove--;
                    break;
            }

            // New robot location
            int xNew = _state.Location.X + xMove;
            int yNew = _state.Location.Y + yMove;

            // Map size
            int rowCount = _roomMap.Cells.GetLength(0);
            int columnCount = _roomMap.Cells.GetLength(1);

            // Cell outside map
            bool obstacle = xNew > rowCount - 1 || xNew < 0 || yNew > columnCount - 1 || yNew < 0;

            // Cell inside map not accessible
            if (!obstacle) obstacle = _roomMap.Cells[xNew, yNew] == RoomMapCellState.NotCleanable 
                    || _roomMap.Cells[xNew, yNew] == RoomMapCellState.Empty;

            // Battery draining
            _state.Battery = _state.Battery - batteryUnits;

            if (!obstacle)
            {
                // Successfull action

                // New location
                RoomMapPoint roomMapPoint = new RoomMapPoint();
                roomMapPoint.X = xNew;
                roomMapPoint.Y = yNew;
                State.Location = roomMapPoint;

                // Appending visited list (if not visited before)
                if (!_state.MapPointsVisited.Contains(roomMapPoint)) _state.MapPointsVisited.Add(roomMapPoint);

                return CleaningRobotActionResult.Success;
            }
            else
            {
                // Obstacle
                return CleaningRobotActionResult.Obstacle;
            }
        }

        /// <summary>
        /// Robot moves back
        /// </summary>
        public CleaningRobotActionResult Back()
        {
            // Checking battery
            const int batteryUnits = 3;
            if (_state.Battery < batteryUnits) return CleaningRobotActionResult.BatteryDrained;

            // Movement by coordinates 
            int xMove = 0;
            int yMove = 0;

            // Facing change
            switch (_state.Facing)
            {
                case CleaningRobotFacing.North:
                    yMove++;
                    break;
                case CleaningRobotFacing.East:
                    xMove--;
                    break;
                case CleaningRobotFacing.South:
                    yMove--;
                    break;
                case CleaningRobotFacing.West:
                    xMove++;
                    break;
            }

            // New robot location
            int xNew = _state.Location.X + xMove;
            int yNew = _state.Location.Y + yMove;

            // Map size
            int rowCount = _roomMap.Cells.GetLength(0);
            int columnCount = _roomMap.Cells.GetLength(1);

            // Cell outside map
            bool obstacle = xNew > rowCount - 1 || xNew < 0 || yNew > columnCount - 1 || yNew < 0;

            // Cell inside map not accessible
            if (!obstacle) obstacle = _roomMap.Cells[xNew, yNew] == RoomMapCellState.NotCleanable
                    || _roomMap.Cells[xNew, yNew] == RoomMapCellState.Empty;

            // Battery draining
            _state.Battery = _state.Battery - batteryUnits;

            if (!obstacle)
            {
                // Successfull action

                // New location
                RoomMapPoint roomMapPoint = new RoomMapPoint();
                roomMapPoint.X = xNew;
                roomMapPoint.Y = yNew;
                State.Location = roomMapPoint;

                // Appending visited list (if not visited before)
                if (!_state.MapPointsVisited.Contains(roomMapPoint)) _state.MapPointsVisited.Add(roomMapPoint);

                return CleaningRobotActionResult.Success;
            }
            else
            {
                // Obstacle
                return CleaningRobotActionResult.Obstacle;
            }
        }

        /// <summary>
        /// Robot cleans
        /// </summary>
        public CleaningRobotActionResult Clean()
        {
            // Checking battery
            const int batteryUnits = 5;
            if (_state.Battery < batteryUnits) return CleaningRobotActionResult.BatteryDrained;

            // Battery draining
            _state.Battery = _state.Battery - batteryUnits;

            // Appending cleaned list (if not cleaned before)
            if (!_state.MapPointsCleaned.Contains(State.Location)) _state.MapPointsCleaned.Add(State.Location);

            return CleaningRobotActionResult.Success; // Always success after cleaning
        }

        /// <summary>
        /// Loading robot state from file
        /// </summary>
        public void Load(string inputFileName)
        {
            _state = _serializer.Deserialize(inputFileName);
        }

        /// <summary>
        /// Save robot state to file
        /// </summary>
        public void Save(string outputFileName)
        {
            _serializer.Serialize(_state, outputFileName);
        }
    }
}
