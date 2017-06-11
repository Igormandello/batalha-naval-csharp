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
            try
            {
                usuario.SolicitarConexao(IPAddress.Parse(cbIPs.SelectedItem + ""));
            }
            catch
            {
                MessageBox.Show("Erro ao tentar enviar solicitação de conexão");
            }
        }

        private void IniciarCliente()
        {
            usuario = new ClienteP2P("Igor", tabUser);

            usuario.OnClienteDisponivel += ClienteDisponivel;
            usuario.OnClienteIndisponivel += ClienteIndisponivel;

            usuario.OnClienteRequisitandoConexao += RequisitandoConexao;
            usuario.OnClienteConectado += ClienteConectado;
            usuario.OnClienteDesconectado += ClienteDesconectado;

            usuario.OnDarTiro += DarTiro;
            usuario.OnResultadoDeTiro += Usuario_OnResultadoDeTiro; ;
            usuario.OnTiroRecebido += TiroRecebido;

            usuario.Iniciar();

            gbGaragem.Visible = false;
            pnlConexao.Visible = true;
        }

        private void ClienteIndisponivel(IPAddress addr)
        {
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteIndisponivel(addr); }));
            else
                cbIPs.Items.Remove(addr);
        }

        private void ClienteDesconectado(IPAddress addr)
        {
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteDesconectado(addr); }));
            else
            {
                MessageBox.Show("Seu inimigo não aguentou a pressão e desistiu");
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
                bool b = MessageBox.Show(this, "O usuário " + usuario.NomeRemoto + " ( "+ addr.ToString() + " ) deseja realizar conexão. Aceitar?", "Conexão", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
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
