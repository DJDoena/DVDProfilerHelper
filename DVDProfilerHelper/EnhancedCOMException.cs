using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public class EnhancedCOMException : COMException
    {
        public readonly String LastApiError;

        private readonly COMException OriginalException;

        public EnhancedCOMException(COMException originalException
            , String lastApiError)
        {
            OriginalException = originalException;
            LastApiError = lastApiError;
        }

        public override IDictionary Data
        {
            get
            {
                return (OriginalException.Data);
            }
        }

        public override Int32 ErrorCode
        {
            get
            {
                return (OriginalException.ErrorCode);
            }
        }

        public override String HelpLink
        {
            get
            {
                return (OriginalException.HelpLink);
            }
            set
            {
                OriginalException.HelpLink = value;
            }
        }

        public override String Message
        {
            get
            {
                return (OriginalException.Message);
            }
        }

        public override Exception GetBaseException()
        {
            return (OriginalException.GetBaseException());
        }

        public override void GetObjectData(SerializationInfo info
            , StreamingContext context)
        {
            OriginalException.GetObjectData(info, context);
        }

        public override String Source
        {
            get
            {
                return (OriginalException.Source);
            }
            set
            {
                OriginalException.Source = value;
            }
        }

        public override String StackTrace
        {
            get
            {
                return (OriginalException.StackTrace);
            }
        }

        public override Boolean Equals(Object obj)
        {
            return (OriginalException.Equals(obj));
        }

        public override int GetHashCode()
        {
            return (OriginalException.GetHashCode());
        }

        public override String ToString()
        {
            return (OriginalException.ToString());
        }
    }
}