using System;
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
using System.Threading;
using System.Net;

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
        public static readonly Point EMPTY_POINT                = new Point(-1, -1);

        private bool inside = false, shooting = false;

        private Point cell  = EMPTY_POINT, splashCell = EMPTY_POINT;
        private SoundPlayer player = new SoundPlayer(RESOURCES_FOLDER + WATER_SPLASHES_SOUND[0]);

        private Image[,] water;

        private System.Windows.Forms.Timer splash;
        #endregion

        public GameForm()
        {
            InitializeComponent();

            //UserForm user = new UserForm();
            //if (user.ShowDialog(this) == DialogResult.OK)
            //   userName = user.User;
            //else
            //    this.Dispose();

            keyChecker = new Semaphore(0, 2);
            keyChecker.Release();

            tabUser = new Tabuleiro();
            disponiveis.Add(TipoDeNavio.PortaAvioes, TipoDeNavio.PortaAvioes.Limite());
            disponiveis.Add(TipoDeNavio.Encouracado, TipoDeNavio.Encouracado.Limite());
            disponiveis.Add(TipoDeNavio.Cruzador, TipoDeNavio.Cruzador.Limite());
            disponiveis.Add(TipoDeNavio.Destroier, TipoDeNavio.Destroier.Limite());
            disponiveis.Add(TipoDeNavio.Submarino, TipoDeNavio.Submarino.Limite());

            checkKey = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            checkKey.SetApartmentState(System.Threading.ApartmentState.STA);
            checkKey.Start();

            water = new Image[10, 10];
            FrameTick(null, new EventArgs());

            splash          = new System.Windows.Forms.Timer();
            splash.Interval = 1000 / SPLASH_FPS;
            splash.Tick    += SplashTick;

            var t      = new System.Windows.Forms.Timer();
            t.Interval = 1000 / WATER_CHANGE_FPS;
            t.Tick    += FrameTick;
            t.Start();

            tabUser.PosicionarNavio(TipoDeNavio.PortaAvioes, 0, 0, Direcao.Direita);
            tabUser.PosicionarNavio(TipoDeNavio.Encouracado, 0, 1, Direcao.Direita);
            tabUser.PosicionarNavio(TipoDeNavio.Cruzador, 0, 2, Direcao.Direita);
            tabUser.PosicionarNavio(TipoDeNavio.Destroier, 0, 3, Direcao.Direita);
            tabUser.PosicionarNavio(TipoDeNavio.Submarino, 0, 4, Direcao.Direita);

            IniciarCliente();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            usuario?.Close();

            stopRunning = true;

            try
            {
                keyChecker.Release();
            }
            catch { }

            keyChecker.Dispose();
        }

        #region Frames Methods
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

                    if (water[n, i] != null)
                        water[n, i].Dispose();
                    water[n, i] = new Bitmap(WATER_TILE_FRAMES[index], new Size(CELL_SIZE + 1, CELL_SIZE + 1));
                }

            boardPlayer.Invalidate();
            boardEnemy.Invalidate();
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

            boardEnemy.Invalidate();
        }
        #endregion
    }
}
