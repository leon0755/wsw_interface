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
                    LogHelper.Info(string.Format("正在入库:TestGroup:{2},TestDate:{0};SampleNO:{1}",
                        i_resultinfo.SampleInfo.SampleDate,
                        i_resultinfo.SampleInfo.SampleNo,
                        Consts.Config.TestGroup
                        ));

                    #region TB_TODAY_TEST_INFO

                    if (this.IsAleadyExistsInTable(i_resultinfo, Enum_Tables.TB_TODAY_TEST_INFO) == false)
                    {
                        this.InsertToDB(i_resultinfo, Enum_Tables.TB_TODAY_TEST_INFO);
                    }

                    #endregion

                    #region TB_TODAY_TEST_STATUS

                    if (this.IsAleadyExistsInTable(i_resultinfo, Enum_Tables.TB_TODAY_TEST_STATUS) == false)
                    {
                        this.InsertToDB(i_resultinfo, Enum_Tables.TB_TODAY_TEST_STATUS);
                    }

                    #endregion

                    #region TB_BIO_BACT_RESULT

                    if (this.IsAleadyExistsInTable(i_resultinfo, Enum_Tables.TB_BIO_BACT_RESULT) == false)
                    {
                        this.InsertToDB(i_resultinfo, Enum_Tables.TB_BIO_BACT_RESULT);
                    }
                    else
                    {
                        this.UpdateToDB(i_resultinfo, Enum_Tables.TB_BIO_BACT_RESULT);
                    }

                    #endregion

                    LogHelper.Info("入库成功!");
                }
                catch (Exception ex)
                {
                    LogHelper.Error("Save To DB," + ex.ToString());
                }

                return t_result;
            }
        }

        #region 判断是否存在重复键
        /// <summary>
        /// 判断是否存在重复键
        /// </summary>
        /// <param name="i_sampleresultinfo"></param>
        /// <param name="i_table"></param>
        /// <returns></returns>
        private bool IsAleadyExistsInTable ( WSWSampleResultInfo i_sampleresultinfo, Enum_Tables i_table )
        {
            bool t_is_exists = false;
            StringBuilder t_sb = new StringBuilder( );
            switch (i_table)
            {
                #region TB_TODAY_TEST_INFO
                case Enum_Tables.TB_TODAY_TEST_INFO:
                    t_sb.AppendFormat( "select count(*) from TB_TODAY_TEST_INFO " );
                    t_sb.AppendFormat(" where SAMPLE_ID='{0}'", i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat(" and TEST_DATE='{0}'", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat(" and TEST_GROUP='{0}'", Consts.Config.TestGroup);
                    break;
                #endregion

                #region TB_TODAY_TEST_STATUS
                case Enum_Tables.TB_TODAY_TEST_STATUS:
                    t_sb.AppendFormat("select count(*) from TB_TODAY_TEST_STATUS ");
                    t_sb.AppendFormat(" where SAMPLE_ID='{0}'", i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat(" and TEST_DATE='{0}'", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat(" and TEST_GROUP='{0}'", Consts.Config.TestGroup);
                    break;
                #endregion

                #region TB_BIO_BACT_RESULT
                case Enum_Tables.TB_BIO_BACT_RESULT:
                    t_sb.AppendFormat("select count(*) from TB_BIO_BACT_RESULT ");
                    t_sb.AppendFormat(" where SAMPLE_ID='{0}'", i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat(" and TEST_DATE='{0}'", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat(" and TEST_GROUP='{0}'", Consts.Config.TestGroup);
                    t_sb.AppendFormat(" and BACT_CODE='{0}'", i_sampleresultinfo.BactResult.ItemCode);
                    break;
                #endregion
            }

            DataTable t_dt =
                SqlHelper.ExecuteDataset(Consts.Config.ConnectString, CommandType.Text, t_sb.ToString()).Tables[0];

            if (Convert.ToInt32(t_dt.Rows[0][0]) > 0)
            {
                t_is_exists = true;
            }
            else
            {
                t_is_exists = false;
            }
            return t_is_exists;
        }
        #endregion

        #region 将数据新增至数据库
        /// <summary>
        /// 将数据新增至数据库
        /// </summary>
        /// <param name="i_sampleresultinfo"></param>
        /// <param name="i_table"></param>
        private void InsertToDB(WSWSampleResultInfo i_sampleresultinfo,Enum_Tables i_table)
        {
            StringBuilder t_sb = new StringBuilder();
            switch (i_table)
            {
                #region TB_TODAY_TEST_INFO
                case Enum_Tables.TB_TODAY_TEST_INFO:
                    t_sb.AppendFormat("insert into TB_TODAY_TEST_INFO ");
                    t_sb.AppendFormat("(SAMPLE_CODE,SAMPLE_ID,TEST_DATE,TEST_GROUP,VALID_FLG,PATIENT_ID) ");
                    t_sb.AppendFormat("values ('{0}','{1}',",1,i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat("'{0}',", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat("'{0}',", Consts.Config.TestGroup);
                    t_sb.AppendFormat("'{0}',", 0);
                    t_sb.AppendFormat("'{0}')", i_sampleresultinfo.SampleInfo.PatientID);
                    break;
                #endregion

                #region TB_TODAY_TEST_STATUS
                case Enum_Tables.TB_TODAY_TEST_STATUS:
                    t_sb.AppendFormat("insert into TB_TODAY_TEST_STATUS ");
                    t_sb.AppendFormat("(SAMPLE_CODE,SAMPLE_ID,TEST_DATE,TEST_GROUP,VALID_FLG,TEST_FLG,TEST_TIME) ");
                    t_sb.AppendFormat("values ('{0}','{1}',", 1, i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat("'{0}',", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat("'{0}',", Consts.Config.TestGroup);
                    t_sb.AppendFormat("'{0}',", 0);
                    t_sb.AppendFormat("'{0}',", 1);
                    t_sb.AppendFormat("'{0}')", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"));
                    break;
                #endregion

                #region TB_BIO_BACT_RESULT
                case Enum_Tables.TB_BIO_BACT_RESULT:
                    t_sb.AppendFormat("insert into TB_BIO_BACT_RESULT ");
                    t_sb.AppendFormat("(SAMPLE_CODE,SAMPLE_ID,TEST_DATE,TEST_GROUP,BACT_CODE,BACT_NAME,CREATOR,");
                    t_sb.AppendFormat("TEST_TIME,TEST_METHOD,INPUT_PERSON,INPUT_TIME) ");
                    t_sb.AppendFormat("values ('{0}','{1}',", 1, i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat("'{0}',", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat("'{0}',", Consts.Config.TestGroup);
                    t_sb.AppendFormat("'{0}',", i_sampleresultinfo.BactResult.ItemCode);
                    t_sb.AppendFormat("'{0}',", i_sampleresultinfo.BactResult.ItemName.Substring(0, 20));
                    t_sb.AppendFormat("'{0}',", Consts.Config.InstrumentNo);
                    t_sb.AppendFormat("'{0}',", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    t_sb.AppendFormat("'{0}',", 1);
                    t_sb.AppendFormat("'{0}',", Consts.Config.Operator);
                    t_sb.AppendFormat("'{0}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    break;
                #endregion
            }
            SqlHelper.ExecuteNonQuery(Consts.Config.ConnectString, CommandType.Text, t_sb.ToString());
        }
        #endregion

        #region 将数据修改至数据库
        /// <summary>
        /// 将数据修改至数据库
        /// </summary>
        /// <param name="i_sampleresultinfo"></param>
        /// <param name="i_table"></param>
        private void UpdateToDB(WSWSampleResultInfo i_sampleresultinfo, Enum_Tables i_table)
        {
            StringBuilder t_sb = new StringBuilder();
            switch (i_table)
            {
                #region TB_TODAY_TEST_INFO
                case Enum_Tables.TB_TODAY_TEST_INFO:
                    t_sb.AppendFormat("update TB_TODAY_TEST_INFO ");
                    t_sb.AppendFormat("(SAMPLE_CODE,SAMPLE_ID,TEST_DATE,TEST_GROUP,VALID_FLG,PATIENT_ID) ");
                    t_sb.AppendFormat("values ('{0}','{1}',", 1, i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat("'{0}',", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat("'{0}',", Consts.Config.TestGroup);
                    t_sb.AppendFormat("'{0}',", 0);
                    t_sb.AppendFormat("'{0}')", i_sampleresultinfo.SampleInfo.PatientID);
                    break;
                #endregion

                #region TB_TODAY_TEST_STATUS
                case Enum_Tables.TB_TODAY_TEST_STATUS:
                    t_sb.AppendFormat("insert into TB_TODAY_TEST_STATUS ");
                    t_sb.AppendFormat("(SAMPLE_CODE,SAMPLE_ID,TEST_DATE,TEST_GROUP,VALID_FLG,TEST_FLG,TEST_TIME) ");
                    t_sb.AppendFormat("values ('{0}','{1}',", 1, i_sampleresultinfo.SampleInfo.SampleNo);
                    t_sb.AppendFormat("'{0}',", i_sampleresultinfo.SampleInfo.SampleDate.ToString("yyyy-MM-dd"));
                    t_sb.AppendFormat("'{0}',", Consts.Config.TestGroup);
                    t_sb.AppendFormat("'{0}',", 0);
                    t_sb.AppendFormat("'{0}',", 1);
                    t_sb.AppendFormat("'{0}')", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"));
                    break;
                #endregion
            }
            SqlHelper.ExecuteNonQuery(Consts.Config.ConnectString, CommandType.Text, t_sb.ToString());
        }
        #endregion

    }
}
