using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace md5Encrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string hash = "batuhan";

        public string MD5Encrypt(string sifre)
        {
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(sifre);
                using (MD5CryptoServiceProvider md5 = new())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new() { Key = keys, Mode = CipherMode.ECB, Padding = System.Security.Cryptography.PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string MD5Decrypt(string SifrelenmisDeger)
        {
            try
            {
                if (SifrelenmisDeger == null)
                    return "";
                byte[] data = Convert.FromBase64String(SifrelenmisDeger);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = System.Security.Cryptography.PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return UTF8Encoding.UTF8.GetString(results);
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = MD5Encrypt(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = MD5Decrypt(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
    }


    
}