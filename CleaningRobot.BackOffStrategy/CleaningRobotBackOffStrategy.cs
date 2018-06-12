using System;
using System.Collections.Generic;
using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.BackOffStrategy
{
    /// <summary>
    /// Cleaning robot back off strategy
    /// </summary>
    public class CleaningRobotBackOffStrategy : ICleaningRobotStrategy
    {
        // Private fields
        private ICleaningRobotCommand _turnLeftCommand;
        private ICleaningRobotCommand _turnRightCommand;
        private ICleaningRobotCommand _advanceCommand;
        private ICleaningRobotCommand _backCommand;
        private ICleaningRobotCommand _cleanCommand;
        private readonly Queue<ICleaningRobotCommand> _commandQueue = new Queue<ICleaningRobotCommand>(); // Command queue

        /// <summary>
        /// Constructor
        /// </summary>
        public CleaningRobotBackOffStrategy(
            ICleaningRobotCommand turnLeftCommand, 
            ICleaningRobotCommand turnRightCommand,
            ICleaningRobotCommand advanceCommand,
            ICleaningRobotCommand backCommand,
            ICleaningRobotCommand cleanCommand
            )
        {
            _turnLeftCommand = turnLeftCommand;
            _turnRightCommand = turnRightCommand;
            _advanceCommand = advanceCommand;
            _backCommand = backCommand;
            _cleanCommand = cleanCommand;
        }

        /// <summary>
        /// Strategy data
        /// </summary>
        public string[] Data
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Load strategy data from file
        /// </summary>
        public void Load(string inputFileName)
        {
            throw new NotImplementedException();
        }

        // Processing command queue
        private CleaningRobotActionResult ProcessCommandQueue()
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

        /// <summary>
        /// Back off algorithm
        /// </summary>
        public CleaningRobotActionResult Algorithm()
        {
            // Sequence 1
            _commandQueue.Clear();
            _commandQueue.Enqueue(_turnRightCommand);
            _commandQueue.Enqueue(_advanceCommand);
            CleaningRobotActionResult result = ProcessCommandQueue();

            // Sequence 2
            if (result == CleaningRobotActionResult.Obstacle)
            {
                _commandQueue.Clear();
                _commandQueue.Enqueue(_turnLeftCommand);
                _commandQueue.Enqueue(_backCommand);
                _commandQueue.Enqueue(_turnRightCommand);
                _commandQueue.Enqueue(_advanceCommand);
                result = ProcessCommandQueue();
            }

            // Sequence 3
            if (result == CleaningRobotActionResult.Obstacle)
            {
                _commandQueue.Clear();
                _commandQueue.Enqueue(_turnLeftCommand);
                _commandQueue.Enqueue(_turnLeftCommand);
                _commandQueue.Enqueue(_advanceCommand);
                result = ProcessCommandQueue();
            }

            // Sequence 4
            if (result == CleaningRobotActionResult.Obstacle)
            {
                _commandQueue.Clear();
                _commandQueue.Enqueue(_turnRightCommand);
                _commandQueue.Enqueue(_backCommand);
                _commandQueue.Enqueue(_turnRightCommand);
                _commandQueue.Enqueue(_advanceCommand);
                result = ProcessCommandQueue();
            }

            // Sequence 5
            if (result == CleaningRobotActionResult.Obstacle)
            {
                _commandQueue.Clear();
                _commandQueue.Enqueue(_turnLeftCommand);
                _commandQueue.Enqueue(_turnLeftCommand);
                _commandQueue.Enqueue(_advanceCommand);
                result = ProcessCommandQueue();
            }

            return result;
        }
    }
}
