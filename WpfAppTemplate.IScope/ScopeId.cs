using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IScope
{
    /// <summary>
    /// スコープ識別IDクラス
    /// </summary>
    /// <remarks>このクラスはDIによってScopedで管理されることを前提にしたクラスである。</remarks>
    internal sealed class ScopeId : IScopeId
    {
        public Guid Guid { get; }
        public ScopeId()
        {
            Guid = Guid.NewGuid();
        }
    }
}
