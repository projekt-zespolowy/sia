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
    public partial class TradeForm : Form
    {
        private Hub h;
        private bool nosel;
        private CourseHoldAgent cha, cha2, cha3;
        public TradeForm()
        {
         
            InitializeComponent();
            h = new Hub();
            nosel = false;
            
            h.MakeUser("Kozak");
            h.SetMoney(1, 100000);

            h.MakeUser("Wojtek");
            h.SetMoney(2, 100000);

            h.MakeEvent(200000, "Finał", new string[] { "Borussia Dortmund", "(REMIS)", "Bayern Munchen" });
            h.MakeEvent(200000, "Konklawe", new string[] { "Bergoglio", "Scola", "Turkson", "Bertone", "Amato", 
                "Bettori", "Ortega", "Inny kardynał"});
            h.MakeEvent(30000, "Egzotyczny", new string[] { "A", "B" });
            h.MakeEvent(300000, "Popularny", new string[] { "A", "B" });

             cha = new CourseHoldAgent();
            cha.uid = 1;
            cha.eid = 2;
            cha.cid = 4;
            cha.money = 700000;
            cha.tmin = 50;
            cha.tmax = 300;
            cha.reaction = 0.2;
            cha.h = h;
            cha.Run();
            cha.cref = 2.5;

            cha2 = new CourseHoldAgent();
            cha2.uid = 1;
            cha2.eid = 2;
            cha2.cid = 5;
            cha2.money = 200000;
            cha2.tmin = 50;
            cha2.tmax = 300;
            cha2.reaction = 0.2;
            cha2.h = h;
            cha2.Run();
            cha2.cref = 8.0;

            cha3 = new CourseHoldAgent();
            cha3.uid = 1;
            cha3.eid = 2;
            cha3.cid = 6;
            cha3.money = 200000;
            cha3.tmin = 50;
            cha3.tmax = 300;
            cha3.reaction = 0.2;
            cha3.h = h;
            cha3.Run();
            cha3.cref = 7.5;

            ReviseInfo();
            comboBox1.SelectedIndex = 0;
            listView1.Items[0].Selected = true;
        }
        ~TradeForm()
        {
            
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            cha.Suspend();
            cha2.Suspend();
            cha3.Suspend();
        }
        private int SelectedUser()
        {
            try
            {
                return ((UserInfo)comboBox1.SelectedItem).uid;
            }
            catch 
            {
                return -1;
            }
        }
        private int SelectedCase()
        {
            try
            {
                return (int)listView1.SelectedItems[0].Tag;
            }
            catch 
            {
                return -1;
            }
        }
        private int SelectedEvent()
        {
            try
            {
                return (int)listView1.SelectedItems[0].Group.Tag;
            }
            catch 
            {
                return -1;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
           // Text = listView1.S
        }
        private void ReviseUserList()
        {
        }
        private void HandleEvent(bool revise)
        {
            if(revise)
                ReviseInfo();

            int eid = 1;
            int cid = 0;
            try
            {
                label8.Text = listView1.SelectedItems[0].Group.Header + " " + listView1.SelectedItems[0].SubItems[1].Text;

                eid = (int)listView1.SelectedItems[0].Group.Tag;
                cid = (int)listView1.SelectedItems[0].Tag;
            }
            catch {
                return;
            }

            

            int staw = (int)(numericUpDown1.Value * 100.0m);

            int koszt = h.Trade((((UserInfo)comboBox1.SelectedItem).uid), eid, cid, staw, false);

            label3.Text = (((double)koszt) / 100.0).ToString("0.00");

            label5.Text = (h.GetUserInfo(((UserInfo)comboBox1.SelectedItem).uid).money/100.0).ToString("0.00");
            label7.Text = (h.PersonalStake(((UserInfo)comboBox1.SelectedItem).uid, eid, cid) / 100.0).ToString("0.00");

            toolStripStatusLabel1.Text = "(Kurs: " + (((double)staw) / (double)koszt).ToString("0.000") + ")";

            // rozpatrujemy, czy zlecenie jest ok

            bool ok = false;
            if (staw >= 0 && koszt <= h.GetUserInfo(((UserInfo)comboBox1.SelectedItem).uid).money)
                ok = true;
            if (staw < 0 && -staw <= h.PersonalStake(((UserInfo)comboBox1.SelectedItem).uid, eid, cid))
                ok = true;

            if (ok)
            {
                numericUpDown1.BackColor = Color.LightGreen;
                button1.Enabled = true;
            }
            else
            {
                numericUpDown1.BackColor = Color.LightSalmon;
                button1.Enabled = false;
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleEvent(false);
        }
        private bool revised = false;
        private void ReviseInfo()
        {
            if (revised)
                return;
            revised = true;
            tradeGroupBox.Enabled = false;
            
            // usuwamy wszystkie dane
            int old_uid = SelectedUser();
            int old_eid = SelectedEvent();
            int old_cid = SelectedCase();

            comboBox1.Items.Clear();
            listView1.Items.Clear();
            listView1.Groups.Clear();

            // wczytujemy uzytkownikow
            int[] unums = h.UserNums();
            
            foreach (int uid in unums)
            {
                if (uid == 0)
                    continue;

                UserInfo ui = h.GetUserInfo(uid);
                comboBox1.Items.Add(ui);

                if (uid == old_uid)
                    comboBox1.SelectedItem = ui;
            }

            int[] nums = h.EventNums();

            foreach (int eid in nums)
            {
                EventInfo ei = h.GetEventInfo(eid);

                ListViewGroup lvg = new ListViewGroup();
                lvg.Header = ei.name;
                lvg.Tag = eid;

                listView1.Groups.Add(lvg);
                for (int i = 0; i < ei.N; i++)
                {
                    ListViewItem lvi = new ListViewItem();

                    string[] text = new string[3];
                    text[0] = ei.wyns[i];
                    text[1] = ei.kursy[i].ToString("0.0000");
                    text[2] = "";

                    if (SelectedUser() != -1)
                    {
                        if (h.PersonalStake(SelectedUser(), eid, i) != 0)
                        {
                            text[2] = (((double)h.PersonalStake(SelectedUser(), eid, i)) / 100.0).ToString(".00");

                            lvi.BackColor = Color.FromArgb(255, 255, 128);
                        }
                    }
                    lvi.SubItems.AddRange(text);
                    lvi.Tag = i;

                    lvg.Items.Add(lvi);
                    listView1.Items.Add(lvi);

                    if (eid == old_eid && i == old_cid)
                    {
                        lvi.Selected = true;
                    }

                    if (SelectedUser() >= 0 && SelectedCase() >= 0)
                        tradeGroupBox.Enabled = true;
                }

            }

            // konto systemu
            toolStripStatusLabel2.Text = "Konto bukmachera: " + 
                (((double)h.GetUserInfo(0).money) / 100.0).ToString("0.00");

            revised = false;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                h.Trade(SelectedUser(), SelectedEvent(), SelectedCase(), (int)(numericUpDown1.Value * 100.0m), true);
                ReviseInfo();

            }
            catch { }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            HandleEvent(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HandleEvent(true);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleEvent(true);
        }

        private void czyśćBazęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            h.ClearDB();
            ReviseInfo();
        }

        private void wypełnijDanymiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            h.TestFill();
            ReviseInfo();
        }

        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rozwiążToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveEventForm sef = new SolveEventForm(h);
            sef.ShowDialog();

            if (sef.really)
            {
                h.SolveEvent(sef.EID, sef.CID);
                ReviseInfo();
            }
        }

        private void dodajNowegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUserForm nuf = new NewUserForm();
            if (nuf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                h.SetMoney(h.MakeUser(nuf.Username), nuf.Money);
            }

            ReviseInfo();
        }

        private void zmieńStanKontaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeUserMoney cum = new ChangeUserMoney(h);
            if (cum.ShowDialog() == DialogResult.OK)
            {
                h.SetMoney(cum.uid, cum.newmoney);
            }

            ReviseInfo();
        }

        private void zmieńRyzykoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModifyRisk mr = new ModifyRisk(h);
            if (mr.ShowDialog() == DialogResult.OK)
            {
               // h.
            }
        }

        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEventForm nef = new NewEventForm();

            if (nef.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                h.MakeEvent(nef.risk, nef.name, nef.cases);
                ReviseInfo();
            }

            
        }

        private void wykresKursuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphForm graph = new GraphForm(0);
            graph.Show();
        }
    }
}
