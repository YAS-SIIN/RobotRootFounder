using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfiCodeAssignment.Model
{
    public class TimeStep
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
        /// Visited position X
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Visited position Y
        /// </summary>
        public int PositionY { get; set; }
    }
}
