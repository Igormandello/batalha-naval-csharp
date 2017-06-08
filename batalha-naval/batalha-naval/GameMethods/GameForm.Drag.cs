using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using BatalhaNaval;

namespace batalha_naval
{
    public partial class GameForm
    {
        private void run()
        {
            bool pressed = false;

            while (true)
            {
                keyChecker.WaitOne();

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

                if (!stopRunning)
                    keyChecker.Release();
                else
                    checkKey.Abort();
            }
        }

        private void board_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!inGame)
                MouseMove(new Point(e.X, e.Y));
        }

        private void board_MouseEnter(object sender, EventArgs e)
        {
            if (!inGame)
                inside = true;
        }

        private void board_MouseLeave(object sender, EventArgs e)
        {
            if (!inGame)
                inside = false;
        }

        private void ArrasteBarco(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (((PictureBox)sender).Image != null)
            {
                switch (((PictureBox)sender).Name.Substring(2))
                {
                    case "PortaAvioes":
                        draggedBoat = TipoDeNavio.PortaAvioes;
                        break;

                    case "Encouracado":
                        draggedBoat = TipoDeNavio.Encouracado;
                        break;

                    case "Cruzador":
                        draggedBoat = TipoDeNavio.Cruzador;
                        break;

                    case "Destroier":
                        draggedBoat = TipoDeNavio.Destroier;
                        break;

                    case "Submarino":
                        draggedBoat = TipoDeNavio.Submarino;
                        break;
                }

                //Se há uma imagem, ainda existem barcos para serem arrastados, então ele inicia um novo arraste
                arraste = new DragData(draggedBoat, Sentido.Horizontal, sender);
                boardPlayer.DoDragDrop(arraste.Image, DragDropEffects.Copy);
            }
        }

        private void board_DragEnter(object sender, DragEventArgs e)
        {
            if (!inGame)
            {
                inside = true;

                if (e.Data.GetDataPresent(DataFormats.Bitmap))
                {
                    //Para a verificação do usuário tentando girar o navio
                    //if (checkkey.threadstate == system.threading.threadstate.unstarted)
                    //    checkkey.start();
                    //else
                    //    checkkey.resume();
                    try
                    {
                        keyChecker.Release();
                    }
                    catch { }

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
        }

        private void board_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                keyChecker.WaitOne();

                //Posiciona o navio no tabuleiro para o multiplayer
                tabUser.PosicionarNavio(arraste.Navio, cell.X, cell.Y, (Direcao)arraste.SentidoBarco);

                //Guarda os barcos ja adicionados para serem redesenhados
                barcosMapa.Add(new BoatData(new Bitmap(arraste.Image,
                                                      (arraste.SentidoBarco == Sentido.Horizontal ?
                                                      new Size(CELL_SIZE * arraste.Size, CELL_SIZE) :
                                                      new Size(CELL_SIZE, CELL_SIZE * arraste.Size))), new Point(cell.X * CELL_SIZE + 1, cell.Y * CELL_SIZE + 1)));
                arraste = default(DragData);

                if (tabUser.EstaCompleto())
                    IniciarCliente();
            }
            catch
            {
                //Caso a adição do barco não foi sucedida, o barco volta para a garagem e aumenta um em disponíveis
                disponiveis[arraste.Navio]++;
                ((PictureBox)arraste.Sender).Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
                arraste = default(DragData);
            }

            boardPlayer.Invalidate();
        }

        private void board_DragLeave(object sender, EventArgs e)
        {
            if (!inGame)
            {
                inside = false;
                keyChecker.WaitOne();

                //Retorna todas as informações, como se o drag não tivesse iniciado, para evitar que, caso o usuário 
                //tenha solto o drag fora do tabuleiro, ele continue guardando as informações e acabe perdendo um barco
                ((PictureBox)arraste.Sender).Image = arraste.Image;
                if (arraste.SentidoBarco == Sentido.Vertical)
                    ((PictureBox)arraste.Sender).Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                disponiveis[arraste.Navio]++;

                object temp = arraste.Sender;

                arraste = default(DragData);
                arraste.Sender = temp;

                boardPlayer.Invalidate();
            }
        }

        private void board_DragOver(object sender, DragEventArgs e)
        {
            if (!inGame)
            {
                MouseMove(boardPlayer.PointToClient(new Point(e.X, e.Y)));
                boardPlayer.Invalidate();
            }
        }
    }
}
