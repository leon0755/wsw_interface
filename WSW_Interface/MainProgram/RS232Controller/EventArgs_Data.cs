using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.MainProgram
{
    public class EventArgs_Data:EventArgs
    {
        private string m_data = "";

        public string Data
        {
            get { return m_data; }
            set { m_data = value; }
        }

        public EventArgs_Data(string i_data)
        {
            this.m_data = i_data;
        }
    }
}
