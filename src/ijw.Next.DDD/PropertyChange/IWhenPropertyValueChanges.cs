using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next.DDD {
    public interface IWhenPropertyChanges {
        event BeforePropertyChangingEventHandler BeforePropertyValueChanges;
        event AfterPropertyChangedEventHandler AfterPropertyValueChanged;
    }
}
