using GYNOOHLIB.Networking.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GYNOOHLIB.Networking.Protocol;
using Controller = GYNOOHLIB.Networking.Network.Controller;

namespace GYNOOH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Controller.StartListening();
            Controller.Connect(IPAddress.Parse("192.168.1.44"), ProtocolInfo.Port);
            Controller.ProtocolConnect();
        }
    }
}
