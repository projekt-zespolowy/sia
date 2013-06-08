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
    

    public partial class SolveEventForm : Form
    {
        public Hub Hub;
        public List<EventInfo> lei;
        public SolveEventForm(Hub h)
        {
            Hub = h;

            InitializeComponent();

            lei = new List<EventInfo>();

            foreach (int eid in Hub.EventNums())
            {
                if (eid == 0)
                    continue;

                lei.Add(Hub.GetEventInfo(eid));
            }

            foreach(EventInfo ei in lei)
            {
                comboBox1.Items.Add(new NumberedString(ei.name, ei.eid));
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            foreach (string str in lei[comboBox1.SelectedIndex].wyns)
            {
                comboBox2.Items.Add(str);
            }

            comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            really = false;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EID = ((NumberedString)comboBox1.SelectedItem).ID;
            CID = comboBox2.SelectedIndex;
            really = true;

            Close();
        }
        public int EID, CID;
        public bool really;
    }
    public class NumberedString
    {
        public int ID;
        public String Text;

        public NumberedString(String str, int dd)
        {
            ID = dd;
            Text = str;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
