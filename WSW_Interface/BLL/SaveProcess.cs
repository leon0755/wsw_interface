using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
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
        private static object g_lockobj = new object( );

        private string m_tempresult_path = "TempResult";

        private SaveProcess ( )
        {
            //创建入库队列线程
            Thread t_thread = new Thread( new ThreadStart( Process ) );
            t_thread.IsBackground = true;
            t_thread.Start( );
            //创建读取失败文件线程
            Thread t_thread2 = new Thread( new ThreadStart( LoadTempFile ) );
            t_thread2.IsBackground = true;
            t_thread2.Start( );
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

        #region 入库线程
        /// <summary>
        /// 入库
        /// </summary>
        private void Process ( )
        {
            while (true)
            {
                WSWSampleResultInfo t_sampleresultinfo = null;
                if (m_data_queue.Count == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                lock (g_lockobj)
                {
                    t_sampleresultinfo = m_data_queue.Dequeue( );
                }

                if (t_sampleresultinfo != null)
                {
                    WSWDataBaseProcess t_db_process = new WSWDataBaseProcess( );
                    t_db_process.SaveToDB( t_sampleresultinfo );

                }

                Thread.Sleep( 1000 );
            }
        }
        #endregion

        #region 读取结果文件线程
        /// <summary>
        /// 读取未成功入库的文件
        /// </summary>
        private void LoadTempFile ( )
        {
            while (true)
            {
                string t_resultfolder = Path.Combine( Consts.g_exe_folder, m_tempresult_path );
                if (Directory.Exists( t_resultfolder ) == false)
                {
                    continue;
                }
                Thread.Sleep( 1000 );
            }
        }
        #endregion
    }
}
