using System.Collections.Generic;
using System.Linq;

using MassTransit;
using MassTransit.Configuration;

namespace Serilog.Enrichers.MassTransit
{

    class SerilogEnricherSpecification<T> :
        IPipeSpecification<T>
        where T : class, PipeContext
    {

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new SerilogEnricherFilter<T>());
        }

    }

}
