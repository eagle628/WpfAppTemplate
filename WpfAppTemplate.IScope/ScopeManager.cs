using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace $ext_safeprojectname$.IScope
{
    /// <summary>
    /// スコープ管理クラス
    /// </summary>
    internal sealed class ScopeManager : IDisposable, IScopeManager
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private readonly ILogger<ScopeManager> _logger;
        /// <summary>
        /// ルートのサービスプロバイダ
        /// </summary>
        private readonly IServiceProvider _rootServiceProvider;
        /// <summary>
        /// サービススコープの辞書
        /// </summary>
        private readonly ConcurrentDictionary<Guid, IServiceScope> _serviceScopes;
        /// <inheritdoc/>
        public IReadOnlyCollection<IServiceScope> ServiceScopes => _serviceScopes.Values.ToList();
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="rootServiceProvider">ルートのサービスプロバイダ</param>
        public ScopeManager(ILogger<ScopeManager> logger, IServiceProvider rootServiceProvider)
        {
            _logger = logger;
            _rootServiceProvider = rootServiceProvider;

            _serviceScopes = new ConcurrentDictionary<Guid, IServiceScope>();
        }
        /// <inheritdoc/>
        public (Guid, IServiceScope) CreateScope()
        {
            _logger.LogDebug("start create new scope");
            var serviceScope = _rootServiceProvider.CreateScope();
            var id = serviceScope.ServiceProvider.GetRequiredService<IScopeId>();
            _ = _serviceScopes.TryAdd(id.Guid, serviceScope);
            _logger.LogDebug("end create new scope");
            return (id.Guid, serviceScope);
        }
        /// <inheritdoc/>
        public IServiceScope GetScope(Guid guid)
        {
            return _serviceScopes.TryGetValue(guid, out var serviceScope) ? serviceScope : null;
        }
        /// <inheritdoc/>
        public void DestroyScope(Guid guid)
        {
            if (_serviceScopes.TryGetValue(guid, out var scope))
            {
                _logger.LogDebug("start destroy scope");
                scope.Dispose();
                _ = _serviceScopes.TryRemove(guid, out _);
                _logger.LogDebug("end destroy scope");
            }
        }
        /// <summary>
        /// 破棄メソッド
        /// </summary>
        public void Dispose()
        {
            foreach (var disposable in _serviceScopes.Values.OfType<IDisposable>())
            {
                disposable.Dispose();
            }
        }
    }
}
