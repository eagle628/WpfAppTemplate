using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// <see cref="InMemoryLogger"/>をLoggerに登録するためのクラス
    /// </summary>
    internal sealed class InMemoryLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, InMemoryLogger> _loggers;
        private readonly IInternalInMemoryLogStore _store;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="store">インメモリなログストア</param>
        internal InMemoryLoggerProvider(IInternalInMemoryLogStore store)
        {
            _loggers = new ConcurrentDictionary<string, InMemoryLogger>(StringComparer.OrdinalIgnoreCase);
            _store = store;
        }
        /// <summary>
        /// ロガー生成
        /// </summary>
        /// <param name="categoryName">ログカテゴリ</param>
        /// <returns>ロガーインスタンス</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new InMemoryLogger(name, _store));
        }
        /// <summary>
        /// 破棄メソッド
        /// </summary>
        public void Dispose()
        {

        }
    }
}
