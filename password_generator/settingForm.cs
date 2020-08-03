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

namespace password_generator {
    public partial class settingForm : Form {
        public settingForm() {
            InitializeComponent();
        }
        void writePass() {
            ///zápis nového hesla + šifrování
            StreamWriter sw = new StreamWriter(DataContainer.configFilePath);
            string toWrite = DataContainer.Encrypt(textBox1.Text);
            sw.Write(toWrite);
            sw.Close();
            DataContainer.configInit[0] = toWrite;
            this.Close();
        }
        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void acceptButton_Click(object sender, EventArgs e) {
            writePass();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                writePass();
            }
        }
        private void settingsForm_Shown(object sender, EventArgs e) {
            this.ActiveControl = textBox1;
        }
    }
}
