using System;

namespace $ext_safeprojectname$.IMessageBroker
{
    /// <summary>
    /// イベント購読解除クラス
    /// </summary>
    /// <remarks>直接このクラスを使うことはない。<see cref="IDisposable"/>を通して使う</remarks>
    internal sealed class Subscription : IDisposable
    {
        /// <summary>
        /// 購読解除関数
        /// </summary>
        private readonly Action _unsubscribe;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unsubscribe">購読解除関数</param>
        public Subscription(Action unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }
        /// <summary>
        /// 破棄関数
        /// </summary>
        public void Dispose()
        {
            _unsubscribe();
        }
    }
}
