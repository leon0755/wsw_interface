using System;
using System.Collections.Generic;
using System.Text;

namespace Palgain.CommonModule
{
    [Serializable]
    public class SampleResult
    {
        private string m_itemcode = "";

        public string ItemCode
        {
            get { return m_itemcode; }
            set { m_itemcode = value; }
        }
        private string m_result = "";

        public string Result
        {
            get { return m_result; }
            set { m_result = value; }
        }
        private string m_itemname = "";

        public string ItemName
        {
            get { return m_itemname; }
            set { m_itemname = value; }
        }

        private string m_addresult = "";

        public string AddResult
        {
            get { return m_addresult; }
            set { m_addresult = value; }
        }
    }
}
