using System;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    [Serializable()]
    public class ExceptionXml
    {
        public string Type;

        public string Message;

        public string StackTrace;

        public string LastDVDProfilerApiError;

        public ExceptionXml InnerException;

        public ExceptionXml()
        {
        }

        public ExceptionXml(Exception exception)
        {
            if (exception != null)
            {
                Type = exception.GetType().FullName;

                Message = exception.Message;

                StackTrace = exception.StackTrace;

                var comEx = exception as EnhancedCOMException;

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