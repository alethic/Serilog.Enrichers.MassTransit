﻿using System.Threading.Tasks;

using MassTransit;

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
            context.CreateFilterScope("SerilogEnricher");
        }

        public async Task Send(T context, IPipe<T> next)
        {
            using (PipeContextStack.Push(context))
                await next.Send(context);
        }

    }

}
