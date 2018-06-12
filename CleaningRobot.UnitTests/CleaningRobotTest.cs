using System.IO;
using System.Linq;
using CleaningRobot.BackOffStrategy;
using CleaningRobot.Commands;
using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;
using CleaningRobot.Json;
using CleaningRobot.ResumableStrategy;
using CleaninigRobot.RoombaRobot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleaningRobot.UnitTests
{
    [TestClass]
    public class CleaningRobotTest
    {
        // Private fields
        private IRoomMapSerializer _mapSerializer;
        private IRoomMap _map;
        private ICleaningRobotSerializer _robotSerializer;
        private ICleaningRobot _robot;
        private ICleaningRobotStrategySerializer _strategySerializer;
        private ICleaningRobotCommand _trCommand;
        private ICleaningRobotCommand _tlCommand;
        private ICleaningRobotCommand _aCommand;
        private ICleaningRobotCommand _bCommand;
        private ICleaningRobotCommand _cCommand;
        private ICleaningRobotStrategy _givenStrategy;
        private ICleaningRobotStrategy _backOffStrategy;
        private ICleaningRobotController _robotController;

        // Initializing classes for test
        [TestInitialize]
        public void Setup()
        {
            _mapSerializer = new RoomMapJsonSerializer();
            _map = new RoombaMap(_mapSerializer);
            _robotSerializer = new CleaningRobotJsonSerializer();
            _robot = new RoombaRobot(_map, _robotSerializer);
            _strategySerializer = new CleaningRobotStrategyJsonSerializer();
            _trCommand = new CleaningRobotTurnRightCommand(_robot);
            _tlCommand = new CleaningRobotTurnLeftCommand(_robot);
            _aCommand = new CleaningRobotAdvanceCommand(_robot);
            _bCommand = new CleaningRobotBackCommand(_robot);
            _cCommand = new CleaningRobotCleanCommand(_robot);
            _givenStrategy = new CleaningRobotResumableStrategy(_tlCommand, _trCommand, _aCommand, _cCommand, _strategySerializer);
            _backOffStrategy = new CleaningRobotBackOffStrategy(_tlCommand, _trCommand, _aCommand, _bCommand, _cCommand);
            _robotController = new RoombaRobotController(_map, _robot, _givenStrategy, _backOffStrategy);
        }

        // Loading cleaning robot state from Json file
        [TestMethod]
        public void LoadingRobotState()
        {
            // Arrange (in setup)

            // Act
            _robot.Load("test1.json");

            // Assert
            Assert.AreEqual(3, _robot.State.Location.X);
            Assert.AreEqual(0, _robot.State.Location.Y);
            Assert.AreEqual(CleaningRobotFacing.North, _robot.State.Facing);
            Assert.AreEqual(80, _robot.State.Battery);
        }

        // Loading map from Json file
        [TestMethod]
        public void LoadingMap()
        {
            // Arrange (in setup)

            // Act
            _map.Load("test1.json");

            // Assert
            Assert.AreEqual(16, _map.Cells.Length);
            Assert.AreEqual(4, _map.Cells.GetLength(0));
            Assert.AreEqual(4, _map.Cells.GetLength(1));
            Assert.AreEqual(RoomMapCellState.Cleanable, _map.Cells[0, 0]);
            Assert.AreEqual(RoomMapCellState.NotCleanable, _map.Cells[2, 1]);
            Assert.AreEqual(RoomMapCellState.Empty, _map.Cells[1, 3]);
        }

        // Robot simple actions
        [TestMethod]
        public void RobotSimpleActions()
        {
            // Arrange
            _map.Load("test1.json");
            _robot.Load("test1.json");

            // Act
            _robot.TurnLeft();
            _robot.Advance();
            _robot.Clean();
            _robot.Advance();
            _robot.TurnRight();
            _robot.Back();

            // Assert
            Assert.AreEqual(1, _robot.State.Location.X);
            Assert.AreEqual(1, _robot.State.Location.Y);
            Assert.AreEqual(66, _robot.State.Battery);
        }

        // Robot simple actions with commands
        [TestMethod]
        public void RobotSimpleActionsWithCommands()
        {
            // Arrange
            _map.Load("test1.json");
            _robot.Load("test1.json");

            // Act
            _tlCommand.Run();
            _aCommand.Run();
            _cCommand.Run();
            _aCommand.Run();
            _trCommand.Run();
            _bCommand.Run();

            // Assert
            Assert.AreEqual(1, _robot.State.Location.X);
            Assert.AreEqual(1, _robot.State.Location.Y);
            Assert.AreEqual(66, _robot.State.Battery);
        }

        // Loading all data by controller
        [TestMethod]
        public void LoadingController()
        {
            // Arrange (in setup)

            // Act
            _robotController.Load("test1.json");

            // Assert

            // Robot
            Assert.AreEqual(3, _robot.State.Location.X);
            Assert.AreEqual(0, _robot.State.Location.Y);
            Assert.AreEqual(CleaningRobotFacing.North, _robot.State.Facing);
            Assert.AreEqual(80, _robot.State.Battery);

            // Map
            Assert.AreEqual(16, _map.Cells.Length);
            Assert.AreEqual(4, _map.Cells.GetLength(0));
            Assert.AreEqual(4, _map.Cells.GetLength(1));
            Assert.AreEqual(RoomMapCellState.Cleanable, _map.Cells[0, 0]);
            Assert.AreEqual(RoomMapCellState.NotCleanable, _map.Cells[2, 1]);
            Assert.AreEqual(RoomMapCellState.Empty, _map.Cells[1, 3]);

            // Strategy
            Assert.AreEqual(8, _givenStrategy.Data.Length);
        }

        // Execute test 1
        [TestMethod]
        public void ExecuteTest1()
        {
            // Arrange
            _robotController.Load("test1.json");

            // Act
            _robotController.Run();

            // Assert
            Assert.AreEqual(2, _robot.State.Location.X);
            Assert.AreEqual(0, _robot.State.Location.Y);
            Assert.AreEqual(CleaningRobotFacing.East, _robot.State.Facing);
            Assert.AreEqual(54, _robot.State.Battery);
            Assert.AreEqual(3, _robot.State.MapPointsVisited.Count);
            Assert.AreEqual(2, _robot.State.MapPointsCleaned.Count);
        }

        // Execute test 2
        [TestMethod]
        public void ExecuteTest2()
        {
            // Arrange
            _robotController.Load("test2.json");

            // Act
            _robotController.Run();

            // Assert
            Assert.AreEqual(3, _robot.State.Location.X);
            Assert.AreEqual(2, _robot.State.Location.Y);
            Assert.AreEqual(CleaningRobotFacing.East, _robot.State.Facing);
            Assert.AreEqual(1040, _robot.State.Battery);
            Assert.AreEqual(4, _robot.State.MapPointsVisited.Count);
            Assert.AreEqual(3, _robot.State.MapPointsCleaned.Count);
        }

        // Compare Json results to sample (Test 1)
        [TestMethod]
        public void CompareJsonResultsTest1()
        {
            // Arrange
            _robotController.Load("test1.json");

            // Act
            _robotController.Run();
            _robotController.Save("test1_result_mine.json");
            string[] sampleLines = File.ReadAllLines("test1_result.json");
            string[] savedLines = File.ReadAllLines("test1_result_mine.json");

            // Assert
            Assert.AreEqual(true, sampleLines.SequenceEqual(savedLines));
        }

        // Compare Json results to sample (Test 2)
        [TestMethod]
        public void CompareJsonResultsTest2()
        {
            // Arrange
            _robotController.Load("test2.json");

            // Act
            _robotController.Run();
            _robotController.Save("test2_result_mine.json");
            string[] sampleLines = File.ReadAllLines("test2_result.json");
            string[] savedLines = File.ReadAllLines("test2_result_mine.json");

            // Assert
            Assert.AreEqual(true, sampleLines.SequenceEqual(savedLines));
        }
    }
}
