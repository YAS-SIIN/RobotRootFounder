﻿using NfiCodeAssignment.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

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

            List<Robot> robots = ParseRobots(lines);
            List<RobotTimeStep> robotTimeSteps = ParseRobotTimeSteps(lines);

            var timeSteps = robotTimeSteps.Select(a => a.StepNumber).Distinct().ToList();
            foreach (var timeStep in timeSteps)
            {
                var currentRobotTimeSteps = robotTimeSteps.Where(x => x.StepNumber == timeStep).ToList();

                // Iterate through each time step
                foreach (RobotTimeStep currentRobotTimeStep in currentRobotTimeSteps)
                {
                    Robot currentRobot = robots.Where(x => x.Id == currentRobotTimeStep.RobotId).FirstOrDefault();

                    //Get other robots time steps
                    List<RobotTimeStep> otherRobotsTimeSteps = currentRobotTimeSteps.Where(x => x.RobotId != currentRobotTimeStep.RobotId).ToList();
                    // Check collisions for each pair of robots
                    foreach (RobotTimeStep otherRobotTimeSteps in otherRobotsTimeSteps)
                    {
                        Robot otherRobot = robots.Where(x => x.Id == otherRobotTimeSteps.RobotId).FirstOrDefault();
                        // Check for collision using the formula
                        if (DoRectanglesOverlap(
                                currentRobot.OriginPositon, currentRobotTimeStep.VisitedPositon, otherRobot.OriginPositon, otherRobotTimeSteps.VisitedPositon))
                        {
                            Console.WriteLine($"Collision detected at time step {currentRobotTimeStep.StepNumber} between {currentRobotTimeStep.RobotId} and {otherRobotTimeSteps.RobotId}.");
                            return false; // Collision found, schedule is invalid
                        }
                    }
                }

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
        /// Parse robot lists
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<Robot> ParseRobots(List<string> lines)
        {
            return lines.Where(line => line.Split(',').Length == 3).Select(line =>
            {
                var parts = line.Split(',').Select(part => part.Trim()).ToList();
                return new Robot
                {
                    Id = parts[0],
                    OriginPositon = new Positon(int.Parse(parts[1]), int.Parse(parts[2]))
                };
            }).ToList();
        }


        /// <summary>
        /// Parse time step lists
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<RobotTimeStep> ParseRobotTimeSteps(List<string> lines)
        {
            return lines.Where(line => line.Split(',').Length == 4).Select(line =>
            {
                var parts = line.Split(',').Select(part => part.Trim()).ToList();
                return new RobotTimeStep
                {
                    StepNumber = int.Parse(parts[0]),
                    RobotId = parts[1],
                    VisitedPositon = new Positon(int.Parse(parts[2]), int.Parse(parts[3]))
                };
            }).ToList();
        }

        /// <summary>
        /// Rectangle overlap formula
        /// </summary>
        /// <param name="robot1OriginPositon">Robot 1 origin position</param>
        /// <param name="robot1CurrentPositon">Robot 1 ccurren position</param>
        /// <param name="robot2OriginPositon">Robot 2 origin position</param>
        /// <param name="robot2CurrentPositon">Robot 2 ccurren position</param>
        /// <returns></returns>
        private bool DoRectanglesOverlap(Positon robot1OriginPositon, Positon robot1CurrentPositon, Positon robot2OriginPositon, Positon robot2CurrentPositon)
        {
            int maxRobot1X = Math.Max(robot1OriginPositon.PositionX, robot1CurrentPositon.PositionX);
            int maxRobot2X = Math.Max(robot2OriginPositon.PositionX, robot2CurrentPositon.PositionX);
            int maxRobot1Y = Math.Max(robot1OriginPositon.PositionY, robot1CurrentPositon.PositionY);
            int maxRobot2Y = Math.Max(robot2OriginPositon.PositionY, robot2CurrentPositon.PositionY);

            int minRobot1X = Math.Min(robot1OriginPositon.PositionX, robot1CurrentPositon.PositionX);
            int minRobot2X = Math.Min(robot2OriginPositon.PositionX, robot2CurrentPositon.PositionX);
            int minRobot1Y = Math.Min(robot1OriginPositon.PositionY, robot1CurrentPositon.PositionY);
            int minRobot2Y = Math.Min(robot2OriginPositon.PositionY, robot2CurrentPositon.PositionY);

            var x1 = maxRobot1X >= minRobot2X && minRobot1Y <= maxRobot2Y;
            var x2 = maxRobot1X >= minRobot2X && maxRobot1Y >= minRobot2Y;
            var y1 = minRobot1X <= maxRobot2X && maxRobot1Y >= minRobot2Y;
            var y2 = minRobot1X <= maxRobot2X && minRobot1Y <= maxRobot2Y;

            return x1 && x2 && y1 && y2;
        }


    }


}