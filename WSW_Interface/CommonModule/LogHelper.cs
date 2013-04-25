using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Palgain.CommonModule
{
    public enum LogType
    {
        Normal,
        Raw,
    }

    public class LogHelper
    {
        private static Logger g_log = LogManager.GetLogger("Bench");
        private static Logger g_rawlog = LogManager.GetLogger("RSR232_RAW");

        public static void Info(string i_data)
        {
            g_log.Info(i_data);
        } 

        public static void Info(LogType i_logtype, string i_data)
        {  
            Logger t_log = null;
            switch (i_logtype)
            {
                case LogType.Raw:
                    t_log = g_rawlog;
                    break;
                default:
                    t_log = g_log;
                    break;
            }
            if (i_logtype != LogType.Normal)
            {
                Info( i_data );
            }
            t_log.Info(i_data);
        }

        public static void Debug(string i_data)
        {
            g_log.Debug(i_data);
        }

        public static void Error(string i_data)
        {
            g_log.Error(i_data);
        }

    }

    

}
