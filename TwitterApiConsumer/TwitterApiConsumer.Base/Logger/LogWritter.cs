using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TwitterApiConsumer.Base.Logger
{
    public class LogWritter
    {
        #region private Field
        private static object _obj = new object();
        #endregion
        static LogWritter()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.Indent();            
            Trace.Unindent();            
        }
        public void Write(string msg)
        {
            try
            {
                lock (_obj)
                {
                    Trace.WriteLine($"{DateTime.Now} - {msg} {Environment.NewLine}-----------------------------------------------------");
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
