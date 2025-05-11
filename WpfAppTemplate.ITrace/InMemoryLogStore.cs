using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace $ext_safeprojectname$.ITrace
{
    /// <summary>
    /// インメモリログストアクラス
    /// </summary>
    internal class InMemoryLogStore : IInMemoryLogStore, IInternalInMemoryLogStore
    {
        /// <summary>
        /// <see cref="INotifyPropertyChanged"/>実装イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// プロパティ変更イベント発行
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <inheritdoc/>
        public void Push(LogRecord record)
        {
            if (record.LogLevel < _minLevel)
            {
                return;
            }
            lock (_syncObject)
            {
                _top = (_top + 1) & _mask;
                _logRecords[_top] = record;
            }
            OnPropertyChanged(nameof(LogRecords));
            OnPropertyChanged(nameof(HeadLogRecord));
        }
        /// <summary>
        /// ログを積む配列を同期するオブジェクト
        /// </summary>
        private readonly object _syncObject;
        /// <summary>
        /// ログデータ配列
        /// </summary>
        /// <remarks>型が構造体であるため、連続的にデータが並ぶことが保証されている。</remarks>
        private readonly LogRecord[] _logRecords;
        /// <summary>
        /// ログデータ配列長
        /// </summary>
        /// <remarks>リングバッファの計算を楽にするため2^nとすること</remarks>
        private readonly int _capacity;
        /// <summary>
        /// ログ配列のIndexを高速にmod演算するようのマスク
        /// </summary>
        private readonly int _mask;
        /// <summary>
        /// ログ配列の積む位置のIndex
        /// </summary>
        private int _top;
        /// <summary>
        /// ログ配列に積むログの最低レベル
        /// </summary>
        private readonly LogLevel _minLevel;
        /// <inheritdoc/>
        public LogRecord[] LogRecords
        {
            get
            {
                Span<LogRecord> rawData;
                int tempTop;
                lock (_syncObject)
                {
                    //Loggerなので、複数のスレッドが高速にLogに積んだり、Logの取得を行う可能性がある。
                    //なので、処理中にTop位置やデータ位置が変わる可能性がある。
                    //よってコストはかかるがコピーしてしまう。
                    rawData = _logRecords.ToArray().AsSpan();
                    tempTop = _top;
                }

                //全データがデータを持つ場合は、1週目以降
                //構造体で配列をした場合は、最初は常に引数なしコンストラクタで初期化される
                bool isAllHasdata = true;
                for (int i = 0; i < rawData.Length; i++)
                {
                    if (!rawData[i].HasData)
                    {
                        isAllHasdata = false;
                        break;
                    }
                }

                if (isAllHasdata)
                {
                    //Top位置がBufferの最後尾の場合は、整列しているのでそのまま返す。
                    if (tempTop == _capacity - 1)
                    {
                        return rawData.ToArray();
                    }
                    var retData = new LogRecord[_capacity];
                    var refRetData = retData.AsSpan();
                    //先頭からTop位置までは最新データなので、戻り値の後ろに置く
                    rawData.Slice(0, tempTop + 1).CopyTo(refRetData.Slice(_capacity - tempTop - 1));
                    //Top位置より後から最後尾までは古めのデータなので、戻りの先頭からおく
                    rawData.Slice(tempTop + 1).CopyTo(refRetData);
                    return retData;
                }
                else
                {
                    //全データにデータがないときは、長さを調整して返す。
                    if (tempTop == _capacity - 1)
                    {
                        //全データにデータがないにも関わらず、なぜかTopが最後尾を指定している場合は、
                        //そもそもまだ、Logが積まれてない状況なので、空を返す。⇔コンストラクト直後
                        return new LogRecord[0];
                    }
                    return rawData.Slice(0, tempTop + 1).ToArray();
                }

            }
        }
        /// <inheritdoc/>
        public LogRecord HeadLogRecord => _logRecords[_top];
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="config">インメモリログストア設定</param>
        public InMemoryLogStore(InMemoryLogStoreConfig config)
        {
            _capacity = NearPow2(config.Capacity);
            _minLevel = Utility.ConvertLogLevel(config.MinLevel);
            _syncObject = new object();
            _logRecords = new LogRecord[_capacity];
            _mask = _capacity - 1;

            _top = _capacity - 1;
        }
        /// <summary>
        /// 引数よりも大きな最も小さい2のべき乗数を返す関数
        /// </summary>
        /// <param name="n">値</param>
        /// <returns>2のべき乗数</returns>
        private static int NearPow2(uint n)
        {
            if (n > int.MaxValue)
            {
                return int.MaxValue;
            }
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException("InMemoryLogger:Capacity is greater than 1.");
            }
            //ガードで問題ないはず
            int m = (int)n;
            double l = Math.Ceiling(Math.Log(m, 2));
            return (int)Math.Pow(2.0, l);
        }
    }
}
