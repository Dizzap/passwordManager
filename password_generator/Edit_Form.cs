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

namespace password_generator
{
    public partial class Edit_Form : Form
    {
        public string ps_string = "";
        public Edit_Form()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Gen_button_Click(object sender, EventArgs e) {
            //for (int i = 0; i < 3; i++) {
                int pss_lenght = Convert.ToInt32(numericUpDown1.Value);
                int pos_left = pss_lenght;
                //char[] ps_arr = new char[pss_lenght];

                //int act_pos;
                int ran;
                Random rn = new Random();

                int lower_case_count = rn.Next(pss_lenght / 3, (pss_lenght / 2 + 1));
                pos_left = pos_left - lower_case_count;

                int upper_case_count = pos_left / 2;
                pos_left = pos_left - pos_left / 2;

                int number_count = pos_left;

                pos_left = pss_lenght;
                while (pos_left > 0) {
                    //char as_char = Convert.ToChar(rn.Next(65, 91));
                    ran = rn.Next(0, 3);
                    switch (ran) {
                        case (0): if (lower_case_count > 0) {
                                ps_string += Convert.ToChar(rn.Next(97, 123));
                                lower_case_count--;
                                pos_left--;
                            }
                            else { /*ran= rn.Next(1,3);*/}
                            break;
                        case (1): if (upper_case_count > 0) {
                                ps_string += Convert.ToChar(rn.Next(65, 91));
                                upper_case_count--;
                                pos_left--;
                            }
                            else { /*ran = rn.Next(0, 3);*/ }
                            break;
                        case (2): if (number_count > 0) {
                                ps_string += Convert.ToChar(rn.Next(48, 58));
                                number_count--;
                                pos_left--;
                            }
                            else { }
                            break;
                    }
                    //ps_arr[i] = Convert.ToChar(rn.Next(65, 91));

                }
                textBox2.Text = "";
                textBox2.Text = ps_string;
                ps_string = "";
                //DataContainer.generated = true;
                /*for (int j = 0; j < ps_arr.Length; j++) {
                    textBox2.Text += ps_arr[j];
                }*/
         //   }
        }

        private void acceptButton_Click(object sender, EventArgs e) {
            if (textBox1.Text == "") {
                MessageBox.Show("Password name can't be blank!");
            }
            else {                
                DataContainer.ps_name[DataContainer.selected] = textBox1.Text;
                DataContainer.ps_text[DataContainer.selected] = textBox2.Text;
                DataContainer.changed = true; 
                this.Close();                 
            }
        }

        private void Edit_Form_Load(object sender, EventArgs e) {
            textBox1.Text = DataContainer.ps_name[DataContainer.selected];
            textBox2.Text = DataContainer.ps_text[DataContainer.selected];
        }

        private void Edit_Form_FormClosing(object sender, FormClosingEventArgs e) {
                   
        }
    }
}
