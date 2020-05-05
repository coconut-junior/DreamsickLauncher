using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DreamsickLauncher
{
    public partial class settings : Form
    {
        public static Size resolution = new Size(800,600);
        public static bool fullscreen = true;
        public static bool ambientSound = true;
        public static bool playerSound = true;
        public static bool music = true;
        public static bool hardwareAcceleration = true;
        public static bool showDebug = false;

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        WebClient client = new WebClient();
        dynamic data = JArray.Parse("[]");

        public settings()
        {
            SetWindowTheme(Handle, "", "");
            Invalidate();
            InitializeComponent();
            this.CenterToScreen();
            comboBox1.Items.Add(Screen.PrimaryScreen.Bounds.Width + "x" + Screen.PrimaryScreen.Bounds.Height);
            comboBox1.Text = Screen.PrimaryScreen.Bounds.Width + "x" + Screen.PrimaryScreen.Bounds.Height;
        
            foreach (Control c in this.Controls)
            {
                if (c is Button b)
                {
                    b.FlatStyle = FlatStyle.System;
                }
                else if (c is ComboBox cb)
                {
                    cb.FlatStyle = FlatStyle.System;
                }
                else if (c is CheckBox ck)
                {
                    ck.FlatStyle = FlatStyle.System;
                }

                SetWindowTheme(c.Handle, "", "");
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(this.Handle, "Explorer", null);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Int32.Parse(comboBox1.Text.Split('x')[0]);
                int y = Int32.Parse(comboBox1.Text.Split('x')[1]);
                resolution = new Size(x, y);
            }
            catch
            {
                MessageBox.Show("Invalid resolution selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (comboBox1.Text == "Fullscreen")
                {
                    fullscreen = true;
                }
                else
                {
                    fullscreen = false;
                }

                ambientSound = checkBox1.Checked;
                playerSound = checkBox2.Checked;
                music = checkBox3.Checked;
                hardwareAcceleration = checkBox4.Checked;
                showDebug = checkBox5.Checked;

                this.Hide();
            }
            catch
            {
                MessageBox.Show("Failed to save current settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
