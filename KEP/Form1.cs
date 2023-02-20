using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEP
{
    public partial class Form1 : Form
    {
        String name;
        UserKEP user;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            user = new UserKEP();
            name = textBox1.Text;
            user.Name = name;

            this.Hide();
            Form2 form2 = new Form2(user);
            form2.Show();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = this.Location;
            form2.Size = this.Size;
        }
    }
}
