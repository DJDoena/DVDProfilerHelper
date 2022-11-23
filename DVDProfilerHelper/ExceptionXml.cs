using System;
using System.Linq;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    [Serializable]
    public class ExceptionXml
    {
        public string Type;

        public string Message;

        public string StackTrace;

        public string LastDVDProfilerApiError;

        public ExceptionXml InnerException;

        public ExceptionXml[] InnerExceptions;

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

                if (exception is EnhancedCOMException comEx)
                {
                    LastDVDProfilerApiError = comEx.LastApiError;
                }

                if (exception is AggregateException aggrEx && aggrEx.InnerExceptions?.Count > 0)
                {
                    InnerExceptions = aggrEx.InnerExceptions.Select(ex => new ExceptionXml(ex)).ToArray();
                }
                else if (exception.InnerException != null)
                {
                    InnerException = new ExceptionXml(exception.InnerException);
                }
            }
        }
    }
}