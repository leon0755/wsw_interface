using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using Palgain.CommonModule;

namespace Palgain.RS232_Component
{
    public class RS232_Communication:IDisposable
    {
        #region field
        /// <summary>
        /// RS232
        /// </summary>
        private SerialPort m_com = new SerialPort();
        #endregion

        #region property
        private string m_portname = "";

        private int m_baudrate = 0;

        private int m_databits = 0;

        private StopBits m_stopbits = StopBits.One;

        private Parity m_parity = Parity.None;

        private Handshake m_handshake = Handshake.None;

        private int m_ReadBufferSize = 2000;

        public string PortName
        {
            get { return m_portname; }
            set { m_portname = value; }
        }


        public int BaudRate
        {
            get { return m_baudrate; }
            set { m_baudrate = value; }
        }


        public int DataBits
        {
            get { return m_databits; }
            set { m_databits = value; }
        }


        public StopBits StopBits
        {
            get { return m_stopbits; }
            set { m_stopbits = value; }
        }

        public Parity Parity
        {
            get { return m_parity; }
            set { m_parity = value; }
        }

        public Handshake HandShake
        {
            get { return m_handshake; }
            set { m_handshake = value; }
        }

        public int ReadBufferSize
        {
            get { return m_ReadBufferSize; }
            set { m_ReadBufferSize = value; }
        }
        private int m_ReadTimeout = 500;

        public int ReadTimeout
        {
            get { return m_ReadTimeout; }
            set { m_ReadTimeout = value; }
        }
        private int m_WriteBufferSize = 2000;

        public int WriteBufferSize
        {
            get { return m_WriteBufferSize; }
            set { m_WriteBufferSize = value; }
        }
        private int m_WriteTimeout = 500;

        public int WriteTimeout
        {
            get { return m_WriteTimeout; }
            set { m_WriteTimeout = value; }
        }

        private bool m_discardnull = false;

        public bool DiscardNull
        {
            get { return m_discardnull; }
            set { m_discardnull = value; }
        }
        private bool m_dtrenable = false;

        public bool DtrEnable
        {
            get { return m_dtrenable; }
            set { m_dtrenable = value; }
        }
        private byte m_parityreplace;

        public byte ParityReplace
        {
            get { return m_parityreplace; }
            set { m_parityreplace = value; }
        }
        private bool m_rtsenable = false;

        public bool RtsEnable
        {
            get { return m_rtsenable; }
            set { m_rtsenable = value; }
        }

        private bool m_is_running = false;
        /// <summary>
        /// COM��״̬
        /// </summary>
        public bool IsRunning
        {
            get 
            {
                m_is_running=false;
                if (m_com != null)
                {
                    m_is_running = m_com.IsOpen;
                }
                return m_is_running;
            }
        }
        #endregion

        #region event
        /// <summary>
        /// �����յ������¼�
        /// </summary>
        public event EventHandler<RS232_EventArgs> Event_DataReceive;
        /// <summary>
        /// ���ڳɹ����������¼�
        /// </summary>
        public event EventHandler<RS232_EventArgs> Event_DataSend;
        /// <summary>
        /// ���ڳ��ֹ����¼� 
        /// </summary>
        public event EventHandler<RS232_EventArgs> Event_ErrorOccurs;
        #endregion

        #region ���캯��
        public RS232_Communication()
        {
            
        }
        #endregion

        #region ���ڳ����շ�����
        void m_com_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            LogHelper.Error("���ڳ����շ�����:" + e.EventType.ToString());
        }
        #endregion

        #region �����յ�����
        void m_com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] t_readbuffer = new byte[m_com.BytesToRead];

                m_com.Read(t_readbuffer, 0, t_readbuffer.Length);

                m_com.DiscardInBuffer();

                OnDataReceived(t_readbuffer);
            }
            catch (Exception ex)
            {
                LogHelper.Error("COM�ڽ�������ʱ����" + ex.Message + ex.StackTrace);
                throw ex;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="i_data">��Ҫ���͵�����</param>
        /// <param name="i_encode">�����ʽ,Ĭ��ΪDefault</param>
        public void SendData(string i_data,Encoding i_encode)
        {
            try
            {
                byte[] t_bytearray = i_encode.GetBytes(i_data);
                m_com.Write(t_bytearray, 0, t_bytearray.Length);
                OnDataSend(t_bytearray);
            }
            catch (Exception ex)
            {
                LogHelper.Error("���ڷ������ݳ����쳣:" + ex.ToString());
                throw ex;
            }
        }
        #endregion
         
        #region ���ڲ�������
        /// <summary>
        /// ���ڿ�ʼ����
        /// </summary>
        public void Start()
        {
            try
            {
                this.Stop();

                this.m_com.PortName = this.PortName;
                this.m_com.BaudRate = this.BaudRate;
                this.m_com.DataBits = this.DataBits;
                this.m_com.StopBits = this.StopBits;
                this.m_com.Parity = this.Parity;
                this.m_com.Handshake = this.HandShake;
                this.m_com.DiscardNull = this.DiscardNull;
                this.m_com.DtrEnable = this.DtrEnable;
                this.m_com.ParityReplace = this.ParityReplace;
                this.m_com.RtsEnable = this.RtsEnable;

                this.m_com.ReadBufferSize = this.ReadBufferSize;
                this.m_com.ReadTimeout = this.ReadTimeout;
                this.m_com.WriteBufferSize = this.WriteBufferSize;
                this.m_com.WriteTimeout = this.WriteTimeout;

                if (this.m_com.IsOpen)
                {
                    this.m_com.Close();
                }

                this.m_com.DataReceived += new SerialDataReceivedEventHandler(m_com_DataReceived);
                this.m_com.ErrorReceived += new SerialErrorReceivedEventHandler(m_com_ErrorReceived);

                m_com.Open();
            }
            catch (Exception ex)
            {
                LogHelper.Error("RSR232����ʧ��" + ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// ����ֹͣ����
        /// </summary>
        public void Stop()
        {
            try
            {
                this.m_com.DataReceived -= new SerialDataReceivedEventHandler(m_com_DataReceived);
                this.m_com.ErrorReceived -= new SerialErrorReceivedEventHandler(m_com_ErrorReceived);
                m_com.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Error("RSR232�ر�ʧ��" + ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void Restart()
        {
            this.Stop();
            this.Start();
        }
        #endregion

        #region �����¼�
        private void OnDataReceived(byte[] i_data)
        {
            if (Event_DataReceive != null)
            {
                RS232_EventArgs t_event = new RS232_EventArgs();
                t_event.ReceiveData = i_data;
                Event_DataReceive(this, t_event);
            }
        }
        #endregion

        #region �����¼�
        private void OnDataSend(byte[] i_data)
        {
            if (Event_DataSend != null)
            {
                RS232_EventArgs t_event = new RS232_EventArgs();
                t_event.ReceiveData = i_data;
                Event_DataSend(this, t_event);
            }
        }
        #endregion

        #region �����¼�
        private void OnErrorOccurs(string i_message)
        {
            if (Event_ErrorOccurs != null)
            {
                RS232_EventArgs t_event = new RS232_EventArgs();
                t_event.ErrorMessage = i_message;
                Event_ErrorOccurs(this, t_event);
            }
        }
        #endregion

        #region IDisposable ��Ա

        public void Dispose()
        {
            try
            {
                if (m_com.IsOpen)
                {
                    m_com.Close();
                }
                this.m_com.DataReceived -= new SerialDataReceivedEventHandler(m_com_DataReceived);
                this.m_com.ErrorReceived -= new SerialErrorReceivedEventHandler(m_com_ErrorReceived);
                m_com = null;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error:" + ex.ToString());
            }
        }

        #endregion
    }
}
