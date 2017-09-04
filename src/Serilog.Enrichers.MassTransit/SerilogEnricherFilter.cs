using System.Threading.Tasks;

using GreenPipes;

namespace Serilog.Enrichers.MassTransit
{

    /// <summary>
    /// Maintains the current pipe context on the logical call stack.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class SerilogEnricherFilter<T> : 
        IFilter<T>
        where T : class, PipeContext
    {

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateFilterScope("SerilogEnricher");
            scope.Add("Data", SerilogEnricherData.Current);
        }

        public async Task Send(T context, IPipe<T> next)
        {
            using (PipeContextStack.Push(context))
                await next.Send(context);
        }

    }

}
