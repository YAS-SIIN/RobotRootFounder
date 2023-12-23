using NfiCodeAssignment.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfiCodeAssignment.Models
{
    public class RobotTimeStep
    {
        /// <summary>
        /// Step number
        /// </summary>
        public int StepNumber { get; set; }

        /// <summary>
        /// Robot identity key
        /// </summary>
        public string RobotId { get; set; }

        /// <summary>
        /// Visited position
        /// </summary>
        public Position VisitedPosition { get; set; }
 
    }
}
