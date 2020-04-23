using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace DreamsickLauncher
{
    public partial class Form1 : Form
    {
        settings s = new settings();
        private PrivateFontCollection pfc = new PrivateFontCollection();

        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            label1.MouseMove += Label_MouseMove;
            label1.MouseLeave += Label_MouseLeave;
            label2.MouseMove += Label_MouseMove;
            label2.MouseLeave += Label_MouseLeave;
            label3.MouseMove += Label_MouseMove;
            label3.MouseLeave += Label_MouseLeave;
            label4.MouseMove += Label_MouseMove;
            label4.MouseLeave += Label_MouseLeave;

            Stream fontStream = this.GetType().Assembly.GetManifestResourceStream("DreamsickLauncher.kenyan coffee rg.ttf");
            byte[] fontdata = new byte[fontStream.Length];
            fontStream.Read(fontdata, 0, (int)fontStream.Length);
            fontStream.Close();

            unsafe
            {
                fixed (byte* pFontData = fontdata)
                {
                    pfc.AddMemoryFont((System.IntPtr)pFontData, fontdata.Length);
                }
            }

            foreach (Control c in this.Controls)
            {
                if (c is Label l)
                {
                    l.Font = new Font(pfc.Families[0], 30, FontStyle.Regular);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            s.Show();
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.Gray;
            }
        }

        private void Label_MouseMove(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.FromArgb(137, 197, 65);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
