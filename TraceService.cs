using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;

namespace TraceMiddleware
{
    public static class TraceService
    {
        public static bool InsertTraceLog(TraceLog log)
        {
            using (var con = new SQLiteConnection(Setting.DbConnectionString))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                return con.Insert(log) > 0;
            }
        }

        public static IEnumerable<TraceLog> SearchLog()
        {
            using (var con = new SQLiteConnection(Setting.DbConnectionString))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                return con.GetList<TraceLog>("limit 200");
            }
        }
    }
}
