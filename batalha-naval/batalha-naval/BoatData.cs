using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace batalha_naval
{
    public struct BoatData
    {
        public Size Size { get; private set; }
        public Point Point { get; private set; }
        public Image Image { get; private set; }

        public BoatData(Image img, Point p) : this()
        {
            Image = img;
            Size = img.Size;
            Point = p;
        }
    }
}
