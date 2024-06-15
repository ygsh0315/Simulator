using Microsoft.Maui.Controls;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public partial class MainPage : ContentPage
    {
        private const int port = 9876; // 유니티가 전송하는 포트 번호
        private UdpClient udpClient;

        public MainPage()
        {
            InitializeComponent();
            udpClient = new UdpClient(port); // 생성자에서 초기화
            Task.Run(() => StartListening());
        }

        private async Task StartListening()
        {
            var remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

            while (true)
            {
                var receivedResults = await udpClient.ReceiveAsync();
                string receivedMessage = Encoding.UTF8.GetString(receivedResults.Buffer);
                Dispatcher.Dispatch(() => UpdateLabel(receivedMessage)); // 변경된 부분
            }
        }

        private void UpdateLabel(string message)
        {
            if (receivedLabel != null)
            {
                receivedLabel.Text = $"Received: {message}";
            }
        }
    }
}