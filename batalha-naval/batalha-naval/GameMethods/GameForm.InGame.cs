using System;
using BatalhaNaval;
using System.Windows.Forms;

namespace batalha_naval
{
    public partial class GameForm
    { 
        private void InicializarJogo()
        {
            //Deixa apenas os componentes para o jogo na tela
            panel1.Visible = false;
            boardEnemy.Visible = true;
            this.AutoSize = true;

            inGame = true;
            inside = true;
            cell   = EMPTY_POINT;

            boardPlayer.Invalidate();
            boardEnemy.Invalidate();

            tirosRecebidos = new System.Collections.Generic.List<Tiro>();
        }

        private void SairJogo()
        {
            //Volta os componentes para o estado normal
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
            //Para saber se esta ou não dentro da board
            if (inGame)
                inside = true;
        }

        private void boardEnemy_MouseLeave(object sender, EventArgs e)
        {
            //Para saber se esta ou não dentro da board
            if (inGame)
                inside = false;
        }

        private void boardEnemy_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Atualiza a posição atual do mouse
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

                    //Envia o tiro para o cliente
                    usuario.DarTiro(splashCell.X, splashCell.Y);

                    //Atualiza a tela, para a animação
                    boardEnemy.Invalidate();
                    boardPlayer.Invalidate();
                }
                else
                    MessageBox.Show(this, "Ta apressado demais", "Não é seu turno!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void TiroRecebido(Tiro t)
        {
<<<<<<< HEAD
            MessageBox.Show("3");
=======
>>>>>>> e137e44b71230ea11dddd3a88e34acc8e73a9e35
            tirosRecebidos.Add(t);

            boardPlayer.Invalidate();
        }

        private void ResultadoTiro(Tiro t, ResultadoDeTiro resultado)
        {
<<<<<<< HEAD
            MessageBox.Show("2");
=======

>>>>>>> e137e44b71230ea11dddd3a88e34acc8e73a9e35
        }

        private void DarTiro()
        {
<<<<<<< HEAD
            MessageBox.Show("1");
=======
>>>>>>> e137e44b71230ea11dddd3a88e34acc8e73a9e35
            shooting = false;
        }
    }
}
