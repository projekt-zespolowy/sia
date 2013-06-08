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
        public GraphForm()
        {
            InitializeComponent();
        }

        private void GraphBox_Enter(object sender, EventArgs e)
        {

        }
        /*
        private void GraphBox_Paint(object sender, EventArgs e)
        {
            using (Graphics g = this.GraphBox.CreateGraphics())
            {
                Pen pen = new Pen(Color.Black, 2);
                Brush brush = new SolidBrush(Color.Black);

                g.DrawRectangle(pen, 100, 100, 100, 200);

                pen.Dispose();
            }
        }*/
    }
}
