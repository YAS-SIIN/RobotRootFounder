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
        /// Origin position
        /// </summary>
        public Position OriginPosition { get; set; }
 
    }
}
