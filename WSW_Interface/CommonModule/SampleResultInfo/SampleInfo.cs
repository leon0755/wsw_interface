using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.CommonModule
{
    public class SampleInfo
    {
        private DateTime m_sampledate = new DateTime(1900, 1, 1);

        public DateTime SampleDate
        {
            get { return m_sampledate; }
            set { m_sampledate = value; }
        }

        private string m_testgroup = "";

        public string TestGroup
        {
            get { return m_testgroup; }
            set { m_testgroup = value; }
        }

        private string m_instrumentno = "";

        public string InstrumentNo
        {
            get { return m_instrumentno; }
            set { m_instrumentno = value; }
        }

        private string m_sampleno = "";

        public string SampleNo
        {
            get { return m_sampleno; }
            set { m_sampleno = value; }
        }

        private string m_testtype = "";

        public string TestType
        {
            get { return m_testtype; }
            set { m_testtype = value; }
        }
        private string m_patient_id = "";

        public string PatientID
        {
            get { return m_patient_id; }
            set { m_patient_id = value; }
        }
    }
}
