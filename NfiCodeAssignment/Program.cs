// You may not change this file.

using System;
using System.IO;
using NfiCodeAssignment.Services;

namespace NfiCodeAssignment
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            string filePath;
            if (args.Length != 1)
            {
                Console.WriteLine("Supply exactly 1 argument to this program with the path to an input file.");
                Console.WriteLine("Falling back to default file: 'SampleInputWithCollision.txt'");
                filePath = "SampleInputWithCollision.txt";
            }
            else
            {
                filePath = args[0];
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"The file '{filePath}' does not exists!");
                return 1;
            }

            var fileContent = File.ReadAllText(filePath);
            var scheduleValidator = new RobotScheduleValidationService();
            var scheduleIsValid = scheduleValidator.ValidateSchedule(fileContent);

            if (scheduleIsValid)
            {
                Console.WriteLine($"The schedule `{filePath}` is valid. No collisions were found.");
                return 0;
            }
            else
            {
                Console.WriteLine($"The schedule `{filePath}` is invalid! At least one collision was found.");
                return 2;
            }
        }
    }
}