using System;

using GreenPipes;

using Serilog.Enrichers.MassTransit;

namespace MassTransit
{

    /// <summary>
    /// Provides extension methods for relating MassTransit context information to Serilog.
    /// </summary>
    public static class PipeConfiguratorExtensions
    {

        /// <summary>
        /// Injects the required configuration into Mass Transit to allow Serilog to acquire enrichment data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configurator"></param>
        public static void UseSerilogEnricher<T>(this IPipeConfigurator<T> configurator)
            where T : class, PipeContext
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            configurator.AddPipeSpecification(new SerilogEnricherSpecification<T>());
        }

    }

}
