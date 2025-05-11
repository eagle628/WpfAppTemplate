using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        LogRecord[] LogRecords { get; }
        /// <summary>
        /// 最後にインメモリに蓄積されたログ
        /// </summary>
        LogRecord HeadLogRecord { get; }
    }
}
