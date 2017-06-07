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
        //private Size _size;
        public Size Size { get; private set; }

        //private Point _point;
        public Point Point { get; private set; }

        //private Image _image;
        public Image Image { get; private set; }

        /*
        public BoatData()
        {
            Image = null;
            Point = new Point(-1, -1);
            Size  = new Size(-1, -1);
        }
        */

        public BoatData(Image img, Point p) : this()
        {
            Image = img;
            Size = img.Size;
            Point = p;
        }
    }
}
