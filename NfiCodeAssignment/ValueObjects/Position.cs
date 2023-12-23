using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfiCodeAssignment.ValueObjects
{
    public class Position
    {
        /// <summary>
        /// Position X
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Position Y
        /// </summary>
        public int PositionY { get; set; }

        public Position
            (int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
    }
}
