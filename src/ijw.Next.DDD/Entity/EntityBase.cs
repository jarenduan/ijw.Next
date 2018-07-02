using System;

namespace ijw.Next.DDD { 
    /// <summary>
    /// 实体的基类
    /// </summary>
    public abstract class EntityBase : NotifyPropertyChangeBase, IEntity<Guid> { 
        /// <summary>
        /// 实体的ID
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public EntityBase() => this.Id = new Guid();

        /// <summary>
        /// 根据指定的ID构造实体
        /// </summary>
        /// <param name="id">指定的ID</param>
        public EntityBase(Guid id) => this.Id = id;
    }
}
