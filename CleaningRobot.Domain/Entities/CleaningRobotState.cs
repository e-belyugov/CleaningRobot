using System.Collections.Generic;

namespace CleaningRobot.Domain.Entities
{
    /// <summary>
    /// Cleaning robot state 
    /// </summary>
    public class CleaningRobotState
    {
        // Private fields
        private IList<RoomMapPoint> _mapPointsVisited = new List<RoomMapPoint>();
        private IList<RoomMapPoint> _mapPointsCleaned = new List<RoomMapPoint>();

        /// <summary>
        /// Location on the map
        /// </summary>
        public RoomMapPoint Location { get; set; }

        /// <summary>
        /// Robot facing
        /// </summary>
        public CleaningRobotFacing Facing { get; set; }

        /// <summary>
        /// Battery state
        /// </summary>
        public int Battery { get; set; }

        /// <summary>
        /// List of map points visited
        /// </summary>
        public IList<RoomMapPoint> MapPointsVisited
        {
            get
            {
                return _mapPointsVisited;
            }
            set
            {
                _mapPointsVisited = MapPointsVisited;
            }
        }

        /// <summary>
        /// List of map points cleaned
        /// </summary>
        public IList<RoomMapPoint> MapPointsCleaned
        {
            get
            {
                return _mapPointsCleaned;
            }
            set
            {
                _mapPointsCleaned = MapPointsCleaned;
            }
        }
    }
}
