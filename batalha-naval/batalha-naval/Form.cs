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
using System.Windows.Input;
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
        public static readonly Point EMPTY_POINT                = new Point(-1, -1);

        private bool inside = false, shooting = false;

        private Point cell  = EMPTY_POINT, splashCell = EMPTY_POINT;
        private SoundPlayer player = new SoundPlayer(RESOURCES_FOLDER + WATER_SPLASHES_SOUND[0]);

        private Image[,] water;

        private Timer splash;
        #endregion

        private Dictionary<Navio, int> disponiveis = new Dictionary<Navio, int>();
        private List<BoatData> barcosMapa = new List<BoatData>();

        private PictureBox inicioArraste = null;
        private DragData arraste = default(DragData);
        private System.Threading.Thread checkKey;

        private bool inGame = false;
        private String userName;
        private ClienteP2P usuario;
        private Tabuleiro tabUser;

        private Navio draggedBoat = Navio.Cruzador;

        public GameForm()
        {
            InitializeComponent();

            //UserForm user = new UserForm();
            //if (user.ShowDialog(this) == DialogResult.OK)
            //   userName = user.User;
            //else
            //    this.Dispose();

            tabUser = new Tabuleiro();
            disponiveis.Add(Navio.PortaAvioes, Navio.PortaAvioes.Limite());
            disponiveis.Add(Navio.Encouracado, Navio.Encouracado.Limite());
            disponiveis.Add(Navio.Cruzador, Navio.Cruzador.Limite());
            disponiveis.Add(Navio.Destroier, Navio.Destroier.Limite());
            disponiveis.Add(Navio.Submarino, Navio.Submarino.Limite());

            checkKey = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            checkKey.SetApartmentState(System.Threading.ApartmentState.STA);

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

            //Se o usuário está arrastando alguma coisa, desenha o barco e se a posição é válida
            if (!arraste.Equals(default(DragData)))
            {
                e.Graphics.DrawImage(new Bitmap(arraste.Image,
                                               (arraste.SentidoBarco == Sentido.Horizontal ? new Size(CELL_SIZE * arraste.Size, CELL_SIZE) : new Size(CELL_SIZE, CELL_SIZE * arraste.Size))),
                                     cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1);

                Brush b = new Pen(Color.FromArgb(100, Color.Green)).Brush;
                Rectangle validateRect = new Rectangle(new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1),
                                         (arraste.SentidoBarco == Sentido.Horizontal ?
                                         new Size(CELL_SIZE * arraste.Size, CELL_SIZE) :
                                         new Size(CELL_SIZE, CELL_SIZE * arraste.Size)));

                if ((cell.X + arraste.Size > 10 && arraste.SentidoBarco == Sentido.Horizontal) || (cell.Y + arraste.Size > 10 && arraste.SentidoBarco == Sentido.Vertical))
                    b = new Pen(Color.FromArgb(100, Color.Red)).Brush;

                e.Graphics.FillRectangle(b, validateRect);
            }
            //Caso contrário, contorna a célula que o usuário está passando com o mouse
            else
            {
                if (cell != EMPTY_POINT)
                {
                    p.Color = Color.Aqua;
                    e.Graphics.DrawRectangle(p, new Rectangle(new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1), new Size(CELL_SIZE, CELL_SIZE)));
                }
            }

            foreach (BoatData bd in barcosMapa)
                e.Graphics.DrawImage(bd.Image, bd.Point);
        }

        private void board_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Caso esteja em um jogo e no momento o tiro não esta sendo disparado, começa a tocar um novo som de tiro
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

        private void MouseMove(Point p)
        {
            //Caso a posição do mouse tenha mudado, ele troca a celula atual e redesenha
            if (inside || !arraste.Equals(default(DragData)))
                if (p.X / CELL_SIZE != cell.X || p.Y / CELL_SIZE != cell.Y)
                {
                    cell = new Point(p.X / CELL_SIZE, p.Y / CELL_SIZE);
                    board.Invalidate();
                }
        }

        #region Drag Boat Methods
        private void run()
        {
            bool pressed = false;

            while (true)
            {
                //Troca o sentido do barco sendo arrastado cada vez que o usuário pressiona enter
                if (!pressed && Keyboard.IsKeyDown(Key.Enter))
                {
                    if (Keyboard.IsKeyDown(Key.Enter) && !arraste.Equals(default(DragData)))
                        if (arraste.SentidoBarco == Sentido.Horizontal)
                            arraste.SentidoBarco = Sentido.Vertical;
                        else
                            arraste.SentidoBarco = Sentido.Horizontal;

                    pressed = true;
                }
                else if (Keyboard.IsKeyUp(Key.Enter))
                    pressed = false;

            }
        }

        private void board_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseMove(new Point(e.X, e.Y));
        }

        private void board_MouseEnter(object sender, EventArgs e)
        {
            inside = true;
        }

        private void board_MouseLeave(object sender, EventArgs e)
        {
            inside = false;
        }

        private void ArrasteBarco(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (((PictureBox)sender).Image != null)
            {
                switch(((PictureBox)sender).Name.Substring(2))
                {
                    case "PortaAvioes":
                        draggedBoat = Navio.PortaAvioes;
                        break;

                    case "Encouracado":
                        draggedBoat = Navio.Encouracado;
                        break;

                    case "Cruzador":
                        draggedBoat = Navio.Cruzador;
                        break;

                    case "Destroier":
                        draggedBoat = Navio.Destroier;
                        break;

                    case "Submarino":
                        draggedBoat = Navio.Submarino;
                        break;
                }

                //Se há uma imagem, ainda existem barcos para serem arrastados, então ele inicia um novo arraste
                arraste = new DragData(draggedBoat, Sentido.Horizontal, sender);
                board.DoDragDrop(arraste.Image, DragDropEffects.Copy);
            }
        }

        private void board_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                //Para a verificação do usuário tentando girar o navio
                if (checkKey.ThreadState == System.Threading.ThreadState.Unstarted)
                    checkKey.Start();
                else
                    checkKey.Resume();

                //Inicia o novo arraste, guardando o barco que esta sendo arrastado e removendo da garagem
                arraste = new DragData(draggedBoat, Sentido.Horizontal, arraste.Sender);
                disponiveis[arraste.Navio]--;

                if (disponiveis[arraste.Navio] == 0)
                    ((PictureBox)arraste.Sender).Image = null;

                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void board_DragDrop(object sender, DragEventArgs e)
        {
            checkKey.Suspend();

            try
            {
                //Posiciona o navio no tabuleiro para o multiplayer
                tabUser.PosicionarNavio(arraste.Navio, cell.X, cell.Y, (int)arraste.SentidoBarco);

                //Guarda os barcos ja adicionados para serem redesenhados
                barcosMapa.Add(new BoatData(new Bitmap(arraste.Image,
                                                      (arraste.SentidoBarco == Sentido.Horizontal ? 
                                                      new Size(CELL_SIZE * arraste.Size, CELL_SIZE) : 
                                                      new Size(CELL_SIZE, CELL_SIZE * arraste.Size))), new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1)));

                arraste = default(DragData);
            }
            catch
            {
                //Caso a adição do barco não foi sucedida, o barco volta para a garagem e aumenta um em disponíveis
                disponiveis[arraste.Navio]++;
                ((PictureBox)arraste.Sender).Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
                arraste = default(DragData);
            }

            board.Invalidate();
        }

        private void board_DragLeave(object sender, EventArgs e)
        {
            checkKey.Suspend();

            //Retorna todas as informações, como se o drag não tivesse iniciado, para evitar que, caso o usuário 
            //tenha solto o drag fora do tabuleiro, ele continue guardando as informações e acabe perdendo um barco
            ((PictureBox)arraste.Sender).Image = arraste.Image;
            disponiveis[arraste.Navio]++;
            arraste = default(DragData);

            board.Invalidate();
        }

        private void board_DragOver(object sender, DragEventArgs e)
        {
            MouseMove(board.PointToClient(new Point(e.X, e.Y)));
            board.Invalidate();
        }
        #endregion

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
        #endregion
    }
}
