using System;

using Serilog.Configuration;
using Serilog.Core;

namespace Serilog.Enrichers.MassTransit
{

    /// <summary>
    /// Provides various extension methods for configuring Serilog.
    /// </summary>
    public static class SerilogConfigurationExtensions
    {

        /// <summary>
        /// Enriches the Serilog logging data with Mass Transit context information.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static LoggerConfiguration WithMassTransitContext(this LoggerEnrichmentConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            return configuration.With((ILogEventEnricher)new SerilogEnricher());
        }

    }

}
