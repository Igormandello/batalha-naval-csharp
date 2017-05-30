﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatalhaNaval;

namespace batalha_naval
{
    public partial class GameForm : Form
    {
        #region Variaveis e constantes dos graficos e sons
        public const int                   DEFAULT_TILESIZE = 32,
                                           CELL_SIZE        = 40,
                                           WATER_CHANGE_FPS = 1,
                                           SPLASH_FPS       = 6;

        public const string                RESOURCES_FOLDER     = "../../../../Resources/";
        public static readonly string[]    WATER_SPLASHES_SOUND = new string[] { "watersplash.wav", "watersplash2.wav" };
        public static readonly List<Image> WATER_SPLASH_FRAMES  = TileSet.SplitImage(RESOURCES_FOLDER + "splash.png", DEFAULT_TILESIZE, DEFAULT_TILESIZE);
        public static readonly List<Image> WATER_TILE_FRAMES    = TileSet.SplitImage(RESOURCES_FOLDER + "water_tiles.png", DEFAULT_TILESIZE, DEFAULT_TILESIZE);

        private bool inside = false, shooting = false;

        private Point cell  = Point.Empty, splashCell = Point.Empty;
        private SoundPlayer player = new SoundPlayer(RESOURCES_FOLDER + WATER_SPLASHES_SOUND[0]);

        private Image[,] water;

        private Timer splash;
        #endregion

        private bool inGame = false;
        private String userName;
        private ClienteP2P usuario;
        private Tabuleiro tabUser;

        public GameForm()
        {
            InitializeComponent();

            UserForm user = new UserForm();
            if (user.ShowDialog(this) == DialogResult.OK)
                userName = user.User;
            else
                this.Dispose();

            water = new Image[10, 10];
            FrameTick(null, new EventArgs());

            splash          = new Timer();
            splash.Interval = 1000 / SPLASH_FPS;
            splash.Tick    += SplashTick;

            Timer t    = new Timer();
            t.Interval = 1000 / WATER_CHANGE_FPS;
            t.Tick    += FrameTick;
            t.Start();
        }

        Random r = new Random();
        private void board_Paint(object sender, PaintEventArgs e)
        {
            for (int n = 0; n < 10; n++)
                for (int i = 0; i < 10; i++)
                    e.Graphics.DrawImage(water[n,i], new Point(CELL_SIZE * i, CELL_SIZE * n));

            
            if (index >= 0)
                using (Bitmap b = new Bitmap(WATER_SPLASH_FRAMES[index], new Size(CELL_SIZE, CELL_SIZE)))
                    e.Graphics.DrawImage(b, splashCell.X * 40, splashCell.Y * 40);

            Pen p = new Pen(Color.Black, 2);
            for (int i = 0; i < 10; i++)
            {
                e.Graphics.DrawLine(p, 0, CELL_SIZE * i + 1, CELL_SIZE * 10, CELL_SIZE * i + 1);
                e.Graphics.DrawLine(p, CELL_SIZE * i + 1, 0, CELL_SIZE * i + 1, CELL_SIZE * 10);
            }

            if (cell != Point.Empty)
            {
                p.Color = Color.Aqua;
                e.Graphics.DrawRectangle(p, new Rectangle(new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1), new Size(CELL_SIZE, CELL_SIZE)));
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

        private void board_MouseDown(object sender, MouseEventArgs e)
        {
            if (inGame)
                if (!shooting)
                {
                    player.Play();

                    splash.Start();
                    splashCell = cell;
                    shooting = true;

                    board.Invalidate();
                }
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
                        g.DrawLine(p, 0, CELL_SIZE * i + 1, CELL_SIZE * 10, CELL_SIZE * i + 1);
                        g.DrawLine(p, CELL_SIZE * i + 1, 0, CELL_SIZE * i + 1, CELL_SIZE * 10);
                    }

                    p.Color = Color.Aqua;

                    int posX = cell.X * CELL_SIZE,
                        posY = cell.Y * CELL_SIZE;

                    g.DrawRectangle(p, new Rectangle(new Point(posX + 1, posY + 1), new Size(CELL_SIZE, CELL_SIZE)));
                }
        }

        private void FrameTick(object sender, EventArgs e)
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

                    water[n, i]?.Dispose();
                    water[n, i] = new Bitmap(WATER_TILE_FRAMES[index], new Size(CELL_SIZE + 1, CELL_SIZE + 1));
                }

            board.Invalidate();
        }

        private int index = -1;
        private void SplashTick(object sender, EventArgs e)
        {
            if (index == WATER_SPLASH_FRAMES.Count - 1)
            {
                index = -1;
                splashCell = Point.Empty;

                shooting = false;
                splash.Stop();
            }
            else
                index++;

            board.Invalidate();
        }
    }
}
