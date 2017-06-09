using System;
using BatalhaNaval;

namespace batalha_naval
{
    public partial class GameForm
    { 
        private void InicializarJogo()
        {
            panel1.Visible = false;
            boardEnemy.Visible = true;
            this.AutoSize = true;

            inGame = true;
            inside = true;
            cell   = EMPTY_POINT;

            boardPlayer.Invalidate();
            boardEnemy.Invalidate();
        }

        private void SairJogo()
        {
            panel1.Visible = true;
            boardEnemy.Visible = false;
            this.AutoSize = false;

            inGame = false;
            inside = false;
            cell = EMPTY_POINT;

            boardPlayer.Invalidate();
            boardEnemy.Invalidate();
        }

        private void boardEnemy_MouseEnter(object sender, EventArgs e)
        {
            if (inGame)
                inside = true;
        }

        private void boardEnemy_MouseLeave(object sender, EventArgs e)
        {
            if (inGame)
                inside = false;
        }

        private void boardEnemy_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (inGame)
                MouseMove(e.Location);
        }

        private void boardEnemy_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Caso esteja em um jogo e no momento o tiro não esta sendo disparado, começa a tocar um novo som de tiro
            if (inGame)
                if (!shooting)
                {
                    player.Play();

                    splash.Start();
                    splashCell = cell;
                    shooting = true;

                    usuario.DarTiro(splashCell.X, splashCell.Y);

                    boardEnemy.Invalidate();
                }
        }

        private void TiroRecebido(Tiro t)
        {
            
        }

        private void ResultadoTiro(Tiro t, ResultadoDeTiro resultado)
        {
            
        }

        private void DarTiro()
        {
            
        }
    }
}
