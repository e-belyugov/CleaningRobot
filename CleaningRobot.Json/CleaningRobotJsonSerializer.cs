using System.Collections.Generic;
using System.IO;
using System.Linq;
using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CleaningRobot.Json
{
    /// <summary>
    /// Cleaning robot Json serializer
    /// </summary>
    public class CleaningRobotJsonSerializer : ICleaningRobotSerializer
    {
        // Private fields
        private readonly CleaningRobotState _state = new CleaningRobotState();

        /// <summary>
        /// Serializing cleaning robot state
        /// </summary>
        public void Serialize(CleaningRobotState cleaningRobotState, string outputFileName)
        {
            CleaningRobotForJson robotJson = new CleaningRobotForJson();

            // Sorting lists by attributes
            robotJson.visited = ((List<RoomMapPoint>)cleaningRobotState.MapPointsVisited)
                .OrderBy(l => l.X).ThenBy(l => l.Y).ToList();
            robotJson.cleaned = ((List<RoomMapPoint>)cleaningRobotState.MapPointsCleaned)
                .OrderBy(l => l.X).ThenBy(l => l.Y).ToList();

            robotJson.final.X = cleaningRobotState.Location.X;
            robotJson.final.Y = cleaningRobotState.Location.Y;
            robotJson.battery = cleaningRobotState.Battery;

            // Facing
            string facing = "";
            switch (cleaningRobotState.Facing)
            {
                case CleaningRobotFacing.North:
                    facing = "N";
                    break;
                case CleaningRobotFacing.East:
                    facing = "E";
                    break;
                case CleaningRobotFacing.South:
                    facing = "S";
                    break;
                case CleaningRobotFacing.West:
                    facing = "W";
                    break;
            }
            robotJson.final.facing = facing;

            // Write file
            string output = JsonConvert.SerializeObject(robotJson, Formatting.Indented);
            File.WriteAllText(outputFileName, output);
        }

        /// <summary>
        /// Deserializing cleaning robot state
        /// </summary>
        public CleaningRobotState Deserialize(string inputFileName)
        {
            string jsonText = File.ReadAllText(inputFileName);

            JObject jObject = JObject.Parse(jsonText);
            var jToken = jObject["start"];

            // Location
            RoomMapPoint roomMapPoint = new RoomMapPoint();
            roomMapPoint.X = (int)jToken["X"];
            roomMapPoint.Y = (int)jToken["Y"];
            _state.Location = roomMapPoint;

            // Facing
            string facing = jToken["facing"].ToString();
            switch (facing)
            {
                case "N":
                    _state.Facing = CleaningRobotFacing.North;
                    break;
                case "S":
                    _state.Facing = CleaningRobotFacing.South;
                    break;
                case "E":
                    _state.Facing = CleaningRobotFacing.East;
                    break;
                case "W":
                    _state.Facing = CleaningRobotFacing.West;
                    break;
                default:
                    _state.Facing = CleaningRobotFacing.North;
                    break;
            }

            // Battery
            jToken = jObject["battery"];
            _state.Battery = (int)jToken;

            // Lists initializing
            _state.MapPointsVisited.Clear();
            _state.MapPointsCleaned.Clear();
            _state.MapPointsVisited.Add(roomMapPoint); // Add start location to visited list

            return _state;
        }
    }
}
