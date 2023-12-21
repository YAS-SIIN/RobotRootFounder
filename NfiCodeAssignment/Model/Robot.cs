using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfiCodeAssignment.Model
{
    public class Robot
    {
        /// <summary>
        /// Robot identity key
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Origin X position of robot
        /// </summary>
        public int OriginX { get; set; }

        /// <summary>
        /// Origin Y position of robot
        /// </summary>
        public int OriginY { get; set; }

        /// <summary>
        /// Current X position of robot
        /// </summary>
        public int CurrentX { get; set; }

        /// <summary>
        /// Current Y position of robot
        /// </summary>
        public int CurrentY { get; set; }
    }
}
