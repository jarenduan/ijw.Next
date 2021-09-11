using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.DDD {
    /// <summary>
    /// 
    /// </summary>
    public abstract class AggregateRootBase : EntityBase, IAggregateRoot<Guid> {
    }
}
