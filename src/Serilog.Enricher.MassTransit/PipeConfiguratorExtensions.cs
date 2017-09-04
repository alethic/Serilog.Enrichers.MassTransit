using GreenPipes;

namespace Serilog.Enricher.MassTransit
{

    public static class PipeConfiguratorExtensions
    {

        public static void UseSerilogEnricher<T>(this IPipeConfigurator<T> configurator)
            where T : class, PipeContext
        {
            configurator.AddPipeSpecification(new SerilogEnricherSpecification<T>());
        }

    }

}
