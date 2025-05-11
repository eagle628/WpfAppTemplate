using System;

namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// ログレコード構造体
    /// </summary>
    /// <remarks>速度を考慮して防御的コピーを避けるためReadOnly構造体を守ること。</remarks>
    public readonly struct LogRecord
    {
        /// <summary>
        /// カテゴリ名
        /// </summary>
        public string CategoryName { get; }
        /// <summary>
        /// ログレベル
        /// </summary>

        public LogLevel LogLevel { get; }
        /// <summary>
        /// ログメッセージ
        /// </summary>

        public string Message { get; }
        /// <summary>
        /// ログ発生時刻
        /// </summary>

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
