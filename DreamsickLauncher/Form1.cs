using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace DreamsickLauncher
{
    public partial class Form1 : Form
    {
        settings s = new settings();
        private PrivateFontCollection pfc = new PrivateFontCollection();
        private bool moving = false;
        private Point lastLocation = new Point(0, 0);

        public Form1()
        {
            InitializeComponent();
            this.Visible = false;
            this.MaximizeBox = false;
            label1.MouseMove += Label_MouseMove;
            label1.MouseLeave += Label_MouseLeave;
            label2.MouseMove += Label_MouseMove;
            label2.MouseLeave += Label_MouseLeave;
            label3.MouseMove += Label_MouseMove;
            label3.MouseLeave += Label_MouseLeave;
            label4.MouseMove += Label_MouseMove;
            label4.MouseLeave += Label_MouseLeave;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
            this.Load += Form1_Load1;

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
                if (c is Label l && c != label5)
                {
                    l.Font = new Font(pfc.Families[0], 30, FontStyle.Regular);
                }
            }
        }

        private void Form1_Load1(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                this.Location = new Point(
                (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            lastLocation = e.Location;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Enabled = false;
            if (File.Exists("dreamsick.exe"))
            {
                Process.Start("dreamsick.exe");
            }
            else if (File.Exists("dreamsick.py"))
            {
                Process p = new Process();
                p.StartInfo.FileName = "python.exe";
                List<string> args = new List<string>{ "dreamsick.py"};
                if (settings.fullscreen)
                {
                    args.Add("-fullscreen ");
                }
                if (settings.showDebug)
                {
                    args.Add("-debug ");
                }
                args.Add("-resx:" + settings.resolution.Width.ToString() + " ");
                args.Add("-resy:" + settings.resolution.Height.ToString() + " ");

                foreach (string a in args)
                {
                    p.StartInfo.Arguments += a + " ";
                }

                p.Start();
            }
            else
            {
                MessageBox.Show("Could not locate dreamsick.exe", "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Exit();
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

        private void label2_Click(object sender, EventArgs e)
        {
            s.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
