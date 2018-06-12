using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleaningRobot.Domain.Entities;

namespace CleaningRobot.Json
{
    /// <summary>
    /// Cleaning robot final State
    /// </summary>
    struct CleaningRobotFinalState
    {
        // X coordinate
        public int X;

        // Y coordinate
        public int Y;

        // Facing
        public string facing;
    }

    /// <summary>
    /// Cleaning robot for Json
    /// </summary>
    class CleaningRobotForJson
    {
        // List of map points visited
        public IList<RoomMapPoint> visited = new List<RoomMapPoint>();

        // List of map points cleaned
        public IList<RoomMapPoint> cleaned = new List<RoomMapPoint>();

        // Final state
        public CleaningRobotFinalState final;

        // Battery
        public int battery;

    }
}
