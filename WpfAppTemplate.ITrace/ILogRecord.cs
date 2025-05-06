using System;
using Microsoft.Extensions.Logging;

namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// ログレコード構造体IF
    /// </summary>
    public interface ILogRecord
    {
        /// <summary>
        /// カテゴリ名
        /// </summary>
        string CategoryName { get; }
        /// <summary>
        /// 記録時刻
        /// </summary>
        DateTime DateTime { get; }
        /// <summary>
        /// ログレベル
        /// </summary>
        LogLevel LogLevel { get; }
        /// <summary>
        /// ログメッセージ
        /// </summary>
        string Message { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5,
        None = 6,
    }
}