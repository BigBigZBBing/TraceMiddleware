using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using System.Text;

namespace TraceMiddleware
{
    [Table("TraceLog")]
    public class TraceLog
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string RunTime { get; set; }
        public string CreateTime { get; set; }
        public string ErrorMessage { get; set; }
    }
}
