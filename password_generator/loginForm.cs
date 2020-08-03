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
using System.Security;

namespace password_generator {
    
    public partial class loginForm : Form {
        public loginForm() {
            InitializeComponent();
            configInit();
        }
        void configInit() {
            ///oveření složek a souborů, případné vytvoření
            if (!Directory.Exists(DataContainer.appFolderPath)) {
                DirectoryInfo di = Directory.CreateDirectory(DataContainer.appFolderPath);
            }
            if (!File.Exists(DataContainer.configFilePath)) {
                ///ověření existence config file + vytvoření
                //DataContainer.configInit[1] = "1";
                StreamWriter sw = new StreamWriter(DataContainer.configFilePath);
                sw.Write("");
                //sw = new StreamWriter(DataContainer.dataFilePath);
                //sw.Write("");
                sw.Close();
            }
            if (!File.Exists(DataContainer.dataFilePath)) {
                ///ověření existence data file + vytvoření
                //DataContainer.configInit[2] = "1";
                StreamWriter sw = new StreamWriter(DataContainer.dataFilePath);
                sw.Write("");
                sw.Close();
            }
            if (!File.Exists(DataContainer.passwordFilePath)) {
                ///oveření existence password file + vytvoření
                //DataContainer.configInit[2] = "1";
                StreamWriter sw = new StreamWriter(DataContainer.passwordFilePath);
                sw.Write("");
                sw.Close();
            }
            ///přidat data miss handling
            /*for (int i = 1; i < DataContainer.configInit.Length; i++) { 

            }*/
            
            //if null vygenerovat
            StreamReader srConfig = new StreamReader(DataContainer.configFilePath);
            for (int i = 0; i < DataContainer.configInit.Length; i++) {
                string d = srConfig.ReadLine();
                if(d != null) {
                   //DataContainer.configInit[i] = DataContainer.Decrypt(d); 
                    DataContainer.configInit[i] = d;
                }
            }
            srConfig.Close();
            
        }
        string caesDec(string inputText) {
            string dec = ""; ;
            char cTwo;
            foreach(char c in inputText){
                int val = Convert.ToInt32(c);
                val -= 5;
                cTwo = Convert.ToChar(val);
                dec += cTwo;
            }
            return dec;
        }
        string caesEnc(string inputText) {
            string enc = ""; ;
            char cTwo;
            foreach (char c in inputText) {
                int val = Convert.ToInt32(c);
                val += 5;
                cTwo = Convert.ToChar(val);
                enc += cTwo;
            }
            return enc;
        }

        void login(string boxText) {  
            ///zašifrování boxText
            boxText = DataContainer.Encrypt(boxText);

            ///vytvoření hesla při prvním spuštění
            if (DataContainer.configInit[0] == null) {
                StreamWriter sr = new StreamWriter(DataContainer.configFilePath);
                sr.WriteLine(boxText);
                sr.Close();
                DataContainer.configInit[0] = boxText;
            }

            ///porovnání hesla
            if (boxText == DataContainer.configInit[0]) {
                Form1 ss = new Form1();
                ss.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Wrong password!");
            }
        }

        private void loginButton_Click(object sender, EventArgs e) {
            login(passBox.Text);
        }
        private void closeButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void passBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                e.Handled = true;
                login(passBox.Text);
            }
        }

        private void loginForm_Shown(object sender, EventArgs e) {
            this.ActiveControl = passBox;
        }    
    }
}
