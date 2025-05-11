using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.IScope
{
    /// <summary>
    /// DI登録のための拡張メソッド定義
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection UseScopeManagement(this IServiceCollection services)
        {
            return services
                        .AddSingleton<IScopeManager, ScopeManager>()
                        .AddScoped<IScopeId, ScopeId>();
        }
    }
}
