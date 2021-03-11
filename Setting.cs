using System;
using System.Collections.Generic;
using System.Text;

namespace TraceMiddleware
{
    public static class Setting
    {
        /// <summary>
        /// 数据库地址
        /// </summary>
        public static string DbPath { get; set; }

        /// <summary>
        /// 数据库连接地址
        /// </summary>
        public static string DbConnectionString
        {
            get
            {
                return $"Data Source={DbPath};Pooling=True;Max Pool Size=100;FailIfMissing=true";
            }
        }
    }
}
