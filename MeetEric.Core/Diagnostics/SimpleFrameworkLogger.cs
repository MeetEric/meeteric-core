using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeetEric.Security;

namespace MeetEric.Diagnostics
{
    public class SimpleFrameworkLogger : ILoggingService
    {
        public void AddExtension(ILoggingExtension extension)
        {
        }

        public ILoggingContext CreateLoggingContext()
        {
            var id = new IdentifierFactory().Generate();
            return CreateLoggingContext(id);
        }

        public ILoggingContext CreateLoggingContext(IIdentifier requestId)
        {
            return new TraceContext(requestId);
        }

        public void EnableVerboseLogging()
        {
        }

        public IDiagnostics ReadDiagnostics()
        {
            throw new NotImplementedException();
        }

        private class TraceContext : ILoggingContext
        {
            public TraceContext(IIdentifier requestId)
            {
                RequestId = requestId;
            }

            public IIdentifier RequestId { get; private set; }

            public void Dispose()
            {
            }

            public void LogEvent(string eventName, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
            {
                Write($"EVENT {eventName}", contextProperties, metrics);
            }

            public void LogException(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
            {
                Write("EXCEPTION", contextProperties, metrics);
                Write(exception.ToString());
            }

            public void LogMetric(string metricName, double metricValue = 1, Dictionary<string, string> contextProperties = null)
            {
                Write($"METRIC {metricName}. VALUE {metricValue}", contextProperties);
            } 

            public void LogRequest(DateTimeOffset origination, HttpRequestMessage request, HttpResponseMessage response)
            {
                Write($"REQUEST: {request.RequestUri}. RESPONSE: {response?.StatusCode}");
            }

            public void LogRetry(Exception exception, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
            {
                Write($"RETRY: {exception.GetType().Name}");
                LogException(exception, contextProperties, metrics);
            }

            private void Write(string message, Dictionary<string, string> contextProperties = null, Dictionary<string, double> metrics = null)
            {
                Debug.WriteLine(message);
            }
        }
    }
}
