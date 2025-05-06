using System.Collections.Generic;
using System.ComponentModel;

namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// インメモリログストアIF
    /// </summary>
    public interface IInMemoryLogStore : INotifyPropertyChanged
    {
        /// <summary>
        /// インメモリに蓄積されたログ一覧
        /// </summary>
        ILogRecord[] LogRecords { get; }
        /// <summary>
        /// 最後にインメモリに蓄積されたログ
        /// </summary>
        ILogRecord HeadLogRecord { get; }
    }
}
