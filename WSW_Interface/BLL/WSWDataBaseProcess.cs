using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using Palgain.CommonModule;

namespace Palgain.BLL
{
    public class WSWDataBaseProcess
    {
        private static object g_lockobj = new object( );

        private enum Enum_Tables
        {
            TB_TODAY_TEST_INFO,
            TB_TODAY_TEST_STATUS,
            TB_BIO_SAMPLE_RESULT,
            TB_BIO_BACT_RESULT,
            TB_BIO_ANTIBIOTICS_RESULT,
        }

        public bool SaveToDB ( WSWSampleResultInfo i_resultinfo )
        {
            lock (g_lockobj)
            {
                bool t_result = false;

                try
                {
                    #region TB_TODAY_TEST_INFO



                    #endregion
                }
                catch (Exception ex)
                {

                }

                return t_result;
            }
        }

        /// <summary>
        /// ≈–∂œ «∑Ò¥Ê‘⁄÷ÿ∏¥º¸
        /// </summary>
        /// <param name="i_sampleresultinfo"></param>
        /// <param name="i_table"></param>
        /// <returns></returns>
        private bool IsAleadyExistsInTable ( WSWSampleResultInfo i_sampleresultinfo, Enum_Tables i_table )
        {
            StringBuilder t_sb = new StringBuilder( );
            switch (i_table)
            {
                case Enum_Tables.TB_TODAY_TEST_INFO:
                    t_sb.AppendFormat( "select count(*) from TB_TODAY_TEST_INFO " );
                    t_sb.AppendFormat( "where  " );
                    break;
            }
        }

    }
}
