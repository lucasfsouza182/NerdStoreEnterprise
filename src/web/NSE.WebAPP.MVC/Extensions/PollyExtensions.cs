using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace NSE.WebAPP.MVC.Extensions
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndTry()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Trying {retryCount} times!");
                    Console.ForegroundColor = ConsoleColor.White;
                });

            return retry;
        }
    }
}
