using Microsoft.Extensions.DependencyInjection;

namespace ijw.Next.DependencyInjection {
    /// <summary>
    /// 
    /// </summary>
    public static class IServiceCollectionExt {
        public static T GetService<T>(this IServiceCollection srv) {
            return srv.BuildServiceProvider().GetService<T>();
        }
    }
}
