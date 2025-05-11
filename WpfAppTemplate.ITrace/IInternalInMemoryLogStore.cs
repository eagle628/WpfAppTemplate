namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// インメモリログストアの内部インターフェース
    /// </summary>
    internal interface IInternalInMemoryLogStore
    {
        /// <summary>
        /// インメモリログストアにログを積むメソッド
        /// </summary>
        /// <param name="logRecord">ログレコード</param>
        void Push(LogRecord logRecord);
    }
}
