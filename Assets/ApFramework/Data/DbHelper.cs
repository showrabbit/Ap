using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Ap.Data
{
    public class DbHelper
    {

        public static DataTable Quary(string dbCon, string sql)
        {
            SqliteConnection con = Connect(dbCon);
            try
            {
                SqliteCommand cmd = new SqliteCommand(con);

                cmd.CommandText = sql;
                con.Open();
                DataSet ds = new DataSet();
                using (SqliteDataAdapter sda = new SqliteDataAdapter(cmd))
                {
                    sda.Fill(ds);
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        public static void ExecuteNonQuery(string dbCon, string sql)
        {
            SqliteConnection con = Connect(dbCon);
            try
            {
                SqliteCommand cmd = new SqliteCommand(con);

                cmd.CommandText = sql;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private static SqliteConnection Connect(string dbCon)
        {
            SqliteConnection con = new SqliteConnection(dbCon);
            return con;
        }
    }

}
