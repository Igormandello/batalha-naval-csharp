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
                if (!usuario.SolicitarConexao(IPAddress.Parse(cbIPs.SelectedItem + "")))
                    MessageBox.Show(this, "Seu adversário não aceitou a conexão! :c", "Tente novamente!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Erro ao tentar enviar solicitação de conexão");
            }
        }

        private void IniciarCliente()
        {
            //Inicializa um novo cliente com o nome passado e atribui os eventos
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
            //Quando um cliente sair da rede, o remove do combobox.
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteIndisponivel(addr); }));
            else
                cbIPs.Items.Remove(addr);
        }

        private void ClienteDesconectado(IPAddress addr)
        {
            //Quando a pessoa com quem você está conectada sai do jogo
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
            //Quando você se conecta com alguém
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
            //Se tem alguem querendo se conectar a você
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
            //Quando alguem aparece na rede, o coloca no combobox
            if (InvokeRequired)
                Invoke(new Action(() => { ClienteDisponivel(addr); }));
            else if (!cbIPs.Items.Contains(addr))
                cbIPs.Items.Add(addr);
        }
    }
}
