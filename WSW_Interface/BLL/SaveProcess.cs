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
            //�����������߳�
            Thread t_thread = new Thread( new ThreadStart( Process ) );
            t_thread.IsBackground = true;
            t_thread.Start( );
            //������ȡʧ���ļ��߳�
            Thread t_thread2 = new Thread( new ThreadStart( LoadTempFile ) );
            t_thread2.IsBackground = true;
            t_thread2.Start( );
        }

        /// <summary>
        /// ��⿪ʼ����
        /// </summary>
        public void Start ( ) { }

        /// <summary>
        /// �������Ϣ��ӵ����ݿ⴦�����
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

        #region ����߳�
        /// <summary>
        /// ���
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

        #region ��ȡ����ļ��߳�
        /// <summary>
        /// ��ȡδ�ɹ������ļ�
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
