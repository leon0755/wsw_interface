using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Palgain.CommonModule;

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
                if (t_etx_index < i_data.Length)
                {
                    m_data = i_data.Substring(t_etx_index + 1);
                }
                else
                {
                    m_data = "";
                }
            }
            else
            {
                m_data += i_data;
                return;
            }

            if (t_data.Length == 0)
            {
                return;
            }

            #region 数据没有问题,开始解码
            t_data = t_data.Replace((char)0x1e, ' ').Trim();

            string[] t_array = t_data.Split('|');

            SampleInfo t_sampleinfo = new SampleInfo();
            BactResult t_bactresult = new BactResult();
            AntiResult t_antiresult = new AntiResult();
            SampleResultInfo t_resultinfo = new SampleResultInfo();

            foreach (string t_text in t_array)
            {
                string t_flag = "";
                string t_content = "";

                if (t_text.Length < 2)
                {
                    continue;
                }
                t_flag = t_text.Substring(0, 2);
                t_content = t_text.Substring(2);

                switch (t_flag)
                {
                    #region 病人病历号(住院门诊号)
                    case "pi":
                        t_sampleinfo.PatientID = t_content;
                        break;
                    #endregion

                    #region 标本号
                    case "ci":
                        t_sampleinfo.SampleNo = t_content;
                        break;
                    #endregion

                    #region 标本日期
                    case "s1":
                        string[] t_dates = t_content.Split('/');
                        t_sampleinfo.SampleDate = new DateTime(
                            int.Parse(t_dates[2]),
                            int.Parse(t_dates[0]),
                            int.Parse(t_dates[1]));
                        break;
                    #endregion

                    #region 细菌隔离号
                    case "t1":
                        t_bactresult.IsolateNumber = t_content;
                        break;
                    #endregion

                    #region 细菌项目代码
                    case "o1":
                        t_bactresult.ItemCode = t_content;
                        break;
                    #endregion

                    #region 细菌项目名称
                    case "o2":
                        t_bactresult.ItemName = t_content;
                        break;
                    #endregion

                    #region 细菌项目生化编码
                    case "o3":
                        t_bactresult.BactBioCode = t_content;
                        break;
                    #endregion

                    #region 药敏项目编码
                    case "a1":
                        t_antiresult = new AntiResult();
                        t_antiresult.ItemCode = t_content;
                        t_resultinfo.AntiResults.Add(t_antiresult);
                        break;
                    #endregion

                    #region 药敏项目名称
                    case "a2":
                        t_antiresult.ItemName = t_content;
                        break;
                    #endregion

                    #region 药敏MIC值
                    case "a3":
                        t_antiresult.MicResult = t_content;
                        break;
                    #endregion

                    #region 药敏耐药结果
                    case "a4":
                        t_antiresult.AddResult = t_content;
                        break;
                    #endregion
                }
            }

            t_resultinfo.SampleInfo = t_sampleinfo;
            t_resultinfo.BactResult = t_bactresult;

            #endregion
        }
    }
}
