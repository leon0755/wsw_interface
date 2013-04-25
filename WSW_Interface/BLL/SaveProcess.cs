using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Palgain.CommonModule;

namespace Palgain.BLL
{
    public class SaveProcess
    {

        #region singleton
        private static SaveProcess g_instance = null;

        public static SaveProcess Instance
        {
            get 
            {
                if (g_instance == null)
                {
                    g_instance = new SaveProcess( );
                }
                return g_instance; 
            }
        }
        #endregion

        private Queue<WSWSampleResultInfo> m_data_queue = new Queue<WSWSampleResultInfo>( );
        private WSWDataBaseProcess m_db_process = new WSWDataBaseProcess( );
        private static object g_lockobj = new object( );

        private SaveProcess ( )
        {
            //创建入库队残线程

            //创建读取失败文件线程

        }

        /// <summary>
        /// 入库开始监视
        /// </summary>
        public void Start ( ) { }

        /// <summary>
        /// 将结果信息添加到数据库处理队列
        /// </summary>
        /// <param name="i_data"></param>
        public void AddResultInfo ( WSWSampleResultInfo i_data )
        {
            lock (g_lockobj)
            {
                if (i_data != null)
                {
                    m_data_queue.Enqueue( i_data );
                }
            }
        }

        private void Process ( )
        {
            while (true)
            {
                WSWSampleResultInfo t_sampleresultinfo = null;
                lock (g_lockobj)
                {
                    t_sampleresultinfo = m_data_queue.Dequeue( );
                }

                if (t_sampleresultinfo != null)
                {
                    m_db_process.SaveToDB( t_sampleresultinfo );
                }

                Thread.Sleep( 1000 );
            }
        }
    }
}
