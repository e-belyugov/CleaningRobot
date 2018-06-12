namespace CleaningRobot.Domain.Entities
{
    /// <summary>
    /// Cleaning robot action result
    /// </summary>
    public enum CleaningRobotActionResult
    {
        Success,
        Obstacle,
        BatteryDrained
    }

    /// <summary>
    /// Cleaning robot facing
    /// </summary>
    public enum CleaningRobotFacing
    {
        North,
        South,
        West,
        East
    }

    /// <summary>
    /// Room map cell state
    /// </summary>
    public enum RoomMapCellState
    {
        Cleanable,
        NotCleanable,
        Empty
    }
}
