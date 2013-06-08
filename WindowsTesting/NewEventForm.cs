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
    public partial class NewEventForm : Form
    {
        public int risk;
        public string name;
        public string[] cases;
        public NewEventForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox2.Text);
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // anuluj

            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ok

            DialogResult = System.Windows.Forms.DialogResult.OK;

            risk = (int)Math.Round(numericUpDown1.Value * 100.0m);
            name = textBox1.Text;

            cases = new string[listBox1.Items.Count];

            for (int i = 0; i < cases.Length; i++)
            {
                cases[i] = (string)listBox1.Items[i];
            }

            Close();
        }
    }
}
