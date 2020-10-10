using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    public class EnhancedCOMException : COMException
    {
        public string LastApiError { get; }

        private readonly COMException _originalException;

        public EnhancedCOMException(string lastApiError, COMException originalException) : base(lastApiError, originalException)
        {
            LastApiError = lastApiError;
            _originalException = originalException;
        }

        public override IDictionary Data => _originalException?.Data ?? base.Data;

        public override int ErrorCode => _originalException?.ErrorCode ?? base.ErrorCode;

        public override string HelpLink
        {
            get => _originalException?.HelpLink ?? base.HelpLink;
            set { if (_originalException != null) { _originalException.HelpLink = value; } else { base.HelpLink = value; } }
        }

        public override string Message => _originalException?.Message ?? base.Message;

        public override Exception GetBaseException() => _originalException?.GetBaseException() ?? base.GetBaseException();

        public override void GetObjectData(SerializationInfo info, StreamingContext context) { if (_originalException != null) { _originalException.GetObjectData(info, context); } else { base.GetObjectData(info, context); } }

        public override string Source
        {
            get => _originalException?.Source ?? base.Source;
            set { if (_originalException != null) { _originalException.Source = value; } else { base.Source = value; } }
        }

        public override string StackTrace => _originalException?.StackTrace ?? base.StackTrace;

        public override bool Equals(object obj) => _originalException?.Equals(obj) ?? base.Equals(obj);

        public override int GetHashCode() => _originalException?.GetHashCode() ?? base.GetHashCode();

        public override string ToString() => _originalException?.ToString() ?? base.ToString();
    }
}