using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ijw.Next {
    /// <summary>
    /// 
    /// </summary>
    public static class NullableBoolExt {
        /// <summary>
        /// Check nullable value. return true only when it is not null and not false.
        /// </summary>
        /// <param name="nullableBool"></param>
        /// <returns>return true only when it is not null and not false.</returns>
        public static bool IsNotTrue(this bool? nullableBool) => nullableBool is null || nullableBool == false;
    }
}
