using System;
using System.Globalization;

namespace TaskSystem
{
    public class BC_Fmt
    {
        
        // 日期格式化
        private static DateTimeFormatInfo dtFormat;

        // 将object转换成日期类型
        public static DateTime FormatToDateTime(object date, string format = "yyyy/MM/dd HH:mm:ss:ffff")
        {
            if (dtFormat == null)
            {
                dtFormat = new DateTimeFormatInfo();
            }
            dtFormat.ShortDatePattern = format;
            return Convert.ToDateTime(date, dtFormat);
        }
        // 将DateTime转换成字符串类型
        public static string FormatToDateTimeStr(DateTime date, string format = "yyyy/MM/dd HH:mm:ss:ffff")
        {
            return date.ToString(format);
        }

    }
}