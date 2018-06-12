using System;
using System.IO;
using System.Linq;
using CleaningRobot.Domain.Abstract;
using Newtonsoft.Json.Linq;

namespace CleaningRobot.Json
{
    /// <summary>
    /// Cleaning robot strategy serializer
    /// </summary>
    public class CleaningRobotStrategyJsonSerializer : ICleaningRobotStrategySerializer
    {
        /// <summary>
        /// Deserializing strategy data
        /// </summary>
        public string[] Deserialize(string inputFileName)
        {
            string jsonText = File.ReadAllText(inputFileName);

            JObject jObject = JObject.Parse(jsonText);

            // Getting array length
            var jToken = jObject["commands"];
            int count = jToken.Count();

            // Creating array
            string[] data = new String[count];

            // Populating array
            for (int i = 0; i <= count - 1; i++)
            {
                data[i] = jToken[i].ToString();
            }

            return data;
        }
    }
}
