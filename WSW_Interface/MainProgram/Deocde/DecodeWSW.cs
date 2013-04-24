using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Palgain.MainProgram
{
    public class DecodeWSW
    {
        private int m_stx = 0x02;
        private int m_etx = 0x03;
        private string m_data = "";

        public void Decode(string i_data)
        {
            string t_data = m_data + i_data;
            int t_stx_index = 0;
            int t_etx_index = t_data.IndexOf((char)m_etx);
            if (t_etx_index >= 0)
            {
                t_stx_index = t_data.IndexOf((char)m_stx);
                t_data = t_data.Substring(t_stx_index, t_etx_index);
                m_data = t_data.Substring(t_etx_index + 1);
            }
            else
            {
                m_data += i_data;
                return;
            }



        }
    }
}
