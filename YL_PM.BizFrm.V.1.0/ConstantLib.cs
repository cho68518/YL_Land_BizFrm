﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL_PM.BizFrm
{
    public static class ConstantLib
    {
        public static string BasicConn_Dev  = "Server=14.63.163.73; Port=3306;  Database=domalife; Uid=doma; Pwd=@dhkdldpf9!;CharSet=utf8;SslMode=none;";
        public static string BasicConn_Real = "Server=14.63.165.36; Port=30201; Database=domalife; Uid=doma; Pwd=@dhkdldpf9!;CharSet=utf8;SslMode=none;";
        // public static string BasicConn_Real = "Server=14.63.163.73; Port=3306;  Database=domalife; Uid=doma; Pwd=@dhkdldpf9!;CharSet=utf8;";

        //public static string BasicConn_Real = "server=14.63.165.36;user=doma;database=domalife;port=22030;password=@dhkdldpf9!;";
        public static string TelConn_Real = "Server=180.210.34.31; Port=22025; Database=telecom; Uid=telecom; Pwd=dudbfkdlvm)^!*;CharSet=utf8;SslMode=none;";
        //public static string TelConn_Real = "Server=118.218.136.88; Port=22025; Database=telecom; Uid=telecom; Pwd=dudbfkdlvm)^!*;CharSet=utf8;SslMode=none;";
        public static string Member_Real = "Server=14.63.162.138; Port=22031; Database=DB_MEMBER; Uid=root; Pwd=@bonmin;CharSet=utf8;SslMode=none;";
    }
}
