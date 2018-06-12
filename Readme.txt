
INSTRUCTIONS

This project is a Microsoft Visual Studio 2017 solution (solution file is CleaningRobot.sln). 
It can be opened and built in this IDE. Target framework is .NET Framework 4.6.1.

StartUp project is a console application which has two command line arguments saved in project options: 
"test1.json test1_result_mine.json". 

So you can just build and run application and see results in output directory:
 ..\CleaningRobot\CleaningRobot.Console\bin\Debug. 
You can also use bat file named cleaning_robot.bat located in output directory as well. 

Or you can run unit tests which are located in CleaningRobot.UnitTests project.

DESCRIPTION

DEPENDENCY INVERSION

Before starting project I thought about modelling given domain. Good style is to think about abstractions 
not implementations. I identified key entities that can interact with each other: Map, Robot and Strategy. 
And one more entity which controls general workflow - Controller. 

So higher level entity Controller should not depend on concrete implementations and should work only 
with appropriate interfaces. I created special library for abstractions CleaningRobot.Domain 
which also includes some general purpose implementations like enums, map point and robot state.

From technical point of view I needed some IoC container. My choice is Castle.Windsor 
and I put all resolving techniques in separate Resolver static class near console client application.
And when I need some behavor I simply inject appropriate interface in constructor.

STRATEGY AND COMMAND PATTERNS

Robot behavior is a good case for using Command pattern. Robot is controlled by commands
so I defined ICleaningRobotCommand with Run method which is implemented by different classes 
in separate library named CleaningRobot.Commands.

According to task description I figured out two strategies: given one and more complicated back off strategy.
Strategy pattern suggests algorithm encapsulation in each concrete strategy so I implemented these strategies
in separate libraries with command interfaces constructor injection.

SINGLE RESPONSIBILITY

At one of the first steps I implemented loading data from json file in each class (just to save time). 
But I didn't like this and felt that it needed refactoring. At last single responsibilty principle
moved me to implement separate serializers which will be injected when they are needed.
I located all Json specific serialization logic in CleaningRobot.Json library.

FINAL IMPLEMENTATIONS

Finally I needed to construct my robot, my map and robot controller.
All things concerning concrete Roomba robot are in CleaningRobot.RoombaRobot library with injections it needs.

UNIT TESTING

Last to mention but not last by value is unit testing. Moving step by step in project I testing different use cases
to ensure quality and cover key scenarios:

- loading map and robot state from Json;
- robot movements and command execution;
- running given and back off strategies;
- testing results correctness and saving.

All tests are implemented in special CleaningRobot.UnitTests project based on MSTest framework.
