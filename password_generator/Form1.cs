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
using System.Threading;
using System.Security;
using System.Security.Cryptography;

namespace password_generator {
    public partial class Form1 : Form {
        /*public void Updateform(){
            label1.Text=DataContainer.ps_name;
            textBox1.Text = DataContainer.ps_text;
            if (DataContainer.generated == true)
                textBox1.ReadOnly = true;
        }*/     
        StreamWriter sw;
        StreamReader sr;

        public Form1() {
            ///příprava polí
            int numOfRows = 0;
            StreamReader srr = new StreamReader(DataContainer.passwordFilePath);
            while ((srr.ReadLine() != null)) {
                numOfRows++;
            }
            srr.Close();
            DataContainer.ps_name = new string[numOfRows/2];
            DataContainer.ps_text = new string[numOfRows/2];

            ///načtení user hesla?
            /*StreamReader sread = new StreamReader(DataContainer.configFilePath);
            DataContainer.passPhrase = sread.ReadLine();
            sread.Close();*/
            
            ///načtení hesel do array a dešifrování
            sr = new StreamReader(DataContainer.passwordFilePath);
            for (int i = 0; i < DataContainer.ps_name.Length; i++) {
                string name = sr.ReadLine();
                string pass = sr.ReadLine();
                if ((name != "") && (name != null)) {
                    DataContainer.ps_name[i] = DataContainer.Decrypt(name, DataContainer.configInit[0]);
                    DataContainer.ps_text[i] = DataContainer.Decrypt(pass, DataContainer.configInit[0]);
                    
                }
            }
            sr.Close();
            InitializeComponent(); 
 
            ///naštení hesel do listboxu
            for (int i = 0; i < DataContainer.ps_name.Length; i++) {
                listBox1.Items.Add(DataContainer.ps_name[i]);
            }           
        }
        private void button1_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        private void Edit_button_Click(object sender, EventArgs e) {
            /// otevření okna editace hesla
            DataContainer.selected = listBox1.SelectedIndex;
            if (DataContainer.selected >= 0) {                
                Edit_Form fm = new Edit_Form();
                fm.ShowDialog();
            }    
        }
        private void Form1_Load(object sender, EventArgs e) {
            
        } 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            sw = new StreamWriter(DataContainer.passwordFilePath);
            //provizorně na celé pole
            ///zápis hesel do souboru + zašifrování
            for (int i = 0; /*DataContainer.ps_name[i]!=null*/ i < DataContainer.ps_name.Length; i++) {
                if (DataContainer.ps_name[i] != null) {
                    sw.WriteLine(DataContainer.Encrypt(DataContainer.ps_name[i], DataContainer.configInit[0]));
                    sw.WriteLine(DataContainer.Encrypt(DataContainer.ps_text[i], DataContainer.configInit[0]));
                }
            }
            sw.Close();
            Application.Exit();
        }
        private void Del_button_Click(object sender, EventArgs e) {
            DataContainer.selected = listBox1.SelectedIndex;
            if (DataContainer.selected >= 0) {
                DataContainer.ps_name[DataContainer.selected] = null;
                DataContainer.ps_text[DataContainer.selected] = null;
                listBox1.Items.RemoveAt(DataContainer.selected);
            }      
        }
        private void addButton_Click(object sender, EventArgs e) {
            int count = 0;
            for (int i = 0; DataContainer.ps_name[i] != null; i++) {
                count++;      
            }            
            Edit_Form fm = new Edit_Form();
            DataContainer.selected = count;
            timer1.Enabled = true;            
            fm.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e) {
            if (DataContainer.changed == true) {
                listBox1.Items.Add(DataContainer.ps_name[DataContainer.selected]);
                DataContainer.changed = false;
                timer1.Enabled = false;
            }                            
        }

        private void listBox1_DoubleClick(object sender, EventArgs e) {
            Edit_Form fm = new Edit_Form();
            DataContainer.selected = listBox1.SelectedIndex;
            if (DataContainer.selected == -1) {
                DataContainer.selected = 0;
            }
            fm.ShowDialog();
        }

        private void settingButton_Click(object sender, EventArgs e) {

        }

        private void setButton_Click(object sender, EventArgs e) {
            settingForm sf = new settingForm();
            sf.ShowDialog();
        }
        /*public string Encrypt(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(DataContainer.initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(DataContainer.saltValue);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[] rgbKey = new PasswordDeriveBytes(DataContainer.passPhrase, rgbSalt, DataContainer.hashAlgorithm, DataContainer.passwordIterations).GetBytes(DataContainer.keySize / 8);
            RijndaelManaged managed = new RijndaelManaged();
            managed.Mode = CipherMode.CBC;
            ICryptoTransform transform = managed.CreateEncryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }*/

       /*public string Decrypt(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(DataContainer.initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(DataContainer.saltValue);
            byte[] buffer = Convert.FromBase64String(data);
            byte[] rgbKey = new PasswordDeriveBytes(DataContainer.passPhrase, rgbSalt, DataContainer.hashAlgorithm, DataContainer.passwordIterations).GetBytes(DataContainer.keySize / 8);
            RijndaelManaged managed = new RijndaelManaged();
            managed.Mode = CipherMode.CBC;
            ICryptoTransform transform = managed.CreateDecryptor(rgbKey, bytes);
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            byte[] buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return Encoding.UTF8.GetString(buffer5, 0, count);
        }        */
    }
    
}
