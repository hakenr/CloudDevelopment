using System;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using Microsoft.Azure.ServiceBus;
using Message = Microsoft.Azure.ServiceBus.Message;

namespace Bus.Sender
{
    public partial class Sender : Form
    {
        private readonly QueueClient _queueClient;

        public Sender()
        {
            InitializeComponent();

            _queueClient = new QueueClient(ConfigurationManager.AppSettings.Get("ServiceBusConnectionString"), "demo");
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            byte[] messageBody = Encoding.UTF8.GetBytes(MessageText.Text);
            await _queueClient.SendAsync(new Message(messageBody));

            MessageText.Text = string.Empty;
        }
    }
}
