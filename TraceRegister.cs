using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data.SQLite;
using Dapper;
using System.Data;

namespace TraceMiddleware
{
    public static class Register
    {
        public static void TraceRegister(this IServiceCollection services)
        {
            services.AddSingleton<TraceMiddleware>();
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var dbFile = Path.Combine(path, "Trace.db");
            Setting.DbPath = dbFile;
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
                CreateTraceLog(dbFile);
            }
        }

        static void CreateTraceLog(string dbPath)
        {
            using (SQLiteConnection con = new SQLiteConnection(Setting.DbConnectionString))
            {
                if (con.State != ConnectionState.Open) con.Open();
                var cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = @"
CREATE TABLE TraceLog (
    Id integer primary key not null,
    Url text,
    RunTime text,
    CreateTime text,
    ErrorMessage text
);";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
