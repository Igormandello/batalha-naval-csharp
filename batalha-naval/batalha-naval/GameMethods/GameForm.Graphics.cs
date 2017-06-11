using BatalhaNaval;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace batalha_naval
{
    public partial class GameForm
    {
        private List<Point> pontosBarcos  = new List<Point>();
        private List<BoatData> barcosMapa = new List<BoatData>();

        Random r = new Random();
        private void boardPlayer_Paint(object sender, PaintEventArgs e)
        {
            DrawBackground(e);

            //Se o usuário está arrastando alguma coisa, desenha o barco e se a posição é válida
            if (!inGame)
                //Verificações de arraste: se a posição é valida, se não sobrepõe nenhum navio e o desenho do caso atual
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

                    for (int i = 0; i < arraste.Size; i++)
                        for (int n = 0; n < pontosBarcos.Count; n++)
                            if (pontosBarcos[n].X == (cell.X + (arraste.SentidoBarco == Sentido.Horizontal ? i : 0)) && pontosBarcos[n].Y == (cell.Y + (arraste.SentidoBarco == Sentido.Vertical ? i : 0)))
                                b = new Pen(Color.FromArgb(100, Color.Red)).Brush;

                    e.Graphics.FillRectangle(b, validateRect);
                }
                //Caso contrário, contorna a célula que o usuário está passando com o mouse
                else if (cell != EMPTY_POINT)
                {
                    Pen p = new Pen(Color.Aqua, 2);
                    e.Graphics.DrawRectangle(p, new Rectangle(new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1), new Size(CELL_SIZE, CELL_SIZE)));
                }

            foreach (BoatData bd in barcosMapa)
                e.Graphics.DrawImage(bd.Image, bd.Point);

            //Marca no mapa os tiros recebidos
            if (usuario != null)
                foreach (Tiro t in usuario.TirosRecebidos)
                    e.Graphics.FillRectangle(new Pen(Color.FromArgb(100, Color.IndianRed)).Brush, new Rectangle(t.X * CELL_SIZE + 1, t.Y * CELL_SIZE + 1, CELL_SIZE, CELL_SIZE));

            //Frames de animação para a água e explosão
            if (player == Animacao.Espirrando)
            {
                if (indexS >= 0)
                    using (Bitmap b = new Bitmap(WATER_SPLASH_FRAMES[indexS], new Size(CELL_SIZE, CELL_SIZE)))
                        e.Graphics.DrawImage(b, receivedCell.X * 40, receivedCell.Y * 40);
            }
            else if (player == Animacao.Explodindo)
                if (indexE >= 0)
                   using (Bitmap b = new Bitmap(EXPLOSION_FRAMES[indexE], new Size(CELL_SIZE, CELL_SIZE)))
                       e.Graphics.DrawImage(b, receivedCell.X * 40, receivedCell.Y * 40);
        }

        private void boardEnemy_Paint(object sender, PaintEventArgs e)
        {
            DrawBackground(e);

            if (cell != EMPTY_POINT && inGame)
            {
                Pen p = new Pen(Color.Aqua, 2);
                e.Graphics.DrawRectangle(p, new Rectangle(new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1), new Size(CELL_SIZE, CELL_SIZE)));
            }

            //Marca no mapa os tiros ja efetuados
            if (usuario != null)
                foreach (Tiro t in usuario.TirosDados)
                    e.Graphics.FillRectangle(new Pen(Color.FromArgb(100, Color.IndianRed)).Brush, new Rectangle(t.X * CELL_SIZE + 1, t.Y * CELL_SIZE + 1, CELL_SIZE, CELL_SIZE));

            //Frames de animação para água e explosão
            if (enemy == Animacao.Espirrando)
            {
                if (indexS >= 0)
                    using (Bitmap b = new Bitmap(WATER_SPLASH_FRAMES[indexS], new Size(CELL_SIZE, CELL_SIZE)))
                        e.Graphics.DrawImage(b, targetCell.X * 40, targetCell.Y * 40);
            }
            else if (enemy == Animacao.Explodindo)
                if (indexE >= 0)
                    using (Bitmap b = new Bitmap(EXPLOSION_FRAMES[indexE], new Size(CELL_SIZE, CELL_SIZE)))
                        e.Graphics.DrawImage(b, targetCell.X * 40, targetCell.Y * 40);
        }

        private void DrawBackground(PaintEventArgs e)
        {
            //Desenha o fundo do tabuleiro
            for (int n = 0; n < 10; n++)
                for (int i = 0; i < 10; i++)
                    e.Graphics.DrawImage(water[n, i], new Point(CELL_SIZE * i, CELL_SIZE * n));

            //Desenha a grid
            Pen p = new Pen(Color.Black, 2);
            for (int i = 0; i < 10; i++)
            {
                e.Graphics.DrawLine(p, 0, CELL_SIZE * i + 1, CELL_SIZE * 10, CELL_SIZE * i + 1);
                e.Graphics.DrawLine(p, CELL_SIZE * i + 1, 0, CELL_SIZE * i + 1, CELL_SIZE * 10);
            }
        }

        private new void MouseMove(Point p)
        {
            //Caso a posição do mouse tenha mudado, ele troca a celula atual e redesenha
            if (inside || !arraste.Equals(default(DragData)))
                if (p.X / CELL_SIZE != cell.X || p.Y / CELL_SIZE != cell.Y)
                {
                    cell = new Point(p.X / CELL_SIZE, p.Y / CELL_SIZE);

                    if (inGame)
                        boardEnemy.Invalidate();
                    else
                        boardPlayer.Invalidate();
                }
        }
    }
}
