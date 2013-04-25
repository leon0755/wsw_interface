using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.CommonModule
{
    [Serializable]
    public class WSWSampleResultInfo
    {
        private SampleInfo m_sampleinfo = new SampleInfo();

        public SampleInfo SampleInfo
        {
            get { return m_sampleinfo; }
            set { m_sampleinfo = value; }
        }

        private BactResult m_bactresult = new BactResult();

        public BactResult BactResult
        {
            get { return m_bactresult; }
            set { m_bactresult = value; }
        }
        private List<AntiResult> m_antiresults = new List<AntiResult>();

        public List<AntiResult> AntiResults
        {
            get { return m_antiresults; }
            set { m_antiresults = value; }
        }
    }
}
