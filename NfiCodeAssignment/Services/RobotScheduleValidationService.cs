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

            List<Robot> robots = ParseRobots(lines);
            List<RobotPositon> robotPositions = ParseRobotPositions(lines);

            // Iterate through each time step
            foreach (RobotPositon currentRobotPosition in robotPositions)
            {  
                //Get another robots
                List<RobotPositon> otherRobots = robotPositions.Where(x => x.RobotId != currentRobotPosition.RobotId).ToList();
                var currentRobotNextPosition = robotPositions.Where(x => x.RobotId == currentRobotPosition.RobotId).Skip(1).FirstOrDefault();

                // Check collisions for each pair of robots
                foreach (RobotPositon nextRobotPosition in otherRobots)
                {
                    var nextRobotNextPosition = otherRobots.Where(x => x.RobotId == nextRobotPosition.RobotId).Skip(1).FirstOrDefault();
                    // Check for collision using the formula
                    if (DoRectanglesOverlap(
                            currentRobotPosition.Positon.PositionX, currentRobotPosition.Positon.PositionY, currentRobotNextPosition.Positon.PositionX, currentRobotNextPosition.Positon.PositionY,
                            nextRobotPosition.Positon.PositionX, nextRobotPosition.Positon.PositionY, nextRobotNextPosition.Positon.PositionX, nextRobotNextPosition.Positon.PositionY))
                    {
                        Console.WriteLine($"Collision detected at time step {currentRobotPosition.StepNumber} between {currentRobotPosition.RobotId} and {nextRobotPosition.RobotId}.");
                        return false; // Collision found, schedule is invalid
                    }
                }

                //// Check collisions for each pair of robots
                //foreach (Robot robot1 in robots)
                //{
                //    //Get another robots
                //    List<Robot> otherRobots1 = robots.Where(x => x.Id != robot1.Id).ToList();
                //    foreach (Robot robot2 in otherRobots)
                //    {
                //        // Check for collision using the formula
                //        if (DoRectanglesOverlap(
                //                robot1.OriginX, robot1.OriginY, robot1.CurrentX, robot1.CurrentY,
                //                robot2.OriginX, robot2.OriginY, robot2.CurrentX, robot2.CurrentY))
                //        {
                //            Console.WriteLine($"Collision detected at time step {currentRobotPosition.StepNumber} between {robot1.Id} and {robot2.Id}.");
                //            return false; // Collision found, schedule is invalid
                //        }
                //    }
                //}


                //// Check collisions for each pair of robots
                //for (int i = 0; i < robots.Count; i++)
                //{
                //    for (int j = i + 1; j < robots.Count; j++)
                //    {
                //        var robotA = robots[i];
                //        var robotB = robots[j];

                //        // Check for collision using the formula
                //        if (DoRectanglesOverlap(
                //                robotA.OriginX, robotA.OriginY, robotA.CurrentX, robotA.CurrentY,
                //                robotB.OriginX, robotB.OriginY, robotB.CurrentX, robotB.CurrentY))
                //        {
                //            Console.WriteLine($"Collision detected at time step {currentStep.StepNumber} between {robotA.Id} and {robotB.Id}.");
                //            return false; // Collision found, schedule is invalid
                //        }
                //    }
                //}

                // Update robot positions for the next time step

                //var robot = robots.Find(r => r.Id == currentStep.RobotId);
                //robot.CurrentX = currentStep.PositionX;
                //robot.CurrentY = currentStep.PositionY;
        
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
                    OriginX = int.Parse(parts[1]),
                    OriginY = int.Parse(parts[2]),
                    CurrentX = int.Parse(parts[1]), // Initial position is the origin
                    CurrentY = int.Parse(parts[2])  // Initial position is the origin
                };
            }).ToList();
        }


        /// <summary>
        /// Parse time step lists
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<RobotPositon> ParseRobotPositions(List<string> lines)
        { 
            return lines.Where(line => line.Split(',').Length == 4).Select(line =>
            {
                var parts = line.Split(',').Select(part => part.Trim()).ToList();
                return new RobotPositon
                {
                    StepNumber = int.Parse(parts[0]),
                    RobotId = parts[1],
                    Positon = new Positon(int.Parse(parts[2]), int.Parse(parts[3]))
                };
            }).ToList();
        }

        /// <summary>
        /// Rectangle overlap formula
        /// </summary>
        /// <param name="Robot1OriginX">Robot 1 origin X position</param>
        /// <param name="Robot1OriginY">Robot 1 origin Y position</param>
        /// <param name="Robot1CurrentX">Robot 1 ccurren X position</param>
        /// <param name="Robot1CurrentY">Robot 1 ccurren Y position</param>
        /// <param name="Robot2OriginX">Robot 2 origin X position</param>
        /// <param name="Robot2OriginY">Robot 2 origin Y position</param>
        /// <param name="Robot2CurrentX">Robot 2 ccurren X position</param>
        /// <param name="Robot2CurrentY">Robot 2 ccurren Y position</param>
        /// <returns></returns>
        private bool DoRectanglesOverlap(int Robot1OriginX, int Robot1OriginY, int Robot1CurrentX, int Robot1CurrentY, int Robot2OriginX, int Robot2OriginY, int Robot2CurrentX, int Robot2CurrentY)
        {
            return Robot1OriginX <= Robot2CurrentX && Robot1CurrentX >= Robot2OriginX && Robot1OriginY <= Robot2CurrentY && Robot1CurrentY >= Robot2OriginY;
        }

    }


}