using Serilog.Configuration;
using Serilog.Core;

namespace Serilog.Enricher.MassTransit
{

    public static class SerilogConfigurationExtensions
    {

        public static LoggerConfiguration WithMassTransitContext(this LoggerEnrichmentConfiguration configuration)
        {
            return configuration.With((ILogEventEnricher)new SerilogEnricher());
        }

    }

}
