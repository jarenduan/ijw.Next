using System;

namespace ijw.Next.DDD {
    /// <summary>
    /// 实体的接口
    /// </summary>
    /// <typeparam name="TKey">ID的类型</typeparam>
    public interface IEntity<TKey> {
        /// <summary>
        /// 实体的ID
        /// </summary>
        TKey Id { get; }
    }
}