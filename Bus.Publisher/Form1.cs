using System;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using Microsoft.Azure.ServiceBus;
using Message = Microsoft.Azure.ServiceBus.Message;

namespace Bus.Publisher
{
    public partial class Publisher : Form
    {
        private readonly ITopicClient _topicClient;

        public Publisher()
        {
            InitializeComponent();

            _topicClient = new TopicClient(ConfigurationManager.AppSettings.Get("ServiceBusConnectionString"), "demotopic");
        }

        private async void PublishButton_Click(object sender, EventArgs e)
        {
            byte[] messageBody = Encoding.UTF8.GetBytes(MessageText.Text);
            await _topicClient.SendAsync(new Message(messageBody));

            MessageText.Text = string.Empty;
        }

    }
}
