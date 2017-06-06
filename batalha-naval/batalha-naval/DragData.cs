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

        public DragData(Navio n, Sentido s)
        {
            switch (n)
            {
                case Navio.Cruzador:
                    Size = 5;
                    Image = ((System.Drawing.Image)(new System.ComponentModel.ComponentResourceManager(typeof(GameForm)).GetObject("barco.Image")));
                    this._sentido = Sentido.Horizontal;
                    SentidoBarco = s;
                    break;

                default:
                    Image = null;
                    Size = 0;
                    this._sentido = Sentido.Horizontal;
                    break;
            }
        }
    }

    public enum Sentido { Vertical, Horizontal }
}
