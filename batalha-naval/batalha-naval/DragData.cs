using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatalhaNaval;

namespace batalha_naval
{
    public struct DragData
    {
        public System.Drawing.Image Image { get; private set; }
        public Navio Navio { get; private set; }
        public int Size { get; private set; }

        private Sentido _sentido;
        public Sentido SentidoBarco
        { 
            get
            {
                return _sentido;
            }

            set
            {
                if (this._sentido != value && Image != null)
                    if (value == batalha_naval.Sentido.Vertical)
                        Image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                    else
                        Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);

                this._sentido = value;
            }
        }
        public object Sender { get; private set; }

        public DragData(Navio n, Sentido s, object sender)
        {
            Navio = n;
            Size  = n.Tamanho();
            this._sentido = Sentido.Horizontal;
            this.Sender   = sender;

            switch (n)
            {
                case Navio.PortaAvioes:
                    Image = batalha_naval.Properties.Resources.PortaAvioes;
                    break;

                case Navio.Encouracado:
                    Image = batalha_naval.Properties.Resources.Encouracado;
                    break;

                case Navio.Cruzador:
                    Image = batalha_naval.Properties.Resources.Cruzador;
                    break;

                case Navio.Destroier:
                    Image = batalha_naval.Properties.Resources.Destroier;
                    break;

                case Navio.Submarino:
                    Image = batalha_naval.Properties.Resources.Submarino;
                    break;

                default:
                    Image = null;
                    Size = 0;
                    this._sentido = Sentido.Horizontal;
                    break;
            }

            SentidoBarco = s;
        }
    }

    public enum Sentido { Vertical = 0, Horizontal = 3 }
}
