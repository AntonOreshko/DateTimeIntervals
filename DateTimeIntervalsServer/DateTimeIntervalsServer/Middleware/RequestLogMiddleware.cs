using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using DateTimeIntervals.Logger.DomainModels;
using DateTimeIntervals.Logger.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DateTimeIntervals.Api.Middleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILoggerRepository _loggerRepository;

        private readonly ILogger _logger;

        public RequestLogMiddleware(RequestDelegate next, ILoggerFactory factory, IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = factory.CreateLogger("RequestLog");
            _loggerRepository = serviceProvider.GetService<ILoggerRepository>();

        }

        private Func<LogData, string> _logLineFormatter;

        private Func<LogData, string> LogLineFormatter
        {
            get
            {

                if (this._logLineFormatter != null)
                {
                    return this._logLineFormatter;
                }
                return this.DefaultFormatter();
            }
            set => this._logLineFormatter = value;
        }

        private Func<LogData, string> DefaultFormatter()
        {
            return (logData => $"{logData.RemoteAddr} - {logData.User} {logData.RequestTimestamp} \"{logData.RequestMethod} {logData.RequestPath} {logData.RequestProtocol}\" {logData.ResponseStatus} \"{logData.UserAgent}\" {logData.DurationMs}ms");
        }

        public void SetLogLineFormat(Func<LogData, string> formatter)
        {
            this._logLineFormatter = formatter;
        }

        public async Task Invoke(HttpContext context)
        {
            var now = DateTime.Now;
            var watch = Stopwatch.StartNew();
            await _next.Invoke(context);
            watch.Stop();

            var nowString = now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);
            var user = context.User.Identity.Name ?? "-";
            var request = context.Request.Path + (string.IsNullOrEmpty(context.Request.QueryString.ToString()) ? "" : context.Request.QueryString.ToString());
            var responseStatus = context.Response.StatusCode;
            var userAgent = context.Request.Headers.ContainsKey("User-Agent") ? context.Request.Headers["User-Agent"].ToString() : "-";
            var protocol = context.Request.Protocol;
            var duration = watch.ElapsedMilliseconds;
            var remoteAddr = context.Connection.RemoteIpAddress;
            var method = context.Request.Method;

            var logData = new LogData
            {
                RemoteAddr = remoteAddr.ToString(),
                RequestMethod = method,
                RequestPath = request,
                RequestProtocol = protocol,
                RequestTimestamp = nowString,
                ResponseStatus = responseStatus,
                User = user,
                UserAgent = userAgent,
                DurationMs = duration,
            };

            _logger.LogInformation(LogLineFormatter(logData));

            _loggerRepository.AddLogData(logData);
            await _loggerRepository.SaveAll();
        }
    }
}
