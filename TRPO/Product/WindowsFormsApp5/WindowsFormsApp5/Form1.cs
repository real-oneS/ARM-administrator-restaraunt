using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            textBox1.GotFocus += (sender, e) => { textBox1.Clear(); };
            textBox1.LostFocus += (sender, e) => { if (textBox1.Text == string.Empty) textBox1.Text = "Введите логин..."; };
            textBox2.GotFocus += (sender, e) => { textBox2.Clear(); };
            textBox2.LostFocus += (sender, e) => { if (textBox2.Text == string.Empty) textBox1.Text = "Parol'"; };
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="admin" && textBox2.Text == "admin")
            {
                Form f2 = new Form2();
                f2.Show();
                this.Hide();
            }
            else
            {
                label5.Visible = true;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
