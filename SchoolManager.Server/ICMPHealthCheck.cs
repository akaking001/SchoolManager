using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace SchoolManager.Server
{
    public class ICMPHealthCheck : IHealthCheck
    {
        private readonly string Host;
        private readonly int HealthyRoundTripTime;

        public ICMPHealthCheck(string host, int healthyRoundTripTime)
        {
            Host = host;
            HealthyRoundTripTime = healthyRoundTripTime;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(Host);
                switch (reply.Status)
                {
                    case IPStatus.Success:
                        {
                            var msg = $"ICMP tới {Host} mất {reply.RoundtripTime} ms.";

                            return (reply.RoundtripTime > HealthyRoundTripTime) ? HealthCheckResult.Degraded(msg) : HealthCheckResult.Healthy(msg);
                        }
                    default:
                        var err = $"ICMP tới {Host} không thành công: {reply.Status}";
                        return HealthCheckResult.Unhealthy(err);
                }
            }
            catch (Exception ex)
            {
                var err = $"ICMP tới {Host} không thành công: {ex.Message}";
                return HealthCheckResult.Unhealthy(err);
            }
        }
    }
}
