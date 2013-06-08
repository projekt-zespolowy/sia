using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsTesting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TradeForm());

            /*
             * this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            listViewGroup1.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1, listViewItem2, listViewItem3 });
            listViewGroup2.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem4, listViewItem5});
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            
             * */
        }
    }
}
