using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Palgain.CommonModule;
using Palgain.BLL;

namespace Palgain.MainProgram
{
    public class MainProgram
    {
        private RS232Controller m_rs232 = null;
        private Enum_State m_state = Enum_State.Ready;
        private DecodeWSW m_decode = new DecodeWSW(); 

        public Enum_State State 
        {
            get { return m_state; }
            set { m_state = value; }
        }

        public MainProgram()
        {
            Consts.Config = this.LoadConfig();
            Consts.g_exe_folder = 
                System.IO.Path.GetDirectoryName( 
                System.Reflection.Assembly.GetExecutingAssembly( ).Location );

            SaveProcess.Instance.Start( );
        }

        public void StartListenCOM()
        {
            try
            {
                //创建串口实例
                m_rs232 = new RS232Controller();
                m_rs232.OpenCOM();

                m_rs232.Event_ReceiveData -= new EventHandler<EventArgs_Data>(m_rs232_Event_ReceiveData);
                m_rs232.Event_SendData -= new EventHandler<EventArgs_Data>(m_rs232_Event_SendData);

                m_rs232.Event_ReceiveData += new EventHandler<EventArgs_Data>(m_rs232_Event_ReceiveData);
                m_rs232.Event_SendData += new EventHandler<EventArgs_Data>(m_rs232_Event_SendData);

                m_state = Enum_State.Ready;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
                m_state = Enum_State.Error;
            }
        }

        void m_rs232_Event_SendData(object sender, EventArgs_Data e)
        {

        }

        #region 接收到串口数据
        void m_rs232_Event_ReceiveData(object sender, EventArgs_Data e)
        {
            m_state = Enum_State.Decode;

            try
            {
                WSWSampleResultInfo t_resultinfo = m_decode.Decode( e.Data );
                SaveProcess.Instance.AddResultInfo( t_resultinfo );
            }
            catch (Exception ex)
            {
                LogHelper.Error("Decode Error:" + ex.ToString());
            }
            m_state = Enum_State.Ready;
        }
        #endregion

        ~MainProgram()
        {
            this.ExitProgram();
        }

        public void ExitProgram()
        {
            try
            {
                if (m_rs232 != null)
                {
                    m_rs232.Event_ReceiveData -= new EventHandler<EventArgs_Data>( m_rs232_Event_ReceiveData );
                    m_rs232.Event_SendData -= new EventHandler<EventArgs_Data>( m_rs232_Event_SendData );

                    m_rs232.CloseCOM( );
                    m_rs232.Dispose( );
                    m_rs232 = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error( ex.ToString( ) );
            }
        }

        public void SaveConfig(ConfigInfo i_config)
        {
            Consts.Config = i_config;
            ConfigInfo.Save(Consts.Config);

            #region 重新初始化串口
            if (m_rs232 != null)
            {
                m_rs232.Dispose();
                m_rs232 = null;
            }
            StartListenCOM( );
            #endregion
        }

        public ConfigInfo LoadConfig()
        {
            ConfigInfo t_info = new ConfigInfo();
            t_info = ConfigInfo.Load();
            return t_info;
        }
    }
}
