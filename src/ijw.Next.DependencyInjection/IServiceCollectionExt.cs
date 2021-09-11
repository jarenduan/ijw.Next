using Microsoft.Extensions.DependencyInjection;

namespace ijw.Next.DependencyInjection {
    /// <summary>
    /// 
    /// </summary>
    public static class IServiceCollectionExt {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srv"></param>
        /// <returns></returns>
        public static T GetService<T>(this IServiceCollection srv) {
            return srv.BuildServiceProvider().GetService<T>();
        }
    }
}
