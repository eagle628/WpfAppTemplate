using Microsoft.Extensions.Logging;
using System;
using $ext_safeprojectname$.ITrace;

namespace $ext_safeprojectname$.Trace
{
    /// <summary>
    /// ログレコード構造体
    /// </summary>
    /// <remarks>速度を考慮して防御的コピーを避けるためReadOnly構造体を守ること。</remarks>
    public readonly struct LogRecord : ILogRecord
    {
        /// <inheritdoc cref="ILogRecord.CategoryName"/>
        public string CategoryName { get; }
        /// <inheritdoc cref="ILogRecord.LogLevel"/>

        public ITrace.LogLevel LogLevel { get; }
        /// <inheritdoc cref="ILogRecord.Message"/>

        public string Message { get; }
        /// <inheritdoc cref="ILogRecord.DateTime"/>

        public DateTime DateTime { get; }
        /// <summary>
        /// データの保持フラグ
        /// </summary>
        internal bool HasData { get; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="categoryName">ログカテゴリ名</param>
        /// <param name="logLevel">ログレベル</param>
        /// <param name="message">ログメッセージ</param>
        internal LogRecord(string categoryName, Microsoft.Extensions.Logging.LogLevel logLevel, string message)
        {
            CategoryName = categoryName;
            LogLevel = Utility.ConvertLogLevel(logLevel);
            Message = message;
            DateTime = DateTime.Now;
            HasData = true;
        }
    }
}
