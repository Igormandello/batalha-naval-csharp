using System;
using System.Net;
using BatalhaNaval;
using System.Windows.Forms;

namespace batalha_naval
{
    public partial class GameForm
    {
        private bool inGame = false, stopRunning = false;
        private String userName;
        private ClienteP2P usuario;
        private Tabuleiro tabUser;

        private void btnConectar_Click(object sender, EventArgs e)
        {
            usuario.SolicitarConexao(IPAddress.Parse(cbIPs.SelectedItem + ""));
        }

        private void IniciarCliente()
        {
            usuario = new ClienteP2P("Igor", tabUser);

            usuario.OnClienteDisponivel += ClienteDisponivel;
            usuario.OnClienteIndisponivel += ClienteIndisponivel;

            usuario.OnClienteRequisitandoConexao += RequisitandoConexao;
            usuario.OnClienteConectado += ClienteConectado;
            usuario.OnClienteDesconectado += ClienteDesconectado;

            usuario.OnTiroRecebido += TiroRecebido;
            usuario.OnDarTiro += DarTiro;

            usuario.OnResultadoDeTiro += ResultadoTiro;
            
            usuario.Iniciar();

            gbGaragem.Visible = false;
            pnlConexao.Visible = true;
        }

        private void ClienteIndisponivel(IPAddress addr)
        {
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteIndisponivel(addr); }));
            else //if (cbIPs.Items.IndexOf(addr) >= 0)
                cbIPs.Items.Remove(addr);
        }

        private void ClienteDesconectado(IPAddress addr)
        {
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteDesconectado(addr); }));
            else
            {
                MessageBox.Show("mamou");
                SairJogo();
            }
        }

        private void ClienteConectado(IPAddress addr)
        {
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteConectado(addr); }));
            else
            {
                InicializarJogo();
                inGame = true;
            }
        }

        private bool RequisitandoConexao(System.Net.IPAddress addr)
        {
            bool r = false;
            if (InvokeRequired)
            {
                Invoke(new Action(() => { r = RequisitandoConexao(addr); }));
                return r;
            }
            else
            {
                bool b = MessageBox.Show(this, "Deseja se conectar ao " + addr.ToString() + "?", "Conexao", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
                return b;
            }
        }

        private void ClienteDisponivel(System.Net.IPAddress addr)
        {
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteDisponivel(addr); }));
            else if (!cbIPs.Items.Contains(addr))
                cbIPs.Items.Add(addr);
        }
    }
}
