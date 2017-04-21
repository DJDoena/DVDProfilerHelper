using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    [Serializable()]
    public class ExceptionXml
    {
        public String Type;

        public String Message;

        public String StackTrace;

        public String LastDVDProfilerApiError;

        public ExceptionXml InnerException;

        public ExceptionXml()
        { }

        public ExceptionXml(Exception exception)
        {
            if (exception != null)
            {
                Type = exception.GetType().FullName;

                Message = exception.Message;

                StackTrace = exception.StackTrace;

                EnhancedCOMException comEx = exception as EnhancedCOMException;

                if (comEx != null)
                {
                    LastDVDProfilerApiError = comEx.LastApiError;
                }

                if (exception.InnerException != null)
                {
                    InnerException = new ExceptionXml(exception.InnerException);
                }
            }
        }
    }
}