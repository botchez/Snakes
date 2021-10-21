using System;
using System.Collections.Generic;
using System.Text;

namespace goddamnedsnakes
{
    class head
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<int> BodX { get; set; }
        public List<int> BodY { get; set; }
        public string dir { get; set; }

        public int Lbod { get; set; }

        public head(int x, int y, string dire, int body, List<int> bodyX, List<int> bodyY)
        {
            X = x;
            Y = y;
            dir = dire;
            BodX = bodyX;
            BodY = bodyY;
            Lbod = body;
        }

    }
}
