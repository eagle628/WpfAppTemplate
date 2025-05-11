using Microsoft.Extensions.Logging;
using System;

namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// インメモリロガー
    /// </summary>
    internal class InMemoryLogger : ILogger
    {
        /// <summary>
        /// カテゴリ名
        /// </summary>
        private readonly string _name;
        /// <summary>
        /// インメモリログストア
        /// </summary>
        private readonly IInternalInMemoryLogStore _store;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">カテゴリ名</param>
        /// <param name="store">インメモリログストア</param>
        public InMemoryLogger(string name, IInternalInMemoryLogStore store)
        {
            _name = name;
            _store = store;
        }
        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }
        /// <inheritdoc/>
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return logLevel != Microsoft.Extensions.Logging.LogLevel.None;
        }
        /// <inheritdoc/>
        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            _store.Push(new LogRecord(_name, logLevel, message));
        }
    }
}
