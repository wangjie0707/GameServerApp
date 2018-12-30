using System.Collections;
using System.Collections.Generic;
public static class StringUtil {

    /// <summary>
    /// 把string类型转换成int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }

    public static short ToShort(this string str)
    {
        short temp = 0;
        short.TryParse(str, out temp);
        return temp;
    }

    /// <summary>
    /// 把string类型转换成long
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long ToLong(this string str)
    {
        long temp = 0;
        long.TryParse(str, out temp);
        return temp;
    }

    /// <summary>
    /// 把string类型转换成float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float temp = 0;
        float.TryParse(str, out temp);
        return temp;
    }
}
