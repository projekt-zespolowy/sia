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
    public partial class ModifyRisk : Form
    {
        private Hub h;
        public int eid, risk;
        public bool smart;
        public ModifyRisk(Hub hh)
        {
            InitializeComponent();

            h = hh;

            foreach (int eid in h.EventNums())
            {
                if (eid == 0)
                    continue;

                EventInfo ei = h.GetEventInfo(eid);
                comboBox1.Items.Add(new NumberedString(ei.name, ei.eid));
            }

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventInfo ei = h.GetEventInfo(((NumberedString)comboBox1.SelectedItem).ID);
            numericUpDown1.Value = ((decimal)ei.ryzyko) / 100.0m;

            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;

            eid = ((NumberedString)comboBox1.SelectedItem).ID;
            risk = (int)Math.Round(100.0m * numericUpDown1.Value);

            smart = checkBox1.Checked;

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
