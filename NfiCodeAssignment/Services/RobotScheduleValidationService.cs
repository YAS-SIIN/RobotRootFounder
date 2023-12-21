using NfiCodeAssignment.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace NfiCodeAssignment.Services
{
    public class RobotScheduleValidationService
    {
        /// <summary>
        /// Validates whether there are no collisions between any of the robots in the schedule.
        /// </summary>
        /// <param name="schedule">The schedule, as read from a file. See the "Readme.pdf" file in the root folder of
        /// this project for more information about the structure/contents of this string. </param>
        /// <returns>Whether the schedule is valid. The schedule is valid if there are no collisions between any of the
        /// robots.</returns>
        public bool ValidateSchedule(string schedule)
        {
            var lines = ParseScheduleInputLines(schedule);

            // TODO: implement this function.
            // This function should return `true` if the schedule is valid (no collisions).
            // If there is a collision in any of the time steps, this function should return `false`.
            // Of course, you are allowed to create additional functions and classes. Don't put all code into one huge function!

            // Check the "Readme.pdf" file in the root folder of this project for instructions!
            // Once you have finished implementing this function and all test cases succeed, you are done.

            var robots = ParseRobots(lines);
            var timeSteps = ParseTimeSteps(lines);

            // Iterate through each time step
            for (int timeStepIndex = 0; timeStepIndex < timeSteps.Count; timeStepIndex++)
            {
                var currentStep = timeSteps[timeStepIndex];

                // Check collisions for each pair of robots
                for (int i = 0; i < robots.Count; i++)
                {
                    for (int j = i + 1; j < robots.Count; j++)
                    {
                        var robotA = robots[i];
                        var robotB = robots[j];

                        // Check for collision using the formula
                        if (DoRectanglesOverlap(
                                robotA.OriginX, robotA.OriginY, robotA.CurrentX, robotA.CurrentY,
                                robotB.OriginX, robotB.OriginY, robotB.CurrentX, robotB.CurrentY))
                        {
                            Console.WriteLine($"Collision detected at time step {timeStepIndex} between {robotA.Id} and {robotB.Id}.");
                            return false; // Collision found, schedule is invalid
                        }
                    }
                }

                // Update robot positions for the next time step

                var robot = robots.Find(r => r.Id == currentStep.RobotId);
                robot.CurrentX = currentStep.PositionX;
                robot.CurrentY = currentStep.PositionY;

            }

            return true; // No collisions found, schedule is valid
        }

        /// <summary>
        /// Extracts the lines from the schedule string.
        /// Removes all comments and empty lines.
        /// </summary>
        private List<string> ParseScheduleInputLines(string schedule)
        {
            return schedule
                .Replace("\r\n", "\n")
                .Split('\n')
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.StartsWith("#"))
                .ToList();
        }


        /// <summary>
        /// Parse robots
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<Robot> ParseRobots(List<string> lines)
        {
            var robotLines = lines.TakeWhile(line => !line.Contains("#")).ToList();
            return robotLines.Where(line => line.Split(',').Length == 3).Select(line =>
            {
                var parts = line.Split(',').Select(part => part.Trim()).ToList();
                return new Robot
                {
                    Id = parts[0],
                    OriginX = int.Parse(parts[1]),
                    OriginY = int.Parse(parts[2]),
                    CurrentX = int.Parse(parts[1]), // Initial position is the origin
                    CurrentY = int.Parse(parts[2])
                };
            }).ToList();
        }


        /// <summary>
        /// Parse time steps
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<TimeStep> ParseTimeSteps(List<string> lines)
        {
            var timeStepLines = lines.SkipWhile(line => !line.Contains("#")).Skip(1).ToList();
            return timeStepLines.Select(line =>
            {
                var parts = line.Split(',').Select(part => part.Trim()).ToList();
                return new TimeStep
                {
                    StepNumber = int.Parse(parts[0]),
                    RobotId = parts[1],
                    PositionX = int.Parse(parts[2]),
                    PositionY = int.Parse(parts[3])
                };
            }).ToList();
        }

        /// <summary>
        /// Rectangle overlap formula
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <returns></returns>
        private bool DoRectanglesOverlap(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            return x1 <= x4 && x2 >= x3 && y1 <= y4 && y2 >= y3;
        }

    }


}