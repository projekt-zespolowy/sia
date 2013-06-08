using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Kozak.SQLDatabaseGate
{
    public abstract class SQLGate
    {
        public abstract int ExecuteNonQuery(string sql);
        public abstract DataTable ExecuteQuery(string sql);
        public abstract void Open(string connString);
        public abstract void Close();
    }
    public class SQLiteGate : SQLGate
    {
        SQLiteConnection sqlc;

        public override void Open(string connString)
        {
            sqlc = new SQLiteConnection(connString);
            sqlc.Open();
        }
        public override int ExecuteNonQuery(string sql)
        {
            SQLiteCommand mycommand = new SQLiteCommand(sqlc);
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();

            //Console.Error.Write("\n\n\tSQL:  {0}\n", sql);

            return rowsUpdated;
        }
        public override DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            SQLiteCommand mycommand = new SQLiteCommand(sqlc);
            mycommand.CommandText = sql;

            //Console.Error.Write("\n\n\tSQL:  {0}\n", sql);


            SQLiteDataReader reader = mycommand.ExecuteReader();
            dt.Load(reader);
            reader.Close();

            return dt;
        }
        public override void Close()
        {
            sqlc.Close();
        }
    }
}
