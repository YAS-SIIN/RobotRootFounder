using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfiCodeAssignment.Model
{
    public class Positon
    {
        /// <summary>
        /// Position X
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Position Y
        /// </summary>
        public int PositionY { get; set; }

        public Positon(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
    }
}
