using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace batalha_naval
{
    public partial class GameForm : Form
    {
        public const int                   DEFAULT_TILESIZE = 32,
                                           CELL_SIZE        = 40,
                                           FPS              = 1;

        public const string                RESOURCES_FOLDER     = "../../../../Resources/";
        public static readonly string[]    WATER_SPLASHES_SOUND = new string[] { "watersplash.mp3", "watersplash2.mp3" };
        public static readonly List<Image> WATER_SPLASH_FRAMES  = TileSet.SplitImage(RESOURCES_FOLDER + "splash.png", DEFAULT_TILESIZE, DEFAULT_TILESIZE);
        public static readonly List<Image> WATER_TILE_FRAMES    = TileSet.SplitImage(RESOURCES_FOLDER + "water_tiles.png", DEFAULT_TILESIZE, DEFAULT_TILESIZE);

        private bool inside = false, changeWater = true;
        private Point cell  = Point.Empty;

        public GameForm()
        {
            InitializeComponent();

            Timer t    = new Timer();
            t.Interval = 1000 / FPS;
            t.Tick    += FrameTick;
            t.Start();
        }

        Random r = new Random();
        private void board_Paint(object sender, PaintEventArgs e)
        {
            if (changeWater)
            {
                for (int n = 0; n < 10; n++)
                    for (int i = 0; i < 10; i++)
                    {
                        double f = r.NextDouble();
                        int index;

                        if (f < 0.5)
                            index = 0;
                        else if (f < 0.7)
                            index = 2;
                        else
                            index = 1;


                        e.Graphics.DrawImage(new Bitmap(WATER_TILE_FRAMES[index], new Size(CELL_SIZE + 1, CELL_SIZE + 1)), new Point(CELL_SIZE * n, CELL_SIZE * i));
                    }

                changeWater = false;
            }

            Pen p = new Pen(Color.Black, 2);
            for (int i = 0; i < 10; i++)
            {
                e.Graphics.DrawLine(p, 0, CELL_SIZE * i, CELL_SIZE * 10, CELL_SIZE * i);
                e.Graphics.DrawLine(p, CELL_SIZE * i, 0, CELL_SIZE * i, CELL_SIZE * 10);
            }

            if (cell != Point.Empty)
            {
                p.Color = Color.Aqua;

                int posX = cell.X * CELL_SIZE,
                    posY = cell.Y * CELL_SIZE;

                e.Graphics.DrawLine(p, posX, posY, posX + CELL_SIZE, posY);
                e.Graphics.DrawLine(p, posX, posY, posX, posY + CELL_SIZE);
                e.Graphics.DrawLine(p, posX, posY + CELL_SIZE, posX + CELL_SIZE, posY + CELL_SIZE);
                e.Graphics.DrawLine(p, posX + CELL_SIZE, posY, posX + CELL_SIZE, posY + CELL_SIZE);
            }
        }

        private void board_MouseEnter(object sender, EventArgs e)
        {
            inside = true;
        }

        private void board_MouseLeave(object sender, EventArgs e)
        {
            inside = false;
        }

        private void board_MouseMove(object sender, MouseEventArgs e)
        {
            if (inside)
                if (e.X / CELL_SIZE != cell.X || e.Y / CELL_SIZE != cell.Y)
                {
                    cell = new Point(e.X / CELL_SIZE, e.Y / CELL_SIZE);

                    Pen p = new Pen(Color.Black, 2);
                    Graphics g = board.CreateGraphics();

                    for (int i = 0; i < 10; i++)
                    {
                        g.DrawLine(p, 0, CELL_SIZE * i, CELL_SIZE * 10, CELL_SIZE * i);
                        g.DrawLine(p, CELL_SIZE * i, 0, CELL_SIZE * i, CELL_SIZE * 10);
                    }

                    p.Color = Color.Aqua;

                    int posX = cell.X * CELL_SIZE,
                        posY = cell.Y * CELL_SIZE;

                    g.DrawLine(p, posX, posY, posX + CELL_SIZE, posY);
                    g.DrawLine(p, posX, posY, posX, posY + CELL_SIZE);
                    g.DrawLine(p, posX, posY + CELL_SIZE, posX + CELL_SIZE, posY + CELL_SIZE);
                    g.DrawLine(p, posX + CELL_SIZE, posY, posX + CELL_SIZE, posY + CELL_SIZE);
                }
        }

        private void FrameTick(object sender, EventArgs e)
        {
            changeWater = true;
            board.Invalidate();
        }
    }
}
