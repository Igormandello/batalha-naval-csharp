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

            //Reinicia o cliente
            IniciarCliente();

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
                    //Verifica se o lugar que o usuário está tentando atirar não foi usado ainda
                    foreach (Tiro t in usuario.TirosDados)
                        if (t.X == cell.X && t.Y == cell.Y)
                        {
                            MessageBox.Show("Você já atirou neste lugar!");
                            return;
                        }

                    shooting = true;
                    targetCell = cell;

                    //Envia o tiro para o cliente
                    usuario.DarTiro(targetCell.X, targetCell.Y);
                }
                else
                    MessageBox.Show(this, "Não é seu turno!", "Ta apressado demais", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void TiroRecebido(Tiro t)
        {
            ResultadoDeTiro resultado = t.Aplicar(tabUser);
            Invoke(new Action(() =>
            {
                //Atualiza a tela
                boardPlayer.Invalidate();
                boardEnemy.Invalidate();

                //Guarda onde recebeu o tiro
                receivedCell = new System.Drawing.Point(t.X, t.Y);

                ResultadoDeTiro resultadoTotal = usuario.TirosRecebidos.Resultado(t);

                //Se, no resultado obtido no ultimo tiro, contiver a flag "ganhou", significa que o adversário ganhou
                if (resultadoTotal.HasFlag(ResultadoDeTiro.Ganhou))
                {
                    usuario.Close();
                    SairJogo();

                    IniciarCliente();
                    MessageBox.Show(this, "Você perdeu o joguinho =(", "Que pena...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (resultado == ResultadoDeTiro.Errou)
                {
                    //Inicia a animação de água no seu tabuleiro
                    player = Animacao.Espirrando;
                    splash.Start();
                }
                else
                {
                    //Inicia a animação de explosão no seu tabuleiro
                    player = Animacao.Explodindo;
                    explosion.Start();
                }
            }));
        }

        private void Usuario_OnResultadoDeTiro(Tiro t, ResultadoDeTiro resultado)
        {
            Invoke(new Action(() =>
            {
                //Atualiza a tela, para a animação
                boardEnemy.Invalidate();
                boardPlayer.Invalidate();

                //Se nas flags do resultado tiver a flag "ganhou", significa que você ganhou
                if (resultado.HasFlag(ResultadoDeTiro.Ganhou))
                {
                    usuario.Close();
                    SairJogo();

                    IniciarCliente();
                    MessageBox.Show(this, "Você ganhou o joguinho =)", "Parabéns!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (resultado == ResultadoDeTiro.Errou)
                {
                    //Inicia a animação e o som de água no tabuleiro inimigo
                    enemy = Animacao.Espirrando;
                    splash.Start();

                    soundPlayerS.Play();
                }
                else
                {
                    //Inicia a animação e o som de explosão no tabuleiro inimigo
                    enemy = Animacao.Explodindo;
                    explosion.Start();

                    soundPlayerE.Play();
                }

                if (resultado.HasFlag(ResultadoDeTiro.Afundou))
                    MessageBox.Show(this, "O " + Enum.GetName(typeof(TipoDeNavio), resultado.TipoDeNavio()) + " inimigo foi afundado");
            }));
        }

        private void DarTiro()
        {
            Invoke(new Action(() =>
            {
                shooting = false;
            }));
        }
    }
}
