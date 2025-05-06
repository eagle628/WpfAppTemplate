using Microsoft.Extensions.Logging;

namespace $ext_safeprojectname$.Trace
{
    /// <summary>
    /// インメモリログストアの設定
    /// </summary>
    public sealed class InMemoryLogStoreConfig
    {
        /// <summary>
        /// インメモリログレコードの上限数
        /// </summary>
        public uint Capacity { get; set; } = 64;
        /// <summary>
        /// インメモリログストアに登録される最小ログレベル
        /// </summary>
        public LogLevel MinLevel { get; set; } = LogLevel.Information;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InMemoryLogStoreConfig() { }
    }
}
