using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hubx;

namespace WindowsTesting
{
    public partial class ChangeUserMoney : Form
    {
        private Hub h;
        public int uid;
        public int newmoney;
        public ChangeUserMoney(Hub hh)
        {
            h = hh;
            InitializeComponent();

            foreach (int uid in h.UserNums())
            {
                if(uid != 0)
                    comboBox1.Items.Add(new NumberedString(h.GetUserInfo(uid).name, uid));
            }

            comboBox1.SelectedIndex = 0;
        }

        private void ChangeUserMoney_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uid = ((NumberedString)comboBox1.SelectedItem).ID;
            newmoney = (int)Math.Round(100.0m * numericUpDown1.Value);

            DialogResult = System.Windows.Forms.DialogResult.OK;

            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserInfo user = h.GetUserInfo(((NumberedString)comboBox1.SelectedItem).ID);
            numericUpDown1.Value = ((decimal)((double)user.money)) / 100.0m;
        }
    }
}
