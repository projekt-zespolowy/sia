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
        EventInfo active_event;
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
            max_value_num = 50;
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
            active_event = hub.GetEventInfo(EID);
            float val = (float)active_event.kursy[CID];

            PushValue(val);
            this.GraphBox.Refresh();
        }

        private void GraphBox_Enter(object sender, EventArgs e)
        {

        }

        private void GraphBox_Paint(object sender, PaintEventArgs e)
        {
            int width = this.GraphBox.Width-50;
            int height = this.GraphBox.Height-30;
            int x, y, x_prev, y_prev;
            float val;
            float spread = (float)width / (float)max_value_num;

            Brush brush_black = new SolidBrush(Color.Black);
            Brush brush_green = new SolidBrush(Color.Green);
            Pen pen2 = new Pen(Color.Green, 2.0F);
            Pen pen1 = new Pen(Color.Green, 1.0F);
            Font drawFont = new Font("Arial", 8);

            e.Graphics.FillRectangle(brush_black, 10, 20, width, height);
            e.Graphics.DrawRectangle(pen2, 10, 20, width, height);

            x = width + 15;
            y = 10 + height - (height * 9 / 10);
            e.Graphics.DrawString(maximum_value.ToString(), drawFont, brush_green, x, y);

            x = width + 15;
            y = 10 + height;
            e.Graphics.DrawString(0.ToString(), drawFont, brush_green, x, y);

            if (values.Count() > 0)
            {
                val = values.ElementAt(0) / maximum_value * ((float)height * 9 / 10);
                x = 10 + width - dot_size;
                y = 20 + height - (int)val - dot_size;
                for (int i = 1; i < values.Count(); i++)
                {
                    x_prev = x;
                    y_prev = y;

                    val = values.ElementAt(i) / maximum_value * ((float)height * 9 / 10);
                    x = 10 + width - i * (int)spread;
                    y = 20 + height - (int)val;

                    //e.Graphics.FillEllipse(brush_green, x - dot_size / 2, y - dot_size / 2, dot_size, dot_size);

                    e.Graphics.DrawLine(pen1, x_prev, y_prev, x, y);

                }
            }

            pen1.Dispose();
            pen2.Dispose();
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
            EID = lei[comboBox1.SelectedIndex].eid;
            CID = comboBox2.SelectedIndex;
            values.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            EID = lei[comboBox1.SelectedIndex].eid;
            CID = comboBox2.SelectedIndex;
            values.Clear();
        }
    }
}
