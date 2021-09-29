﻿using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using System;
using System.IO;

namespace e_Locadora5.Infra.GeradorLogs
{
    public static class GeradorDeLog
    {

        public static void ConfigurarLog()
        {
            Logger logger = new LoggerConfiguration()
               .Enrich.WithExceptionDetails()
               //.WriteTo.Seq("http://20.206.137.196:5341")
               .WriteTo.Seq("http://localhost:5341")
               .WriteTo.File(Directory.GetCurrentDirectory(), rollingInterval: RollingInterval.Day)                            
               .CreateLogger();
            Serilog.Log.Logger = logger;
        }

    }
}
