using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace batalha_naval
{
    public partial class GameForm
    {
        private List<BoatData> barcosMapa = new List<BoatData>();
        private List<BatalhaNaval.Tiro> tirosRecebidos;

        Random r = new Random();
        private void boardPlayer_Paint(object sender, PaintEventArgs e)
        {
            DrawBackground(e);

            //Se o usuário está arrastando alguma coisa, desenha o barco e se a posição é válida
            if (!inGame)
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
                else if (cell != EMPTY_POINT)
                {
                    Pen p = new Pen(Color.Aqua, 2);
                    e.Graphics.DrawRectangle(p, new Rectangle(new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1), new Size(CELL_SIZE, CELL_SIZE)));
                }

            foreach (BoatData bd in barcosMapa)
                e.Graphics.DrawImage(bd.Image, bd.Point);

            if (tirosRecebidos != null)
                foreach (BatalhaNaval.Tiro t in tirosRecebidos)
                    e.Graphics.FillRectangle(Brushes.IndianRed, new Rectangle(t.X * CELL_SIZE + 1, t.Y * CELL_SIZE + 1, CELL_SIZE, CELL_SIZE));
        }

        private void boardEnemy_Paint(object sender, PaintEventArgs e)
        {
            DrawBackground(e);

            if (cell != EMPTY_POINT && inGame)
            {
                Pen p = new Pen(Color.Aqua, 2);
                e.Graphics.DrawRectangle(p, new Rectangle(new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1), new Size(CELL_SIZE, CELL_SIZE)));
            }
        }

        private void DrawBackground(PaintEventArgs e)
        {
            for (int n = 0; n < 10; n++)
                for (int i = 0; i < 10; i++)
                    e.Graphics.DrawImage(water[n, i], new Point(CELL_SIZE * i, CELL_SIZE * n));


            if (index >= 0)
                using (Bitmap b = new Bitmap(WATER_SPLASH_FRAMES[index], new Size(CELL_SIZE, CELL_SIZE)))
                    e.Graphics.DrawImage(b, splashCell.X * 40, splashCell.Y * 40);

            Pen p = new Pen(Color.Black, 2);
            for (int i = 0; i < 10; i++)
            {
                e.Graphics.DrawLine(p, 0, CELL_SIZE * i + 1, CELL_SIZE * 10, CELL_SIZE * i + 1);
                e.Graphics.DrawLine(p, CELL_SIZE * i + 1, 0, CELL_SIZE * i + 1, CELL_SIZE * 10);
            }
        }

        private void MouseMove(Point p)
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
