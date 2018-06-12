using CleaningRobot.Domain.Abstract;

namespace CleaningRobot.Console
{
    class Program
    {
        // Robot controller
        private static ICleaningRobotController _robotController;

        static void Main(string[] args)
        {
            _robotController = Resolver.RobotController;
            _robotController.Load(args[0]);
            _robotController.Run();
            _robotController.Save(args[1]);

            // Waiting for user input to close
            //System.Console.ReadKey();
        }
    }
}
