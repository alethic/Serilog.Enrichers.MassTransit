using GreenPipes;

using MassTransit;

namespace Serilog.Enricher.MassTransit
{

    /// <summary>
    /// Provides enrichment data.
    /// </summary>
    public static class SerilogEnricherData
    {

        /// <summary>
        /// Gets the current enrichment data.
        /// </summary>
        public static object Current => new
        {
            Message = FromMessageContext(PipeContextStack.Current?.GetPayload<MessageContext>())
        };

        /// <summary>
        /// Extracts property data from the given <see cref="MessageContext"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        static object FromMessageContext(MessageContext context)
        {
            return context != null ? new
            {
                context.ConversationId,
                context.CorrelationId,
                context.DestinationAddress,
                context.ExpirationTime,
                context.FaultAddress,
                context.InitiatorId,
                context.MessageId,
                context.RequestId,
                context.ResponseAddress,
                context.SourceAddress,
            } : null;
        }

    }

}
