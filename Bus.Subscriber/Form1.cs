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
        public Subscriber()
        {
            InitializeComponent();
        }

        private void SubscribeButton_Click(object sender, EventArgs e)
        {
            SubscribeButton.Enabled = false;

            // handle button click here
        }
    }
}
