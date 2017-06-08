using System;
using System.Net;
using BatalhaNaval;

namespace batalha_naval
{
    public partial class GameForm
    {
        private void IniciarCliente()
        {
            usuario = new ClienteP2P("Igor", tabUser);

            usuario.OnClienteDisponivel += ClienteDisponivel;
            usuario.OnClienteIndisponivel += ClienteIndisponivel;

            usuario.OnClienteRequisitandoConexao += RequisitandoConexao;
            usuario.OnClienteConectado += ClienteConectado;
            usuario.OnClienteDesconectado += ClienteDesconectado;
            usuario.OnTiroRecebido += TiroRecebido;

            usuario.Iniciar();

            //InicializarJogo();

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
            inGame = false;
        }

        private void ClienteConectado(IPAddress addr)
        {
            inGame = true;
        }

        private bool RequisitandoConexao(System.Net.IPAddress addr)
        {
            return true;
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
