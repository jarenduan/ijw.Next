namespace ijw.Next {
    /// <summary>
    /// 接受ref参数的委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">ref 参数</param>
    public delegate void ActionWithRef<T>(ref T obj);

    /// <summary>
    /// 接受两个ref参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="obj1">第一个ref参数</param>
    /// <param name="obj2">第二个ref参数</param>

    public delegate void ActionWithRef<T1, T2>(ref T1 obj1, ref T2 obj2);

    /// <summary>
    /// 接受三个ref参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="obj1">第一个ref参数</param>
    /// <param name="obj2">第二个ref参数</param>
    /// <param name="obj3">第三个ref参数</param>
    public delegate void ActionWithRef<T1, T2, T3>(ref T1 obj1, ref T2 obj2, ref T3 obj3);


    /// <summary>
    /// 接受三个ref参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <param name="obj1">第1个ref参数</param>
    /// <param name="obj2">第2个ref参数</param>
    /// <param name="obj3">第3个ref参数</param>
    /// <param name="obj4">第4个ref参数</param>
    public delegate void ActionWithRef<T1, T2, T3, T4>(ref T1 obj1, ref T2 obj2, ref T3 obj3, ref T4 obj4);
}