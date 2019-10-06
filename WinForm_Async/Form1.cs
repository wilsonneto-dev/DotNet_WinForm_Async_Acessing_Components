using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Async
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private delegate void SafeCallDelegate(string text);
        private void WriteTextSafe(string text)
        {
            if (txt1.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                txt1.Invoke(
                    d,
                    new object[] { text }
                );
            }
            else
            {
                txt1.Text = text;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(3000);
                    WriteTextSafe($"Executou LOOP 1 {i} {Environment.NewLine }{txt1.Text}");
                }
            });
        }

        private async void btn2_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(3000);
                    WriteTextSafe($"Executou LOOP 2 {i} {Environment.NewLine }{txt1.Text}");
                }
            });
        }

    }
}
