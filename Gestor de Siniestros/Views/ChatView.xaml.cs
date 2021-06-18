using Gestor_de_Siniestros.Models.DB;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_de_Siniestros.Views
{

    public partial class ChatView : UserControl
    {

        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        String msjServidor = "";

        public ChatView()
        {
            InitializeComponent();
        }

        public void LoadData(Usuarios currentUsuario)
        {
            try
            {
                Console.WriteLine("Conectando al servidor...");
                clientSocket.Connect("127.0.0.1", 1234);
                serverStream = clientSocket.GetStream();
                String nombreChat = currentUsuario.nombre + " " + currentUsuario.aPaterno;
                byte[] outStream = Encoding.ASCII.GetBytes(nombreChat + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                Thread threadListen = new Thread(escucharMensajes);
                threadListen.Start();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtBoxMensaje.Text.Length > 0)
            {
                byte[] outStream = Encoding.ASCII.GetBytes(txtBoxMensaje.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                txtBoxMensaje.Text = " ";
            }
            else
            {
                MessageBox.Show("Debes escribir un mensaje");
            }
        }

        private void escucharMensajes()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                byte[] inStream = new byte[65537];
                serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);

                string returndata = Encoding.ASCII.GetString(inStream);
                msjServidor += returndata + " ";

                Console.WriteLine("Mensaje del servidor: " + returndata);

                txtBoxChat.Dispatcher.Invoke((ThreadStart)delegate
                {
                    txtBoxChat.Clear();
                    txtBoxChat.Text += msjServidor;
                });
            }
        }
    }
}
