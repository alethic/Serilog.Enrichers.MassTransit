using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enricher.MassTransit
{

    /// <summary>
    /// Enriches Serilog data with Mass Transit data.
    /// </summary>
    public class SerilogEnricher :
        ILogEventEnricher
    {

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
        {
            var data = SerilogEnricherData.Current;
            if (data != null)
                logEvent.AddOrUpdateProperty(factory.CreateProperty("MassTransit", data, true));
        }

    }

}
