using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Framework
{
    class Log4net
    {
        public  log4net.ILog log;
        //static Log4net()
        //{
        //    log= log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //}

        public Log4net(String classname)
        {
            log = log4net.LogManager.GetLogger(classname);
        }
        public void Info(String message)
        {

            log.Info(message);
        }
        public void Error(String message)
        {
            log.Error(message);
        }
        public void Debug(String message)
        {
            log.Debug(message);
        }
        public void Warning(String message)
        {
            log.Warn(message);
        }
    }
}
