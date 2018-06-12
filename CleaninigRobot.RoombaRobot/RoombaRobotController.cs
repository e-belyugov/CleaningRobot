using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaninigRobot.RoombaRobot
{
    /// <summary>
    /// Roomba robot class
    /// </summary>
    public class RoombaRobotController : ICleaningRobotController
    {
        // Private fields
        private IRoomMap _map;
        private ICleaningRobot _robot;
        private ICleaningRobotStrategy _givenStrategy;
        private ICleaningRobotStrategy _backOffStrategy;

        /// <summary>
        /// Room map
        /// </summary>
       public RoombaRobotController(
            IRoomMap roomMap, 
            ICleaningRobot robot,
            ICleaningRobotStrategy givenStrategy, 
            ICleaningRobotStrategy backOffStrategy)
        {
            _map = roomMap;
            _robot = robot;
            _givenStrategy = givenStrategy;
            _backOffStrategy = backOffStrategy;
        }

        /// <summary>
        /// Room map
        /// </summary>
        public IRoomMap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = Map;
            }
        }

        /// <summary>
        /// Cleaning robot
        /// </summary>
        public ICleaningRobot Robot
        {
            get
            {
                return _robot;
            }
            set
            {
                _robot = Robot;
            }
        }

        /// <summary>
        /// Given strategy
        /// </summary>
        public ICleaningRobotStrategy GivenStrategy
        {
            get
            {
                return _givenStrategy;
            }
            set
            {
                _givenStrategy = GivenStrategy;
            }
        }

        /// <summary>
        /// Back Off strategy
        /// </summary>
        public ICleaningRobotStrategy BackOffStrategy
        {
            get
            {
                return _backOffStrategy;
            }
            set
            {
                _backOffStrategy = BackOffStrategy;
            }
        }

        /// <summary>
        /// Load initial params from file
        /// </summary>
        public void Load(string inputFileName)
        {
            _map.Load(inputFileName);
            _robot.Load(inputFileName);
            _givenStrategy.Load(inputFileName);
        }

        /// <summary>
        /// Save results to file
        /// </summary>
        public void Save(string outputFileName)
        {
            _robot.Save(outputFileName);
        }

        /// <summary>
        /// Run controller
        /// </summary>
        public void Run()
        {
            CleaningRobotActionResult result = _givenStrategy.Algorithm();

            while (result == CleaningRobotActionResult.Obstacle)
            {
                result = _backOffStrategy.Algorithm();
                if (result != CleaningRobotActionResult.BatteryDrained) result = _givenStrategy.Algorithm();
            }
        }
    }
}
