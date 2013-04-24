using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.RS232_Component
{
    public class RS232_EventArgs:EventArgs
    {
        private byte[] m_bytes = null;
        /// <summary>
        /// �յ�����
        /// </summary>
        public byte[] ReceiveData
        {
            get { return m_bytes; }
            set { m_bytes = value; }
        }



        private string m_errormessage = "";
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrorMessage
        {
            get { return m_errormessage; }
            set { m_errormessage = value; }
        }
    }
}
