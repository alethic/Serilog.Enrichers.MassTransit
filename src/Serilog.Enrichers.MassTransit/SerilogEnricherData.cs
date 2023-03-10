using MassTransit;

namespace Serilog.Enrichers.MassTransit
{

    /// <summary>
    /// Provides enrichment data.
    /// </summary>
    static class SerilogEnricherData
    {

        /// <summary>
        /// Gets the current enrichment data.
        /// </summary>
        public static object Current => GetData();

        /// <summary>
        /// Gets the enrichment data.
        /// </summary>
        /// <returns></returns>
        static object GetData()
        {
            var c = PipeContextStack.Current;
            var o = new
            {
                Message = FromMessageContext(c)
            };

            // only return if at least one property set
            if (o.Message != null)
                return o;

            return null;
        }

        /// <summary>
        /// Extracts property data from the given <see cref="MessageContext"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        static object FromMessageContext(PipeContext pipe)
        {
            var context = pipe?.GetPayload<MessageContext>();
            if (context == null)
                return null;
            else
                return new
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
                };
        }

    }

}
