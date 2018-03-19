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
        public Publisher()
        {
            InitializeComponent();
        }

        private void PublishButton_Click(object sender, EventArgs e)
        {
            // handle button click here
        }
    }
}
