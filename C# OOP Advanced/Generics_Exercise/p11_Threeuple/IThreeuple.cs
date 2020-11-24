namespace p11_Threeuple
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IThreeuple<T1, T2, T3>
    {
        T1 Item1 { get; }
        T2 Item2 { get; }
        T3 Item3 { get; }
    }
}
