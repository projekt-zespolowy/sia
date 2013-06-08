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
    public partial class GraphForm : Form
    {
        Queue<float> values = new Queue<float>();
        private Timer timer; 

        int max_value_num;
        int event_id;
        int dot_size;

        public GraphForm(int id)
        {
            InitializeComponent();
            event_id = id;
            max_value_num = 100;
            dot_size = 4;

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
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            PushValue(50);
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
                val = values.ElementAt(i);
                x = 10 + width - i * (int)spread - dot_size;
                y = 20 + height - (int)val - dot_size;

                e.Graphics.FillEllipse(brush_green, x - dot_size / 2, y - dot_size / 2, dot_size, dot_size);
            }

            pen.Dispose();
            brush_black.Dispose();
            brush_green.Dispose();
        }
    }
}
