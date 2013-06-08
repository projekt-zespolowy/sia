using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;
using Kozak.SQLDatabaseGate;

namespace Hubx
{
    public class UserInfo
    {
        public int money, uid;
        public string name;
        public DataTable bets; // VIEW kuponag

        public override string ToString()
        {
            return name + ": " + (((double)money) / 100.0).ToString("0.00") + " PLN";
        }
    }
    public class EventInfo
    {
        public int[] stawki;
        public int eid, N;
        public string[] wyns;
        public string name;
        public double[] kursy;
        public int ryzyko;
    }
    public class Hub
    {
        private SQLGate sqlg;
        public Hub()
        {
            sqlg = new SQLiteGate();
            sqlg.Open("Data Source=..\\betterdb.sql;New=True");
            Console.Error.WriteLine("Opening DB...");

            sqlg = new SQLiteGate();
            sqlg.Open("Data Source=..\\betterdb.sql;New=True");

            Console.Error.WriteLine("DB open. Reading script file...");

            StreamReader sr = new StreamReader("..\\init_db.sql");            
            
            string sql = "";

            while (!sr.EndOfStream)
                sql += sr.ReadLine() + "\r\n";

            Console.Error.WriteLine("SQL script rdy. Executing...");

            sqlg.ExecuteNonQuery(sql);

            Console.Error.WriteLine("SQL script done. DB ready for further operations");
        }
        static int Import(object obj)
        {
            return (int)Math.Round(Convert.ToDouble(obj));
        }
        ~Hub()
        {
            sqlg.Close();
        }
        public string MoneyPrint(int i)
        {
            string str = i >= 0 ? " " : "-";
            if (i < 0)
                i = -i;
            str += (i / 100).ToString() + ".";
            i %= 100;
            str += i / 10;
            str += i % 10;

            return str.PadLeft(13);
        }
        public void TestFill()
        {
            ClearDB();

            MakeUser("Kozak");
            SetMoney(1, 100000);

            MakeUser("Wojtek");
            SetMoney(2, 100000);

            MakeEvent(200000, "Finał", new string[] { "Borussia Dortmund", "(REMIS)", "Bayern Munchen" });
            MakeEvent(200000, "Konklawe", new string[] { "Bergoglio", "Scola", "Turkson", "Bertone", "Amato", 
              "Bettori", "Ortega", "Inny kardynał"});
            MakeEvent(30000, "Egzotyczny", new string[] { "A", "B" });
            MakeEvent(300000, "Popularny", new string[] { "A", "B" });

            Trade(1, 1, 1, 10000, true);
        }
        public void Init()
        {
           Console.Error.WriteLine("Opening DB...");

            sqlg = new SQLiteGate();
            sqlg.Open("Data Source=..\\betterdb.sql;New=True");

            Console.Error.WriteLine("DB open. Reading script file...");

            StreamReader sr = new StreamReader("..\\init_db.sql");

           /* string sql = "DROP VIEW kuponag;" +
                            "DROP VIEW rynek;      " +
                            "DROP TABLE uzytkownik;" +
                            "DROP TABLE wydarzenie;" +
                            "DROP TABLE wynik;     " +
                            "DROP TABLE kupon;     ";*/
            string sql = "";
            
            while (!sr.EndOfStream)
                sql += sr.ReadLine() + "\r\n";

            Console.Error.WriteLine("SQL script rdy. Executing...");

            sqlg.ExecuteNonQuery(sql);

            Console.Error.WriteLine("SQL script done. DB ready for further operations");

        }
        public void ClearDB()
        {
            UserInfo ei;
            string sql = "";
            try
            {
                ei = GetUserInfo(0);

                sql = "DROP VIEW kuponag;" +
                            "DROP VIEW rynek;      " +
                            "DROP TABLE uzytkownik;" +
                            "DROP TABLE wydarzenie;" +
                            "DROP TABLE wynik;     " +
                            "DROP TABLE kupon;     ";
            }
            catch { }
            sqlg.ExecuteNonQuery(sql);
            Console.WriteLine("DB clear. Re-preparing...");
            //sqlg.Close();
            Init();
        }
        public void PrintUserInfo(int uid)
        {
            UserInfo ui = GetUserInfo(uid);
            Console.WriteLine("Info on user '{0}', ID: {1}", ui.name, uid);
            Console.WriteLine("Account: {0}, active bets list:", MoneyPrint(ui.money));

            foreach (DataRow dr in ui.bets.Rows)
            {
                Console.WriteLine(" {0} on '{1}', event '{2}' (e{3}::c{4})", MoneyPrint(Import(dr["stawka"])),
                    (string)dr["nazwawyn"], (string)dr["nazwawyd"], Import(dr["kodwyd"]),
                    Import(dr["kodwyn"]));
            }
        }
        public int PersonalStake(int uid, int eid, int cid)
        {
            string sql = "SELECT stawka FROM kuponag WHERE koduz=" + uid + " AND " +
                "kodwyd=" + eid + " AND kodwyn=" + cid + ";";
            DataTable dt = sqlg.ExecuteQuery(sql);

            try
            {
                return Import(dt.Rows[0][0]);
            }
            catch
            {
                return 0;
            }
            
        }
        public int[] EventNums()
        {
            string sql = "SELECT kodwyd FROM wydarzenie;";
            DataTable dt = sqlg.ExecuteQuery(sql);

            int[] nums = new int[dt.Rows.Count];

            for(int i = 0; i < nums.Length; i++)
                nums[i] = Import(dt.Rows[i]["kodwyd"]);

            return nums;
            
        }
        public int[] UserNums()
        {
            string sql = "SELECT koduz FROM uzytkownik;";
            DataTable dt = sqlg.ExecuteQuery(sql);

            int[] nums = new int[dt.Rows.Count];

            for (int i = 0; i < nums.Length; i++)
                nums[i] = Import(dt.Rows[i]["koduz"]);

            return nums;
            
        }
        public int MakeEvent(int ryzyko, string nazwa, string[] wyns)
        {
            string sql = "SELECT max(kodwyd) as abc FROM wydarzenie;";
            DataTable dt = sqlg.ExecuteQuery(sql);

            int nowy = 1 + Import(dt.Rows[0]["abc"]);

            sql = "INSERT INTO wydarzenie VALUES (" + nowy + ", '" + nazwa + "', " + ryzyko + ");";
            sqlg.ExecuteNonQuery(sql);

            int N = wyns.Length;

            for (int i = 0; i < N; i++)
            {
                // pusty kupon SYSTEMOWY
                sql = "INSERT INTO kupon VALUES (0, " + nowy + ", " + i + ", 0);";
                sqlg.ExecuteNonQuery(sql);

                // wynik
                sql = "INSERT INTO wynik VALUES(" + nowy + ", " + i + ", '" + wyns[i] +
                    "');";
                sqlg.ExecuteNonQuery(sql);
            }

            return nowy;
            
        }
        public void PrintEventInfo(int eid)
        {
            EventInfo ei = GetEventInfo(eid);
            Console.WriteLine("Info on event '{0}', ID: {1}", ei.name, eid);
            Console.WriteLine("Risk max:  {0}, Case #: {1}", MoneyPrint(ei.ryzyko), ei.N);

            for (int i = 0; i < ei.N; i++)
            {
                Console.WriteLine("   Case {0}: {1}, name='{2}', dx={3}", i, MoneyPrint(ei.stawki[i]),
                    ei.wyns[i], ei.kursy[i]);
            }
        }
        public int MakeUser(string nazwa)
        {
            string sql = "SELECT max(koduz) as abc FROM uzytkownik;";
            DataTable dt = sqlg.ExecuteQuery(sql);

            int nowy = 1 + Import(dt.Rows[0]["abc"]);

            // TODO: a co, jesli taki juz jest?

            sql = "INSERT INTO uzytkownik VALUES (" + nowy + ", 0, '" + nazwa + "');";
            sqlg.ExecuteNonQuery(sql);

            return nowy;
        }
        public UserInfo GetUserInfo(int uid)
        {
            UserInfo ui = new UserInfo();
            ui.uid = uid;

            string sql = "SELECT * FROM uzytkownik WHERE koduz=" + uid + ";";
            DataTable lols = sqlg.ExecuteQuery(sql);
            ui.money = Import(lols.Rows[0]["konto"]);
            ui.name = (string)lols.Rows[0]["imie"];

            sql = "SELECT * FROM kuponag WHERE koduz=" + uid + ";";
            ui.bets = sqlg.ExecuteQuery(sql);

            return ui;
        }
        public EventInfo GetEventInfo(int eid)
        {
            string sql = "SELECT * FROM wydarzenie WHERE kodwyd=" + eid + ";";
            DataTable dt = sqlg.ExecuteQuery(sql);

            EventInfo ei = new EventInfo();
            ei.name = (string)dt.Rows[0]["nazwawyd"];
            ei.eid = eid;
            ei.ryzyko = Import(dt.Rows[0]["ryzyko"]);

            sql = "SELECT * FROM rynek WHERE kodwyd=" + eid + ";";
            dt = sqlg.ExecuteQuery(sql);
            ei.N = dt.Rows.Count;

            ei.wyns = new string[ei.N];
            ei.stawki = new int[ei.N];

            for (int i = 0; i < ei.N; i++)
            {
                ei.wyns[i] = (string)dt.Rows[i]["nazwawyn"];
                ei.stawki[i] = Import(dt.Rows[i]["stawka"]);
            }
            ei.kursy = Courses(eid);

            return ei;
        }
        public void SetMoney(int uid, int money)
        {
            Console.WriteLine("Account modified: now {0}'th user has {1}", uid, MoneyPrint(money));
            string sql = "UPDATE uzytkownik SET konto=" + money + " WHERE koduz=" + uid + ";";
            sqlg.ExecuteNonQuery(sql);
        }
        public void SolveEvent(int wyd, int wyn)
        {
            
            EventInfo ei = GetEventInfo(wyd);

            Console.WriteLine("Solving event '{0}' - the winner is '{1}'", ei.name, ei.wyns[wyn]);
            Console.WriteLine("Pay to winners: {0}", MoneyPrint(ei.stawki[wyn]));

            string sql = "UPDATE uzytkownik SET konto=konto-(" + ei.stawki[wyn] + ") WHERE koduz=0;";
            sqlg.ExecuteNonQuery(sql);

            sql = "SELECT * FROM kuponag WHERE kodwyd=" + wyd
                    + " AND kodwyn=" + wyn + ";";

            DataTable dt = sqlg.ExecuteQuery(sql);

            foreach (DataRow dr in dt.Rows)
            {
                sql = "UPDATE uzytkownik SET konto=konto+(" + Import(dr["stawka"]) +
                    ") WHERE koduz=" + Import(dr["koduz"]) + ";";                    

                sqlg.ExecuteNonQuery(sql);
            }
            sql = "DELETE FROM kupon WHERE kodwyd=" + wyd
                    + " AND kodwyn=" + wyn + ";";
            sql += "DELETE FROM wynik WHERE kodwyd=" + wyd + ";";
            sql += "DELETE FROM wydarzenie WHERE kodwyd=" + wyd + ";";

            sqlg.ExecuteNonQuery(sql);
        }
        public int Evaluate(int[] wyd2, int Ri)
        {
            int N = wyd2.Length;
            double[] wyd = new double[N];
            for (int i = 0; i < N; i++)
                wyd[i] = (double)wyd2[i];
            double R = Ri;
            double M = wyd.Max();

            double[] T = new double[N];
            for (int i = 0; i < N; i++)
                T[i] = M - wyd[i];
            double RR = R * N / (N - 1);
            double K = 0.0;
            for (int i = 0; i < N; i++)
                K += RR * (1.0 - Math.Exp(-T[i] / RR));
            K /= N;

            return (int)Math.Floor(M - K);
        }
        public double EvaluateReal(double[] wyd, int Ri)
        {
            double M = (double)wyd.Max();
            double R = (double)Ri;
            int N = wyd.Length;
            double[] T = new double[N];
            for (int i = 0; i < N; i++)
                T[i] = M - (double)wyd[i];
            double RR = R * N / (N - 1);
            double K = 0.0;
            for (int i = 0; i < N; i++)
                K += RR * (1.0 - Math.Exp(-T[i] / RR));
            K /= (double)N;

            return M - K;
        }
        public double[] Courses(int wyd)
        {
            int N = sqlg.ExecuteQuery("SELECT * FROM wynik WHERE kodwyd=" + wyd + ";").Rows.Count;

            double dx = 0.01F;
            double[] outp = new double[N];

            for (int i = 0; i < N; i++)
            {
                double z1 = TradeRealSim(0, wyd, i, dx);
                double z2 = TradeRealSim(0, wyd, i, -dx);
                outp[i] = 2.0 * dx / (z1 - z2);
            }

            return outp;
        }
        public double TradeRealSim(int uid, int wyd, int wyn, double st)
        {
            DataTable dt1 = sqlg.ExecuteQuery("SELECT * FROM rynek WHERE kodwyd=" + wyd + ";");
            int N = dt1.Rows.Count;
            double[] lols = new double[N];
            for (int i = 0; i < N; i++)
            {
                if (dt1.Rows[i].IsNull("stawka"))
                    lols[i] = 0.0;
                else lols[i] = (double)Import(dt1.Rows[i]["stawka"]);
            }
            dt1 = sqlg.ExecuteQuery("SELECT ryzyko FROM wydarzenie WHERE kodwyd=" + wyd + ";");
            int R = Import(dt1.Rows[0]["ryzyko"]);

            double c1 = EvaluateReal(lols, R);

            lols[wyn] += st;

            double c2 = EvaluateReal(lols, R);

            return (c2 - c1);
        }
        public int Trade(int uid, int wyd, int wyn, int st, bool actual)
        {
            DataTable dt1 = sqlg.ExecuteQuery("SELECT * FROM rynek WHERE kodwyd=" + wyd + ";");
            int N = dt1.Rows.Count;
            int[] lols = new int[N];
            for (int i = 0; i < N; i++)
            {
                if (dt1.Rows[i].IsNull("stawka"))
                    lols[i] = 0;
                else lols[i] = Import(dt1.Rows[i]["stawka"]);
            }
            dt1 = sqlg.ExecuteQuery("SELECT ryzyko FROM wydarzenie WHERE kodwyd=" + wyd + ";");
            int R = Import(dt1.Rows[0]["ryzyko"]);

            int c1 = Evaluate(lols, R);

            lols[wyn] += st;

            int c2 = Evaluate(lols, R);

            if (actual)
            {
                string sql;
                sql = "UPDATE uzytkownik SET konto=konto-(" +
                    (c2 - c1) + ") WHERE koduz=" + uid + ";";
                sqlg.ExecuteNonQuery(sql);

                sql = "INSERT INTO kupon VALUES (" + uid + ", "
                    + wyd + ", " + wyn + ", " + st + ");";
                sqlg.ExecuteNonQuery(sql);

                sql = "UPDATE uzytkownik SET konto=konto+(" + (c2 - c1) + ") WHERE koduz=0;";
                sqlg.ExecuteNonQuery(sql);
            }

            return (c2 - c1);
        }
    }
}
