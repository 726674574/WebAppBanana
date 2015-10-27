using System;

namespace Banana.Wapsite.Common
{
    public static class StringHelper
    {
        #region NullOrEmpty
        public static bool IsNullOrEmpty(this String str)
        {
            if (str == null)
                return true;

            String s = str.Trim();

            return s.Length == 0;
        }
        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NoEmpty(this string s)
        {
            if (s == null)
                return false;
            return !string.IsNullOrEmpty(s.Trim());
        }
        public static bool IsNullOrEmpty(String str, Action trueMethod)
        {
            bool rtn = IsNullOrEmpty(str);

            if (rtn)
            {
                trueMethod();
            }

            return rtn;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string s)
        {
            if (s == null)
                return true;
            return string.IsNullOrEmpty(s.Trim());
        }
        public static bool NotNullOrEmpty(this String str)
        {
            return !IsNullOrEmpty(str);
        }

        #endregion

        #region Equals
        /// <summary>
        /// 对比两个字符串是否相等
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static bool CompareEquals(this String str1, String str2)
        {
            return str1.Equals(str2, StringComparison.CurrentCultureIgnoreCase);
        }
        #endregion

        #region To
        public static int ToInt(this String str)
        {
            return ToInt(str, 0);
        }
        /// <summary>
        /// 去除空格
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Trim2(this string s)
        {
            return string.IsNullOrEmpty(s) ? string.Empty : s.Trim();
        }
        public static int ToInt(this String str, int def)
        {
            int rtn = def;
            int.TryParse(str, out rtn);

            return rtn;
        }

        public static decimal ToDecimal(this String str)
        {
            return ToDecimal(str, 0);
        }
        public static decimal ToDecimal(this String str, decimal def)
        {
            decimal rtn = def;
            decimal.TryParse(str, out rtn);

            return rtn;
        }

        public static DateTime ToDateTime(this String str)
        {
            return ToDateTime(str, DateTime.Now);
        }
        public static DateTime ToDateTime(this String str, DateTime def)
        {
            DateTime rtn = def;

            DateTime.TryParse(str, out rtn);

            return rtn;
        }

        public static string GetString(this String str)
        {
            if (str == null)
                return "";
            return str.Trim();
        }

        public static Byte ToByte(this Boolean b)
        {
            return Convert.ToByte(b);
        }

        #endregion


        public static bool IsStringDate(this string s)
        {
            DateTime y = new DateTime();
            try
            {
                y = DateTime.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsInteger(this string s)
        {
            bool bRet = false;
            if (!String.IsNullOrEmpty(s))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(s, @"[+-]?\d+"))
                {
                    try
                    {
                        Int64 iTemp = Convert.ToInt32(s);
                        bRet = true;
                    }
                    catch
                    {
                    }
                }
            }
            return bRet;
        }

        public static string ReplaceHtmlTag(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
                strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

                return strText;
            }
            return html;
        }
    }
}