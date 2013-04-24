using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.CommonModule
{
    public class AntiResult : SampleResult
    {
        private string m_mic_result = "";

        public string MicResult
        {
            get { return m_mic_result; }
            set { m_mic_result = value; }
        }

        private string m_addresult = "";

        public string AddResult
        {
            get { return m_addresult; }
            set { m_addresult = value; }
        }

    }
}
