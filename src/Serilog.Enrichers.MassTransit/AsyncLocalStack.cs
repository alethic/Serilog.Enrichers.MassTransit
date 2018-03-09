using System;
using System.Collections.Immutable;

#if NET452
using System.Runtime.Remoting.Messaging;
#else
using System.Threading;
#endif

namespace Serilog.Enrichers.MassTransit
{

    /// <summary>
    /// Manages a Logical Call Context variable containing a stack of <typeparamref name="T"/> instances.
    /// </summary>
    public static class AsyncLocalStack<T>
    {


        /// <summary>
        /// Wraps the stack information.
        /// </summary>
        class LogicalContextData
#if NET452
            : MarshalByRefObject
#endif
        {

            public ImmutableStack<T> Stack { get; set; }

        }

#if NET452

        static readonly string LOGICAL_DATA_NAME = "LogicalCallContextStack:" + typeof(T).FullName;

        /// <summary>
        /// Gets the current context stack.
        /// </summary>
        static ImmutableStack<T> CurrentContext
        {
            get => ((LogicalContextData)CallContext.LogicalGetData(LOGICAL_DATA_NAME))?.Stack ?? ImmutableStack.Create<T>();
            set => CallContext.LogicalSetData(LOGICAL_DATA_NAME, new LogicalContextData { Stack = value });
        }

#else

        readonly static AsyncLocal<LogicalContextData> data = new AsyncLocal<LogicalContextData>();

        /// <summary>
        /// Gets the current context stack.
        /// </summary>
        static ImmutableStack<T> CurrentContext
        {
            get => data.Value?.Stack ?? ImmutableStack<T>.Empty;
            set => data.Value = new LogicalContextData { Stack = value };
        }

#endif

        /// <summary>
        /// Publishes a <see cref="T"/> onto the stack.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IDisposable Push(T context)
        {
            CurrentContext = CurrentContext.Push(context);
            return new PopWhenDisposed();
        }

        /// <summary>
        /// Gets the current <see cref="T"/>.
        /// </summary>
        public static T Current => Peek();

        /// <summary>
        /// Removes the last item from the stack.
        /// </summary>
        static void Pop()
        {
            var currentContext = CurrentContext;
            if (currentContext.IsEmpty == false)
                CurrentContext = currentContext.Pop();
        }

        /// <summary>
        /// Returns the last item on the stack.
        /// </summary>
        /// <returns></returns>
        static T Peek()
        {
            var currentContext = CurrentContext;
            if (currentContext.IsEmpty == false)
                return currentContext.Peek();
            else
                return default(T);
        }

        /// <summary>
        /// Implements a Pop operation when disposed.
        /// </summary>
        class PopWhenDisposed : IDisposable
        {

            bool disposed;

            /// <summary>
            /// Disposes of the instance.
            /// </summary>
            public void Dispose()
            {
                if (disposed == false)
                {
                    Pop();
                    disposed = true;
                }
            }

        }

    }
}
