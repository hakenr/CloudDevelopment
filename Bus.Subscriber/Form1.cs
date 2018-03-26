using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.ServiceBus;
using Message = Microsoft.Azure.ServiceBus.Message;

namespace Bus.Subscriber
{
    public partial class Subscriber : Form
    {
        private readonly ISubscriptionClient _subscriptionClient;

        public Subscriber()
        {
            InitializeComponent();

            _subscriptionClient = new SubscriptionClient(ConfigurationManager.AppSettings.Get("ServiceBusConnectionString"), "demotopic", "demosubscription");
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            // todo: log exception
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            MessageText.Text += Encoding.UTF8.GetString(message.Body) + Environment.NewLine;

            Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(1000, 2000));

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private void SubscribeButton_Click(object sender, EventArgs e)
        {
            SubscribeButton.Enabled = false;

            _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync,
                new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                }
            );
        }
    }
}
