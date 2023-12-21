// You may not change this file.

using System.Collections.Generic;
using System.Text;
using NfiCodeAssignment.Services;
using NUnit.Framework;

namespace NfiCodeAssignmentTest
{
    public class Tests
    {
        private const string RobotA = "Robot A";
        private const string RobotB = "Robot B";

        private static readonly IReadOnlyList<(string, int, int)> DefaultRobots = new[]
        {
            (RobotA, 0, 0),
            (RobotB, 10, 10),
        };

        [Test]
        public void ScheduleWithOnlyOneRobotIsValid()
        {
            // Arrange
            var robots = new[]
            {
                (RobotA, 0, 0),
            };
            var timeSteps = new[]
            {
                (0, RobotA, 10, 10),
            };
            var schedule = MakeSchedule(robots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.True(scheduleIsValid);
        }

        [TestCase(-1, -1, -2, -2)]
        [TestCase(11, -1, 12, -2)]
        [TestCase(-1, 11, -2, 12)]
        [TestCase(11, 11, 12, 12)]
        [TestCase(-1, -1, 11, -2)]
        [TestCase(-1, -1, -2, 11)]
        [TestCase(11, 11, -1, 12)]
        [TestCase(11, 11, 12, -1)]
        public void ScheduleWithNoCollisionsIsValidTest(int originBx, int originBy, int robotBx, int robotBy)
        {
            // Arrange
            var robots = new[]
            {
                (RobotA, 0, 0),
                (RobotB, originBx, originBy),
            };
            var timeSteps = new[]
            {
                (0, RobotA, 10, 10),
                (0, RobotB, robotBx, robotBy),
            };
            var schedule = MakeSchedule(robots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.True(scheduleIsValid);
        }

        [TestCase(-1, -1, 1, 1)]
        [TestCase(11, -1, 9, 1)]
        [TestCase(-1, 11, 1, 9)]
        [TestCase(11, 11, 9, 9)]
        [TestCase(-1, 4, 1, 6)]
        [TestCase(4, -1, 6, 1)]
        [TestCase(11, 4, 9, 6)]
        [TestCase(4, 11, 6, 9)]
        [TestCase(-1, 4, 11, 6)]
        [TestCase(4, -1, 6, 11)]
        public void ScheduleWithOverlappingAreasIsInvalidTest(int originBx, int originBy, int robotBx, int robotBy)
        {
            // Arrange
            var robots = new[]
            {
                (RobotA, 0, 0),
                (RobotB, originBx, originBy),
            };
            var timeSteps = new[]
            {
                (0, RobotA, 10, 10),
                (0, RobotB, robotBx, robotBy),
            };
            var schedule = MakeSchedule(robots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.False(scheduleIsValid);
        }

        [Test]
        public void ScheduleWithSharedEdgeIsInvalidTest()
        {
            // Arrange
            var timeSteps = new[]
            {
                // The robots touch on edge `x = 5`, so they collide
                (0, RobotA, 5, 9),
                (0, RobotB, 5, 1),
            };
            var schedule = MakeSchedule(DefaultRobots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.False(scheduleIsValid);
        }

        [Test]
        public void ScheduleWithACollisionsCausedByASharedPointIsInvalidTest()
        {
            // Arrange
            var timeSteps = new[]
            {
                // The robots have point (5, 5) in common
                (0, RobotA, 5, 5),
                (0, RobotB, 5, 5),
            };
            var schedule = MakeSchedule(DefaultRobots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.False(scheduleIsValid);
        }

        [Test]
        [Timeout(5_000)]
        public void ScheduleWithAHundredRobotsCanBeValidatedWithinFiveSeconds()
        {
            // Note: 5 seconds to validate a schedule with 100 robots is very lenient.
            // This should easily be achievable within 0.1 second without doing anything special.
            
            // Arrange
            const int totalRobots = 100;

            var robots = new List<(string, int, int)>();
            var timeSteps = new List<(int, string, int, int)>();
            for (var i = 0; i < totalRobots; i++)
            {
                var robotId = i.ToString();
                robots.Add((robotId, i * 4, i * 4));
                timeSteps.Add((0, robotId, i * 4 + 2, i * 4 + 2));
            }

            var schedule = MakeSchedule(robots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.True(scheduleIsValid);
        }

        [Test]
        public void ScheduleWithMultipleTimeStepsIsValidatedCorrectly_NoCollisionTest()
        {
            // Arrange
            var timeSteps = new[]
            {
                (0, RobotA, 9, 9),
                (0, RobotB, 11, 11),

                (1, RobotA, -1, -1),
                (1, RobotB, 1, 1),

                (2, RobotA, 9, 9),
                (2, RobotB, 11, 11),

                (3, RobotA, -1, -1),
                (3, RobotB, 1, 1),
            };
            var schedule = MakeSchedule(DefaultRobots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.True(scheduleIsValid);
        }

        [Test]
        public void ScheduleWithMultipleTimeStepsIsValidatedCorrectly_CollisionTest()
        {
            // Arrange
            var timeSteps = new[]
            {
                (0, RobotA, 1, 1),
                (0, RobotB, 9, 9),
                
                (1, RobotA, 9, 9), // Collision
                (1, RobotB, 1, 1), // Collision
                
                (2, RobotA, 1, 1),
                (2, RobotB, 9, 9),
            };
            var schedule = MakeSchedule(DefaultRobots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.False(scheduleIsValid);
        }

        [Test]
        public void ScheduleWithShuffledTimeStepsIsValidatedCorrectly_NoCollisionTest()
        {
            // Arrange
            var timeSteps = new[]
            {
                (2, RobotA, 8, 8),
                (1, RobotB, 6, 6),
                (0, RobotA, 3, 3),
                (1, RobotA, 5, 5),
                (0, RobotB, 4, 4),
                (2, RobotB, 9, 9),
            };
            var schedule = MakeSchedule(DefaultRobots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.True(scheduleIsValid);
        }

        [Test]
        public void ScheduleWithShuffledTimeStepsIsValidatedCorrectly_CollisionTest()
        {
            // Arrange
            var timeSteps = new[]
            {
                (2, RobotA, 1, 1),
                (1, RobotB, 1, 1), // Collision
                (0, RobotA, 1, 1),
                (1, RobotA, 9, 9), // Collision
                (0, RobotB, 9, 9),
                (2, RobotB, 9, 9),
            };
            var schedule = MakeSchedule(DefaultRobots, timeSteps);
            var scheduleValidator = new RobotScheduleValidationService();

            // Act
            var scheduleIsValid = scheduleValidator.ValidateSchedule(schedule);

            // Assert
            Assert.False(scheduleIsValid);
        }

        private static string MakeSchedule(
            IReadOnlyList<(string, int, int)> robots,
            IReadOnlyList<(int, string, int, int)> timeSteps)
        {
            var sb = new StringBuilder();
            sb.Append(robots.Count).Append('\n');
            sb.Append(timeSteps.Count / robots.Count).Append('\n');

            foreach (var (robotId, x, y) in robots)
            {
                sb.Append(robotId).Append(", ").Append(x).Append(", ").Append(y);
                sb.Append('\n');
            }

            foreach (var (timeStepIndex, robotId, x, y) in timeSteps)
            {
                sb.Append(timeStepIndex).Append(", ").Append(robotId).Append(", ").Append(x).Append(", ").Append(y);
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}