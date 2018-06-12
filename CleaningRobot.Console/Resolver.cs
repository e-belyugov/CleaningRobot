using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CleaningRobot.BackOffStrategy;
using CleaningRobot.Commands;
using CleaningRobot.Domain.Abstract;
using CleaningRobot.Json;
using CleaningRobot.ResumableStrategy;
using CleaninigRobot.RoombaRobot;

namespace CleaningRobot.Console
{
    /// <summary>
    /// Static resolver for cleaning robot interfaces
    /// </summary>
    public static class Resolver
    {
        // Room map
        public static readonly ICleaningRobotController RobotController;

        /// <summary>
        /// Constructor
        /// </summary>
        static Resolver()
        {
            // Container 
            var container = new WindsorContainer();

            // Map serializer
            container.Register(Component.For<IRoomMapSerializer>().ImplementedBy<RoomMapJsonSerializer>());

            // Room map
            container.Register(Component.For<IRoomMap>().ImplementedBy<RoombaMap>());

            // Robot serializer
            container.Register(Component.For<ICleaningRobotSerializer>().ImplementedBy<CleaningRobotJsonSerializer>());

            // Robot
            container.Register(Component.For<ICleaningRobot>().ImplementedBy<RoombaRobot>());

            // Commands
            container.Register(Component.For<ICleaningRobotCommand>()
                .ImplementedBy<CleaningRobotTurnLeftCommand>()
                .Named("turnLeftCommand"));
            container.Register(Component.For<ICleaningRobotCommand>()
                .ImplementedBy<CleaningRobotTurnRightCommand>()
                .Named("turnRightCommand"));
            container.Register(Component.For<ICleaningRobotCommand>()
                .ImplementedBy<CleaningRobotAdvanceCommand>()
                .Named("advanceCommand"));
            container.Register(Component.For<ICleaningRobotCommand>()
                .ImplementedBy<CleaningRobotBackCommand>()
                .Named("backCommand"));
            container.Register(Component.For<ICleaningRobotCommand>()
                .ImplementedBy<CleaningRobotCleanCommand>()
                .Named("cleanCommand"));

            // Strategy serializer
            container.Register(Component.For<ICleaningRobotStrategySerializer>().ImplementedBy<CleaningRobotStrategyJsonSerializer>());

            // Strategies
            container.Register(Component.For<ICleaningRobotStrategy>()
                .ImplementedBy<CleaningRobotResumableStrategy>()
                .Named("resumableStrategy")
                .DependsOn(Dependency.OnComponent("turnLeftCommand", "turnLeftCommand"))
                .DependsOn(Dependency.OnComponent("turnRightCommand", "turnRightCommand"))
                .DependsOn(Dependency.OnComponent("advanceCommand", "advanceCommand"))
                .DependsOn(Dependency.OnComponent("cleanCommand", "cleanCommand"))
                );
            container.Register(Component.For<ICleaningRobotStrategy>()
                .ImplementedBy<CleaningRobotBackOffStrategy>()
                .Named("backOffStrategy")
                .DependsOn(Dependency.OnComponent("turnLeftCommand", "turnLeftCommand"))
                .DependsOn(Dependency.OnComponent("turnRightCommand", "turnRightCommand"))
                .DependsOn(Dependency.OnComponent("advanceCommand", "advanceCommand"))
                .DependsOn(Dependency.OnComponent("backCommand", "backCommand"))
                .DependsOn(Dependency.OnComponent("cleanCommand", "cleanCommand"))
                );

            // Robot controller
            container.Register(Component.For<ICleaningRobotController>().ImplementedBy<RoombaRobotController>()
                .DependsOn(Dependency.OnComponent("givenStrategy", "resumableStrategy"))
                .DependsOn(Dependency.OnComponent("backOffStrategy", "backOffStrategy"))
                );
            RobotController = container.Resolve<ICleaningRobotController>();
        }
    }
}
