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

        public bool SaveToDB ( WSWSampleResultInfo i_resultinfo )
        {
            lock (g_lockobj)
            {
                bool t_result = false;



                return t_result;
            }
        }
    }
}
