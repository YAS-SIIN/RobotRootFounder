using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfiCodeAssignment.Model
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
        public Positon VisitedPositon { get; set; }
 
    }
}
