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
        public TipoDeNavio Navio { get; private set; }
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
        public object Sender { get; set; }

        public DragData(TipoDeNavio n, Sentido s, object sender)  : this()
        {
            _sentido = Sentido.Horizontal;
            Navio = n;
            Size = n.Tamanho();
            SentidoBarco = s;
            Sender = sender;

            switch (n)
            {
                case TipoDeNavio.PortaAvioes:
                    Image = batalha_naval.Properties.Resources.PortaAvioes;
                    break;

                case TipoDeNavio.Encouracado:
                    Image = batalha_naval.Properties.Resources.Encouracado;
                    break;

                case TipoDeNavio.Cruzador:
                    Image = batalha_naval.Properties.Resources.Cruzador;
                    break;

                case TipoDeNavio.Destroier:
                    Image = batalha_naval.Properties.Resources.Destroier;
                    break;

                case TipoDeNavio.Submarino:
                    Image = batalha_naval.Properties.Resources.Submarino;
                    break;

                default:
                    Image = null;
                    Size = 0;
                    this._sentido = Sentido.Horizontal;
                    break;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(DragData))
                return false;

            DragData dg = (DragData)obj;
            if (Image == dg.Image && Navio == dg.Navio && _sentido == dg._sentido)
                return true;

            return false;
        }
    }

    public enum Sentido { Vertical = Direcao.Baixo, Horizontal = Direcao.Direita }
}
