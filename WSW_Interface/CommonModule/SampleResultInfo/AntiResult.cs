using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.CommonModule
{
    [Serializable]
    public class AntiResult : SampleResult
    {
        private string m_mic_result = "";

        public string MicResult
        {
            get { return m_mic_result; }
            set { m_mic_result = value; }
        }

    }
}
