using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsTesting
{
    public partial class NewUserForm : Form
    {
        public int Money;
        public string Username;
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // anuluj
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // OK
            Money = (int)Math.Round(100.0m * numericUpDown1.Value);
            Username = textBox1.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;

            Close();
        }
    }
}
