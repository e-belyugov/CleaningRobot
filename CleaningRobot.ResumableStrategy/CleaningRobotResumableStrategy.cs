using System.Collections.Generic;
using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.ResumableStrategy
{
    /// <summary>
    /// Cleaning robot back off strategy
    /// </summary>
    public class CleaningRobotResumableStrategy : ICleaningRobotStrategy
    {
        // Private fields
        private ICleaningRobotCommand _turnLeftCommand;
        private ICleaningRobotCommand _turnRightCommand;
        private ICleaningRobotCommand _advanceCommand;
        private ICleaningRobotCommand _cleanCommand;
        private readonly ICleaningRobotStrategySerializer _serializer;
        private string[] _data;
        private readonly Queue<ICleaningRobotCommand> _commandQueue = new Queue<ICleaningRobotCommand>(); // Command queue

        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotResumableStrategy(
            ICleaningRobotCommand turnLeftCommand,
            ICleaningRobotCommand turnRightCommand,
            ICleaningRobotCommand advanceCommand,
            ICleaningRobotCommand cleanCommand,
            ICleaningRobotStrategySerializer serializer
        )
        {
            _turnLeftCommand = turnLeftCommand;
            _turnRightCommand = turnRightCommand;
            _advanceCommand = advanceCommand;
            _cleanCommand = cleanCommand;
            _serializer = serializer;
        }

        /// <summary>
        /// Strategy data
        /// </summary>
        public string[] Data
        {
            get
            {
                return _data;
            }
        }

        /// <summary>
        /// Load strategy data from file
        /// </summary>
        public void Load(string inputFileName)
        {
            // Loading raw data
            _data = _serializer.Deserialize(inputFileName);

            // Enqueuing commands
            _commandQueue.Clear();
            foreach (var str in _data)
            {
                ICleaningRobotCommand command = null;

                // Defining commands
                switch (str)
                {
                    case "TL":
                        command = _turnLeftCommand;
                        break;
                    case "TR":
                        command = _turnRightCommand;
                        break;
                    case "A":
                        command = _advanceCommand;
                        break;
                    case "C":
                        command = _cleanCommand;
                        break;
                }

                if (command != null) _commandQueue.Enqueue(command);
            }
        }

        /// <summary>
        /// Resumable algorithm
        /// </summary>
        public CleaningRobotActionResult Algorithm()
        {
            CleaningRobotActionResult result = CleaningRobotActionResult.Success;

            while (_commandQueue.Count > 0)
            {
                ICleaningRobotCommand command = _commandQueue.Dequeue();
                result = command.Run();

                if (result != CleaningRobotActionResult.Success) break;
            }

            return result;
        }
    }
}
