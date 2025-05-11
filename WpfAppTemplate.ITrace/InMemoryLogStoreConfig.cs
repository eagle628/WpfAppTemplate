namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// インメモリログストアの設定
    /// </summary>
    internal sealed class InMemoryLogStoreConfig
    {
        /// <summary>
        /// インメモリログレコードの上限数
        /// </summary>
        public uint Capacity { get; set; } = 64;
        /// <summary>
        /// インメモリログストアに登録される最小ログレベル
        /// </summary>
        public Microsoft.Extensions.Logging.LogLevel MinLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Information;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InMemoryLogStoreConfig() { }
    }
}
