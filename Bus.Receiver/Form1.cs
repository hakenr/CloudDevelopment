using System;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.ServiceBus;
using Message = Microsoft.Azure.ServiceBus.Message;

namespace Bus.Receiver
{
    public partial class Receiver : Form
    {
        public Receiver()
        {
            InitializeComponent();
        }

        private void ReceiveButton_Click(object sender, EventArgs e)
        {
            ReceiveButton.Enabled = false;

            // handle button click here
        }
    }
}
