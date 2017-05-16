using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace batalha_naval
{
    class TileSet
    {
        public static List<Image> SplitImage(string image, int w, int h)
        {
            List<Image> f = new List<Image>();
            Bitmap      b = new Bitmap(image);

            int cols = b.Width  / w,
                rows = b.Height / h;

            for (int i = 0; i < rows; i++)
                for (int n = 0; n < cols; n++)
                    f.Add(b.Clone(new Rectangle(n * w, i * h, w, h), b.PixelFormat));

            return f;
        }

        public static List<Image> SplitImage(Image image, int w, int h)
        {
            List<Image> frames = new List<Image>();
            Bitmap b = new Bitmap(image);

            int cols = b.Width / w,
                rows = b.Height / h;

            for (int i = 0; i < rows; i++)
                for (int n = 0; n < cols; n++)
                    frames.Add(b.Clone(new Rectangle(n * w, i * h, w, h), b.PixelFormat));

            return frames;
        }
    }
}
