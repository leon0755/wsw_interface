using System;
using System.Collections.Generic;
using System.Text;
using Palgain.CommonModule;
using Palgain.RS232_Component;

namespace Palgain.MainProgram
{
    public class RS232Controller:IDisposable
    {
        public event EventHandler<EventArgs_Data> Event_ReceiveData;
        public event EventHandler<EventArgs_Data> Event_SendData;

        private RS232_Communication m_com = new RS232_Communication();

        public RS232Controller()
        {
            m_com.PortName = Consts.Config.Rs232ConfigInfo.PortName;
            m_com.BaudRate = Consts.Config.Rs232ConfigInfo.BaudRate;
            m_com.DataBits = Consts.Config.Rs232ConfigInfo.Databits;
            m_com.StopBits = Consts.Config.Rs232ConfigInfo.Stopbits;
            m_com.HandShake = Consts.Config.Rs232ConfigInfo.Handshake;
            m_com.Parity = Consts.Config.Rs232ConfigInfo.Parity;

            m_com.Event_DataReceive += new EventHandler<RS232_EventArgs>(m_com_Event_DataReceive);
            m_com.Event_DataSend += new EventHandler<RS232_EventArgs>(m_com_Event_DataSend);
            m_com.Event_ErrorOccurs += new EventHandler<RS232_EventArgs>(m_com_Event_ErrorOccurs);
            this.OpenCOM();
        }

        void m_com_Event_ErrorOccurs(object sender, RS232_EventArgs e)
        {
            LogHelper.Error(e.ErrorMessage);
        }

        void m_com_Event_DataSend(object sender, RS232_EventArgs e)
        {
            if (Event_SendData != null)
            {
                Event_SendData(this, new EventArgs_Data(Encoding.Default.GetString(e.ReceiveData)));
            }
        }

        void m_com_Event_DataReceive(object sender, RS232_EventArgs e)
        {
            string t_data = "";
            try
            {
                t_data = Encoding.Default.GetString(e.ReceiveData);
                LogHelper.Info("RS232收到数据:" + t_data);
                LogHelper.Info(LogType.Raw, t_data);

                if (Event_ReceiveData != null)
                {
                    Event_ReceiveData(this, new EventArgs_Data(t_data));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("接收数据时出错:" + ex.ToString());
            }
        }

        #region 串口控制
        public void OpenCOM()
        {
            LogHelper.Info( "准备打开串口:" + m_com.PortName );
            if (m_com.IsRunning == false)
            {
                m_com.Start( );
            }
            LogHelper.Info( m_com.PortName + "串口已经开始监听" );
        }

        public void CloseCOM()
        {
            LogHelper.Info( "准备关闭串口:" + m_com.PortName );
            if (m_com.IsRunning)
            {
                m_com.Stop();
            }
            LogHelper.Info( m_com.PortName + "串口关闭监听" );
        }

        public void RestartCOM()
        {
            m_com.Restart();
            LogHelper.Info( m_com.PortName + "串口重启完成" );
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            m_com.Event_DataReceive -= new EventHandler<RS232_EventArgs>(m_com_Event_DataReceive);
            m_com.Event_DataSend -= new EventHandler<RS232_EventArgs>(m_com_Event_DataSend);
            m_com.Event_ErrorOccurs -= new EventHandler<RS232_EventArgs>(m_com_Event_ErrorOccurs);

            m_com.Dispose();
            m_com = null;
        }

        #endregion
    }
}
