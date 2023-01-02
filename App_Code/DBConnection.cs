using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBConnection
/// </summary>
namespace LMSIntegration
{
    public static class DBConnection
    {
        //
        // TODO: Add constructor logic here
        //PRD Details
        //public const string connString = "server=192.168.1.225;database=DB01_Order_Engine;uid=sa;password=MTeducare@1234;";
        //public const string connStringLMS = "http://roboapi.robomateplus.in/CMDMService/";

        ////Dev Details
        public const string connString = "server=192.168.1.199;database=DB01_Order_Engine;uid=sa;password=mtel#2016;";//
        public const string connStringLMS = "http://49.248.16.165/CMDMService/";

    }
}