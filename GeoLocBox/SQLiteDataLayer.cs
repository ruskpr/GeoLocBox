using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocBox
{
    public class SQLiteDataLayer
    {
        SqliteConnection conn;
        SqliteCommand cmd;
        string connectionString;

        public SQLiteDataLayer(string connectionstring)
        {
            this.connectionString = connectionstring;
        }

        public void InsertRecord(double lat, double lng, double alt, string date)
        {

            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                cmd = new SqliteCommand($"insert into Location " +
                    $"values ('{lat}', '{lng}', '{alt}', '{date}')", conn);

                if (lat != 0 && lng != 0 && alt != 0)
                    cmd.ExecuteNonQuery();
                //else
                //            MessageBox.Show("values are 0");
            }
        }

        public void DeleteRecord(int id)
        {
            //using (SqliteConnection conn = new SqliteConnection(connectionString))
            //{
            //    conn.Open();
            //    cmd = new SqliteCommand($"delete from Table1 where id = '{id}'", conn);
            //    cmd.ExecuteNonQuery();
            //}

        }
        //public DataTable GetRecords()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("test_data");
        //    dt.Columns.Add("test_date");
        //    using (SqliteConnection conn = new SqliteConnection(connectionString))
        //    {
        //        conn.Open();
        //        cmd = new SqliteCommand("select * from Table1", conn);

        //        SqliteDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            dt.Rows.Add(dr[0], dr[1], dr[2]);
        //        }
        //    }

        //    return dt;
        //}
        //public void UpdateRecord(int id, string update, DateTime record_date)
        //{
        //    using (SqliteConnection conn = new SqliteConnection(connectionString))
        //    {
        //        cmd = new SqliteCommand($"update Table1 set test_data = {update} where " +
        //            $"ID = '{id}'", conn);
        //        conn.Open();
        //        // execute for no results, create, update, delete
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }

}
