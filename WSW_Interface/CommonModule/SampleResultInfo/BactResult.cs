using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.CommonModule
{
    public class BactResult:SampleResult
    {
        private string m_isolate_number = "";

        public string IsolateNumber
        {
            get { return m_isolate_number; }
            set { m_isolate_number = value; }
        }

        private string m_bact_biocode = "";

        public string BactBioCode
        {
            get { return m_bact_biocode; }
            set { m_bact_biocode = value; }
        }



    }
}
