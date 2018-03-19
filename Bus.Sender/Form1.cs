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
        public Sender()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            // handle button click here
        }
    }
}
