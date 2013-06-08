using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hubx;
using System.Threading;
using System.Windows.Forms;

namespace WindowsTesting
{
    class CourseHoldAgent
    {
        public Hub h;
        public int uid, eid, cid;
        public Thread t;
        public double money;
        public double cref;
        public double reaction;
        public int tmin, tmax;
        private bool stop;

        public CourseHoldAgent()
        {
            stop = false;
        }

        private void SingleCycle()
        {
            // poznaj kurs
            double kurs = h.Courses(eid)[cid];
            int dalem = h.PersonalStake(uid, eid, cid);

            double x = (kurs - cref) * reaction;
            double nowe_dalem = money * (1.0 - Math.Exp(-x));
            if (nowe_dalem < 0.0)
                nowe_dalem = 0.0;

            int dokup = ((int)nowe_dalem) - dalem;

            h.Trade(uid, eid, cid, dokup, true);
        }
        private void Runner()
        {
            while (true)
            {
                try
                {
                    SingleCycle();
                }
                catch
                {
                    MessageBox.Show("Przerwano agenta CourseHoldAgent, (uid, eid, cid) = (" + uid +
                        ", " + eid + ", " + cid + ") ze wzgledu na wymuszenie", "CourseHoldAgent", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (stop)
                {
                    MessageBox.Show("Przerwano agenta CourseHoldAgent, (uid, eid, cid) = (" + uid +
                       ", " + eid + ", " + cid + ") ze wzgledu na prośbę o przerwanie", "CourseHoldAgent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   

                    return;
                }

                int t = (new Random()).Next(tmax - tmin);
                Thread.Sleep(tmin + t);
            }
        }
        public void Run()
        {
            t = new Thread(new ThreadStart(Runner));
            stop = false;
            t.Start();
        }
        public void Suspend()
        {
            stop = true;
        }
    }
}
