using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IScope
{
    /// <summary>
    /// スコープ管理IF
    /// </summary>
    /// <remarks>
    /// Scopeの作成・破棄についてはこのクラスを通して作業することを期待する。
    /// </remarks>
    public interface IScopeManager
    {
        /// <summary>
        /// 管理されているスコープ一覧
        /// </summary>
        IReadOnlyCollection<IServiceScope> ServiceScopes { get; }
        /// <summary>
        /// スコープの生成
        /// </summary>
        /// <returns>スコープIDおよびスコープのTuple</returns>

        (Guid ScopeId, IServiceScope Scope) CreateScope();
        /// <summary>
        /// 指定されたスコープの放棄
        /// </summary>
        /// <param name="guid">スコープ固有のID</param>
        void DestroyScope(Guid guid);
        /// <summary>
        /// 指定されたスコープの取得
        /// </summary>
        /// <param name="guid">スコープ固有のID</param>
        /// <returns>スコープ</returns>
        IServiceScope GetScope(Guid guid);
    }
}
