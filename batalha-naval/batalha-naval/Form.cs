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
                                           EXPLOSION_FPS    = 14,
                                           SPLASH_FPS       = 6;

        public const string                RESOURCES_FOLDER     = "../../../../Resources/";
        public static readonly string      WATER_SPLASH_SOUND   = "watersplash.wav",
                                           EXPLOSION_SOUND      = "explosion.wav";
        public static readonly List<Image> WATER_SPLASH_FRAMES  = TileSet.SplitImage(RESOURCES_FOLDER + "splash.png", DEFAULT_TILESIZE, DEFAULT_TILESIZE),
                                           WATER_TILE_FRAMES    = TileSet.SplitImage(RESOURCES_FOLDER + "water_tiles.png", DEFAULT_TILESIZE, DEFAULT_TILESIZE),
                                           EXPLOSION_FRAMES     = TileSet.SplitImage(batalha_naval.Properties.Resources.explosion, DEFAULT_TILESIZE, DEFAULT_TILESIZE);
        public static readonly Point EMPTY_POINT                = new Point(-1, -1);

        enum Animacao { Espirrando, Explodindo, Nenhum };

        private Animacao enemy = Animacao.Nenhum, player = Animacao.Nenhum;
        private bool inside = false, shooting = false;

        private Point cell  = EMPTY_POINT, targetCell = EMPTY_POINT, receivedCell = EMPTY_POINT;

        private SoundPlayer soundPlayerS = new SoundPlayer(RESOURCES_FOLDER + WATER_SPLASH_SOUND),
                            soundPlayerE = new SoundPlayer(RESOURCES_FOLDER + EXPLOSION_SOUND);

        private Image[,] water;

        private System.Windows.Forms.Timer splash, explosion;
        #endregion

        public GameForm()
        {
            InitializeComponent();

            UserForm user = new UserForm();
            if (user.ShowDialog(this) == DialogResult.OK)
               userName = user.User;
            else
                this.Dispose();

            keyChecker = new Semaphore(0, 2);
            keyChecker.Release();

            tabUser = new Tabuleiro();
            //Numero de barcos disponíveis atualmente
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

            explosion = new System.Windows.Forms.Timer();
            explosion.Interval = 1000 / EXPLOSION_FPS;
            explosion.Tick += ExplosionTick;

            var t      = new System.Windows.Forms.Timer();
            t.Interval = 1000 / WATER_CHANGE_FPS;
            t.Tick    += FrameTick;
            t.Start();
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

        private int indexS = -1, indexE = -1;
        private void SplashTick(object sender, EventArgs e)
        {
            if (indexS == WATER_SPLASH_FRAMES.Count - 1)
            {
                indexS = -1;
                targetCell = EMPTY_POINT;

                splash.Stop();
            }
            else
                indexS++;

            boardEnemy.Invalidate();
            boardPlayer.Invalidate();
        }

        private void ExplosionTick(object sender, EventArgs e)
        {
            if (indexE == EXPLOSION_FRAMES.Count - 1)
            {
                indexE = -1;
                targetCell = EMPTY_POINT;

                explosion.Stop();
            }
            else
                indexE++;

            boardEnemy.Invalidate();
            boardPlayer.Invalidate();
        }
        #endregion
    }
}
