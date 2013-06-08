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
    public partial class GraphForm : Form
    {
        Hub hub;
        List<EventInfo> lei;
        Queue<float> values = new Queue<float>();
        private Timer timer; 

        int max_value_num;
        int EID, CID;
        int dot_size;
        float maximum_value;

        public GraphForm(Hub h)
        {
            InitializeComponent();
            hub = h;
            max_value_num = 100;
            dot_size = 4;
            lei = new List<EventInfo>();

            foreach (int eid in hub.EventNums())
            {
                if (eid == 0)
                    continue;

                lei.Add(hub.GetEventInfo(eid));
            }

            foreach (EventInfo ei in lei)
            {
                comboBox1.Items.Add(new NumberedString(ei.name, ei.eid));
            }
            comboBox1.SelectedIndex = 0;

            this.GraphBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphBox_Paint);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 500; // in miliseconds
            timer.Start();
        }

        public void SetGraphLen(int len)
        {
            max_value_num = len;
        }

        public void PushValue(float val)
        {
            values.Enqueue(val);
            while (values.Count() > max_value_num)
                values.Dequeue();
            maximum_value = values.Max();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            float val = (float)lei[EID].kursy[CID];

            PushValue(val);
            this.GraphBox.Refresh();
        }

        private void GraphBox_Enter(object sender, EventArgs e)
        {

        }

        private void GraphBox_Paint(object sender, PaintEventArgs e)
        {
            int width = this.GraphBox.Width-20;
            int height = this.GraphBox.Height-30;
            int x, y;
            float val;
            float spread = (float)width / (float)max_value_num;

            Brush brush_black = new SolidBrush(Color.Black);
            Brush brush_green = new SolidBrush(Color.Green);
            Pen pen = new Pen(Color.Green, 2.0F);

            e.Graphics.FillRectangle(brush_black, 10, 20, width, height);
            e.Graphics.DrawRectangle(pen, 10, 20, width, height);

            for (int i = 0; i < values.Count(); i++)
            {
                val = values.ElementAt(i) / maximum_value * ((float)height * 3 / 4);
                x = 10 + width - i * (int)spread - dot_size;
                y = 20 + height - (int)val - dot_size;

                e.Graphics.FillEllipse(brush_green, x - dot_size / 2, y - dot_size / 2, dot_size, dot_size);
            }

            pen.Dispose();
            brush_black.Dispose();
            brush_green.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (string str in lei[comboBox1.SelectedIndex].wyns)
            {
                comboBox2.Items.Add(str);
            }
            comboBox2.SelectedIndex = 0;
            EID = comboBox1.SelectedIndex;
            CID = comboBox2.SelectedIndex;
            values.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            EID = comboBox1.SelectedIndex;
            CID = comboBox2.SelectedIndex;
            values.Clear();
        }
    }
}
