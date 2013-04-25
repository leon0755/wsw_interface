using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO.Ports;
using System.IO;
using System.Runtime.Serialization;

namespace Palgain.CommonModule
{
    [Serializable]
    public class ConfigInfo
    {
        #region RS232配置
        private RS232ConfigInfo m_rs232configinfo = new RS232ConfigInfo();
        [XmlElement("RS232")]
        public RS232ConfigInfo Rs232ConfigInfo
        {
            get { return m_rs232configinfo; }
            set { m_rs232configinfo = value; }
        }

        #region RS232配置类
        public class RS232ConfigInfo
        {
            private string m_portname = "";

            public string PortName
            {
                get { return m_portname; }
                set { m_portname = value; }
            }

            private int m_baudrate = 0;

            public int BaudRate
            {
                get { return m_baudrate; }
                set { m_baudrate = value; }
            }

            private int m_databits = 0;

            public int Databits
            {
                get { return m_databits; }
                set { m_databits = value; }
            }

            private StopBits m_stopbits = StopBits.One;

            public StopBits Stopbits
            {
                get { return m_stopbits; }
                set { m_stopbits = value; }
            }

            private Parity m_parity = Parity.None;

            public Parity Parity
            {
                get { return m_parity; }
                set { m_parity = value; }
            }

            private Handshake m_handshake = Handshake.None;

            public Handshake Handshake
            {
                get { return m_handshake; }
                set { m_handshake = value; }
            }

            private int m_ReadBufferSize = 2000;

            public int ReadBufferSize
            {
                get { return m_ReadBufferSize; }
                set { m_ReadBufferSize = value; }
            }
        }
        #endregion

        #endregion

        #region .net环境配置
        private string m_testgroup = "";

        public string TestGroup
        {
            get { return m_testgroup; }
            set { m_testgroup = value; }
        }

        private string m_instrumentno = "";

        public string InstrumentNo
        {
            get { return m_instrumentno; }
            set { m_instrumentno = value; }
        }

        private string m_operator = "";

        public string Operator
        {
            get { return m_operator; }
            set { m_operator = value; }
        }
        #endregion

        #region 数据库连接字符串
        private string m_connectstring = "";

        public string ConnectString
        {
            get { return m_connectstring; }
            set { m_connectstring = value; }
        }
        #endregion

        #region 序列化与反序列化
        public static void Save(ConfigInfo i_configinfo)
        {
            string t_filepath = "";

            string t_path = System.IO.Directory.GetCurrentDirectory( );
            System.IO.Directory.SetCurrentDirectory( Consts.g_exe_folder );
            t_filepath = Path.Combine( Consts.g_exe_folder, Consts.g_config_file );

            using (FileStream t_filestream = new FileStream( t_filepath, FileMode.Create, FileAccess.Write, FileShare.None ))
            {
                XmlSerializer ser = new XmlSerializer(typeof(ConfigInfo));
                ser.Serialize(t_filestream, i_configinfo);
                ser = null;
            }
            System.IO.Directory.SetCurrentDirectory( t_path );
        }

        public static ConfigInfo Load()
        {
            ConfigInfo t_temp = new ConfigInfo();
            if (File.Exists(Consts.g_config_file))
            {
                using (FileStream t_filestream = new FileStream(Consts.g_config_file, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ConfigInfo));
                    t_temp = ser.Deserialize(t_filestream) as ConfigInfo;
                    ser = null;
                }
            }
            else
            {
                ConfigInfo.Save(t_temp);
            }
            return t_temp;
        }
        #endregion
    }
}
