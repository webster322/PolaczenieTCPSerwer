using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace PołączenieTCPSerwer
{
    public partial class Form1 : Form
    {
        private TcpListener serwer;
        private TcpClient klient;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress adresIP;

            try
            {
                adresIP = IPAddress.Parse(textBox1.Text);
            }
            catch
            {

                MessageBox.Show("Błędny format adresu IP!", "Błąd");
                textBox1.Text = String.Empty;
                return;
            }

            int port = Convert.ToInt16(numericUpDown1.Value);

            try
            {
                serwer = new TcpListener(adresIP, port);
                serwer.Start();
                klient = serwer.AcceptTcpClient();
                listBox1.Items.Add("Nawiązano połączenie");
                button1.Enabled = false;
                button2.Enabled = true;
                klient.Close();
                serwer.Stop();
            }
            catch (Exception ex)
            {

                listBox1.Items.Add("Błąd inicjacji serwera!");
                MessageBox.Show(ex.ToString(), "Błąd");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serwer.Stop();
            klient.Close();
            listBox1.Items.Add("Zakończono pracę serwera...");
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}
