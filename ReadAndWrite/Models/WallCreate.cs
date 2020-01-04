using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndWrite
{
    class WallCreate
    {
    
        //墙ID
        public int WallId { get; set; }
        //墙高
        public double WallHeight { get; set; }

        //墙线的起始点
        public double StartPointX { get; set; }
        public double StartPointY { get; set; }
        public double StartPointZ { get; set; }


        //墙线的终点
        public double EndPointX { get; set; }
        public double EndPointY { get; set; }
        public double EndPointZ { get; set; }

    }
}
