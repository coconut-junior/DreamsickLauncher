using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DreamsickLauncher
{
    public partial class activation : Form
    {
        string hash = "b5265b3fb40c830beb72c2892c59d891";
        string licenseKey = "";
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public activation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(keygen.getKey());
            if (isKeyValid(textBox1.Text + textBox2.Text + textBox3.Text + textBox4.Text))
            {
                if (!Directory.Exists(appdata + @"\Dreamsick"))
                {
                    Directory.CreateDirectory(appdata + @"\Dreamsick");
                }

                File.WriteAllText(appdata + @"key", encrypt(licenseKey));
            }
            else
            {
                MessageBox.Show("License key could not be validated. Please check for errors.", "Activation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool isKeyValid(string key)
        {
            return false;
        }

        private string encrypt(string s)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(s);
            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using(TripleDESCryptoServiceProvider t = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = t.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public string decrypt(string s)
        {
            byte[] data = Convert.FromBase64String(s);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider t = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = t.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}
