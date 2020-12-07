using ijw.Next.Reflection;

using System.Data;

namespace ijw.Next.DataAccess.Oracle.Core {
    static class ObjExt {
        public static T FillPropertiesOfBasicType<T>(this T obj, IDataRecord dataRecord) 
            where T : class 
            => obj.FillPropertiesOfBasicType((p) => dataRecord[p.Name]);
    }
}