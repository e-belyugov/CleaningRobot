using System.IO;
using System.Linq;
using CleaningRobot.Domain.Abstract;
using CleaningRobot.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace CleaningRobot.Json
{
    /// <summary>
    /// Cleaning robot Json serializer
    /// </summary>
    public class RoomMapJsonSerializer : IRoomMapSerializer
    {
        /// <summary>
        /// Deserializing map cells
        /// </summary>
        public RoomMapCellState[,] Deserialize(string inputFileName)
        {
            string jsonText = File.ReadAllText(inputFileName);

            JObject jObject = JObject.Parse(jsonText);

            // Getting array length
            var jToken = jObject["map"];
            int rowCount = jToken.Count();
            int columnCount = jToken[0].Count();

            // Creating array
            RoomMapCellState[,] cells = new RoomMapCellState[rowCount, columnCount];

            // Populating array
            for (int i = 0; i <= rowCount - 1; i++)
            {
                for (int j = 0; j <= columnCount - 1; j++)
                {
                    string value = jToken[j][i].ToString();

                    switch (value)
                    {
                        case "S":
                            cells[i, j] = RoomMapCellState.Cleanable;
                            break;
                        case "C":
                            cells[i, j] = RoomMapCellState.NotCleanable;
                            break;
                        case "null":
                            cells[i, j] = RoomMapCellState.Empty;
                            break;
                    }
                }
            }

            return cells;
        }
    }
}
