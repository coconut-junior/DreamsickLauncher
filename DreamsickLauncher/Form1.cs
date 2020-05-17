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
using System.Windows.Forms.VisualStyles;

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

            foreach (Control c in panel1.Controls)
            {
                if (c is Label l)
                {
                    l.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
                    if (settings.language == "jp")
                    {
                        l.Text = settings.japanese[l.Text];
                        l.Dock = DockStyle.Right;
                    }
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

        private string getArgs(string processName)
        {
            List<string> args = new List<string>();
            args.Add(processName);

            if (settings.fullscreen)
            {
                args.Add("-fullscreen");
            }
            if (settings.showDebug)
            {
                args.Add("-debug");
            }
            if (settings.skipIntro)
            {
                args.Add("-skipintro");
            }
            if (settings.removeFramecap)
            {
                args.Add("-framecap:60");
            }
            if (settings.disableZoom)
            {
                args.Add("-disablezoom");
            }
            if (settings.disableLighting)
            {
                args.Add("-disablelighting");
            }

            args.Add("-resx:" + settings.resolution.Width.ToString() + " ");
            args.Add("-resy:" + settings.resolution.Height.ToString() + " ");

            string formattedArgs = "";

            foreach (string a in args)
            {
                formattedArgs += a + " ";
            }

            return formattedArgs;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Enabled = false;
            Process p = new Process();
            p.StartInfo.FileName = "pythonw.exe";
            List<string> args = new List<string>();

            if (File.Exists("dreamsick.exe"))
            {
                p.StartInfo.Arguments = getArgs("dreamsick.exe");
                p.Start();
            }
            else if (File.Exists(@"C:\GitHub\dreamsick\src\dreamsick.py"))
            {
                p.StartInfo.WorkingDirectory = @"C:\GitHub\dreamsick\src\";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.Arguments = getArgs(@"C:\GitHub\dreamsick\src\dreamsick.py");
                p.Start();
            }
            else if (File.Exists("dreamsick.py"))
            {
                p.StartInfo.Arguments = "dreamsick.py";
                p.Start();
            }
            else
            {
                MessageBox.Show("Could not locate dreamsick executable.", "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
