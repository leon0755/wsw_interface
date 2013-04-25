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
            //�������Ӳ��߳�

            //������ȡʧ���ļ��߳�

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
