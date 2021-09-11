using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.DDD {
    /// <summary>
    /// 
    /// </summary>
    public interface IWhenPropertyChanges {
        /// <summary>
        /// 属性值更改之前触发此事件
        /// </summary>
        event BeforePropertyChangingEventHandler BeforePropertyValueChanges;

        /// <summary>
        /// 属性值更改之后触发此事件
        /// </summary>
        event AfterPropertyChangedEventHandler AfterPropertyValueChanged;
    }
}
