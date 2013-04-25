using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.CommonModule
{
    public class Consts
    {
        public static string g_config_file = @"Config.xml";
        public static string g_exe_folder = "";

        private static ConfigInfo m_config_info = new ConfigInfo();

        public static ConfigInfo Config
        {
            get { return m_config_info; }
            set { m_config_info = value; }
        }
    }
}
