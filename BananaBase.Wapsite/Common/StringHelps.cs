using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Collections;
using System.Net;
using System.IO;
using System.Drawing;

namespace Banana.Manage
{
    /// <summary>
    /// 字符串处理类
    /// </summary>
    public class StringHelps
    {
        public static string FilterXmlMetachar(string value)
        {
            if (String.IsNullOrEmpty(value))
                return String.Empty;

            char[] charArray = value.ToCharArray();
            StringBuilder text = new StringBuilder();
            for (int i = 0; i < charArray.Length; i++)
            {
                switch (charArray[i])
                {
                    case '&':
                        text.Append(" ");
                        break;
                    case '<':
                        text.Append(" ");
                        break;
                    case '>':
                        text.Append(" ");
                        break;
                    case '\'':
                        text.Append(" ");
                        break;
                    case '"':
                        text.Append(" ");
                        break;
                    default:
                        text.Append(charArray[i]);
                        break;
                }
            }

            return text.ToString();
        }

        /// <summary>
        /// 获取字符串长度，英文：1，中文：2
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int GetLength(string source)
        {
            int len = 0;

            if (String.IsNullOrEmpty(source))
                return len;

            for (int i = 0; i < source.Length; i++)
            {
                if ((int)(source[i]) > 0xFF)
                    len = len + 2;
                else
                    len = len + 1;
            }

            return len;
        }

        /// <summary>
        /// 截取字符串，英文：1，中文：2
        /// </summary>
        /// <param name="source">指定字符串</param>
        /// <param name="len">要截取的长度</param>
        /// <returns></returns>
        public static string Substring(string source, int len)
        {
            if (String.IsNullOrEmpty(source))
                return String.Empty;

            int iLength = source.Length;
            int i = 0;
            for (; i < len && i < iLength; ++i)
            {
                if ((int)(source[i]) > 0xFF)
                    --len;
            }

            if (len < i) len = i;
            else if (len > iLength)
                len = iLength;

            return source.Substring(0, len);
        }


        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            #region 返回字符串真实长度, 1个汉字长度为2
            return Encoding.Default.GetBytes(str).Length;
            #endregion
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            #region 判断指定字符串在指定字符串数组中的位置
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }

            }
            return -1;
            #endregion
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            #region 判断指定字符串在指定字符串数组中的位置
            return GetInArrayID(strSearch, stringArray, true);
            #endregion
        }

        /// <summary>
        /// 去掉内容中的含有的图片（或者指定内容）
        /// </summary>
        /// <param name="Oldstring">原先送进来的字符串</param>
        /// <param name="starstring">开始字符</param>
        /// <param name="endstring">结束字符</param>
        /// <returns>返回去掉后的字符串</returns>
        public static string removeimg(string Oldstring, string starstring, string endstring)
        {
            #region 去掉内容中的含有的图片
            int c1 = 0;
            string s = starstring;
            string newstring = Oldstring;
            int n = 0;
            s = s.ToLower();
            newstring = newstring.ToLower();
            while ((c1 = newstring.IndexOf(s, c1)) >= 0)
            {
                n++;
                c1 += s.Length;
            }
            for (int i = 1; i <= n; i++)
            {
                int star = newstring.IndexOf(starstring);
                int end = newstring.IndexOf(endstring, star);
                newstring = newstring.Substring(0, star) + newstring.Substring(end + endstring.Length);
            }
            return newstring;
            #endregion
        }

        /// <summary>
        /// 移除所有的html标签
        /// </summary>
        /// <param name="HTML">html源代码</param>
        /// <returns></returns>
        public static string ParseTags(string HTML)
        {
            #region 移除所有的html标签
            return System.Text.RegularExpressions.Regex.Replace(HTML, "<[^>]*>", "");
            #endregion
        }

        /// <summary>
        /// 取得字符串中的部分内容
        /// </summary>
        /// <param name="Oldstring">原先送进来的字符串</param>
        /// <param name="starstring">开始字符</param>
        /// <param name="endstring">结束字符</param>
        /// <returns>返回字符串中的部分内容</returns>
        public static string CutString(string Oldstring, string starstring, string endstring)
        {
            #region 取得字符串中的部分内容
            string newstring = Oldstring;
            newstring = newstring.ToLower();
            int star = newstring.IndexOf(starstring);
            int end = newstring.IndexOf(endstring, star);
            newstring = newstring.Substring(star, end - star);
            return newstring;
            #endregion
        }

        // 取数组中的最大数
        public static string getmaxnum(string[] numarray)
        {
            #region 取数组中的最大数
            string maxnum = numarray[0];
            for (int i = 1; i < numarray.Length; i++)
            {
                if (int.Parse(numarray[i].ToString()) > int.Parse(maxnum.ToString()))
                    maxnum = numarray[i];
            }
            return maxnum;
            #endregion
        }

        //取float的n位小数位（四舍五入）
        public static float round_floatsf(float f, int bits)
        {
            #region 取float的n位小数位（四舍五入）
            int iii = ((int)(f * 10 + 0.5));
            float tt = (float.Parse(iii.ToString()) / 10);
            return tt;
            #endregion
        }

        //产生随机任意字母长度组合
        public static string RndNum1(int VcodeNum)
        {
            #region 产生随机任意字母长度组合
            string Vchar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string VNum = "";
            Random rand = new Random();
            for (int i = 0; i < VcodeNum; i++)
            {
                VNum += Vchar[rand.Next(Vchar.Length - 1)].ToString();
            }
            return VNum;
            #endregion

        }

        //返回:A46U-2VD4 形式的任意字母组合
        public static string RndNum2(int VcodeNum)
        {
            #region 返回:A46U-2VD4 形式的任意字母组合
            string Vchar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string VNum = "";
            Random rand = new Random();
            for (int i = 1; i <= VcodeNum; i++)
            {
                if (i % 4 == 0 && i != VcodeNum)
                {
                    VNum += Vchar[rand.Next(Vchar.Length - 1)].ToString() + "-";
                }
                else
                {
                    VNum += Vchar[rand.Next(Vchar.Length - 1)].ToString();
                }
            }
            return VNum;
            #endregion

        }

        //如：2|3|0|4 跟 1|4|2|3 两个字符串中的各个数相加
        public static string NewNumTotal(string oldchar, string newchar)
        {
            #region 如：2|3|0|4 跟 1|4|2|3 两个字符串中的各个数相加
            string finanyarray = null;
            string delimStr = "|";
            char[] delimiter = delimStr.ToCharArray();
            string[] oldarray = oldchar.Split(delimiter);//分割成数组
            string[] newarray = newchar.Split(delimiter);//分割成数组
            int num = oldarray.Length;//取总个数
            for (int i = 0; i < num; i++)
            {
                int j = i + 1;
                int newnum = int.Parse(oldarray[i]) + int.Parse(newarray[i]);//其中相对应的数相加
                oldarray[i] = newnum.ToString();//把加之后数再存到数组中
            }

            //把加之后字符串进行合并
            for (int i = 0; i < num; i++)
            {
                if (i == num - 1)
                {
                    finanyarray = finanyarray + oldarray[i];
                }
                else
                { finanyarray = finanyarray + oldarray[i] + "|"; }
            }
            return finanyarray;
            #endregion
        }

        /// <summary>
        ///  分割成数组 return oldstring.Split(new char[] { 'A' });//分割成数组
        /// </summary>
        /// <param name="oldstring">被分割的字符串</param>
        /// <param name="split_char">用来分割的字符</param>
        /// <returns></returns>
        public static string[] SplittoArray(string oldstring, string split_char)
        {
            #region 分割数组
            char[] delimiter = split_char.ToCharArray();
            string[] finanyarray = oldstring.Split(delimiter);
            return finanyarray;

            // return oldstring.Split(new char[] { 'A' });//分割成数组
            #endregion
        }


        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割字符</param>
        /// <param name="strSplit">进行分割的字符（可多个字符）</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit)
        {
            #region  分割字符串
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            #endregion
        }

        /// <summary>
        /// 截取字符串长度（用正则表达式）
        /// </summary>
        public static string GetFirstString(string stringToSub, int length, bool Addpoint)
        {
            #region 截取字符串长度（用正则表达式）
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;

            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > length)
                    break;
            }
            //如果截过则加上半个省略号 
            if (Addpoint)
            {
                if (nLength > length)
                    sb.Append("...");
            }
            return sb.ToString();
            #endregion
        }

        /// <summary>
        /// 截取字符串长度(转换bit)
        /// </summary>
        public static string CutString(string inputString, int len, bool Addpoint)
        {
            #region 截取字符串长度(转换bit)
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                { tempLen += 2; }
                else
                { tempLen += 1; }
                try
                { tempString += inputString.Substring(i, 1); }
                catch
                { break; }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号 
            if (Addpoint)
            {
                byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
                if (mybyte.Length > len)
                    tempString += "...";
            }
            return tempString;
            #endregion
        }

        //邮件格式是否正确
        public static bool isEmail(string inputEmail)
        {
            #region 邮件格式是否正确
            string strRegex = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
            {
                return true;
            }
            else
            {
                return false;
            }
            #endregion
        }

        //获取页面文件信息
        public static string GetWebContent(string Url)
        {
            #region 获取页面文件信息
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                //声明一个HttpWebRequest请求
                request.Timeout = 30000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
            }
            catch
            {

            }
            return strResult;
            #endregion
        }

        /// <summary>
        /// 获取客户端IP 
        /// </summary>
        public static string UserIp
        {
            #region 获取客户端IP  public static string UserIp
            get
            {
                string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip == null || ip == string.Empty)
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (!IsIp(ip))
                {
                    return "Unknown";
                }
                return ip;
            }
            #endregion
        }

        /// <summary> 
        /// 判断字符串是否是正确的 IP 地址格式
        /// </summary>
        public static bool IsIp(string s)
        {
            #region  判断字符串是否是正确的 IP 地址格式 public static bool IsIp(string s)
            string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
            return Regex.IsMatch(s, pattern);
            #endregion
        }

        /// <summary>
        /// 获取页面提交的参数值，相当于 Request.Form
        /// </summary>
        public static string Post(string name)
        {
            #region 获取页面提交的参数值，相当于 Request.Form public static string Post(string name)
            string value = HttpContext.Current.Request.Form[name];
            return value == null ? string.Empty : value.Trim();
            #endregion
        }

        /// <summary>
        /// 获取页面地址的参数值，相当于 Request.QueryString
        /// </summary>
        public static string Get(string name)
        {
            #region 获取页面地址的参数值，相当于 Request.QueryString public static string Get(string name)
            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
            #endregion
        }

        /// <summary>
        /// 获取页面地址的参数值并检查安全性，相当于 Request.QueryString
        /// chkType 有 CheckGetEnum.Int， CheckGetEnum.Safety两种类型，
        /// CheckGetEnum.Int 保证参数是数字型
        /// CheckGetEnum.Safety 保证提交的参数值没有操作数据库的语句
        /// </summary>
        public static string Get(string name, CheckGetEnum chkType)
        {
            #region 获取页面地址的参数值并检查安全性，相当于 Request.QueryString public static string Get(string name, CheckGetEnum chkType)
            string value = Get(name);
            bool isPass = false;
            switch (chkType)
            {
                default:
                    isPass = true;
                    break;
                case CheckGetEnum.Int:
                    {
                        try
                        {
                            int.Parse(value);
                            isPass = IsNumeric(value);
                        }
                        catch
                        {
                            isPass = false;
                        }
                        break;
                    }
                case CheckGetEnum.Safety:
                    isPass = IsSafety(value);
                    break;
            }
            if (!isPass)
            {
                //"地址栏中参数“" + name + "”的值不符合要求或具有潜在威胁，请不要手动修改URL。");
                return string.Empty;
            }
            return value;
            #endregion
        }


        /// <summary>
        /// 判断字符串是否由数字组成
        /// </summary>
        public static bool IsNumeric(string s)
        {
            #region 判断字符串是否由数字组成 public static bool IsNumeric(string s)
            string pattern = @"^\-?[0-9]+$";
            return Regex.IsMatch(s, pattern);
            #endregion
        }


        #region 枚举类型 QueryString值的检查
        /// <summary>
        /// QueryString值的检查
        /// </summary>
        public enum CheckGetEnum
        {
            Int = 0,
            Safety = 1
        }
        #endregion

        /// <summary>
        /// 判断字符串是否合法的日期格式
        /// </summary>
        public static bool IsDate(string value)
        {
            #region 判断字符串是否合法的日期格式 protected virtual bool IsDate(string value)
            try
            {
                System.DateTime.Parse(value);
            }
            catch
            {
                return false;
            }
            return true;
            #endregion
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            #region 获得当前绝对路径
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
            #endregion
        }

        /// <summary>
        /// 获得来源URL
        /// </summary>
        public static string Referrer
        {
            get
            {
                Uri uri = HttpContext.Current.Request.UrlReferrer;
                if (uri == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(uri);
            }
        }

        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            #region 判断文件名是否为浏览器可以直接显示的图片文件名
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
            #endregion
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            #region 检测是否是正确的Url
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
            #endregion
        }

        /// <summary>
        /// 进行指定的替换(脏字过滤)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            #region 进行指定的替换(脏字过滤)
            string text1 = "";
            string text2 = "";
            string[] textArray1 = SplitString(bantext, "\r\n");
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                text1 = textArray1[num1].Substring(0, textArray1[num1].IndexOf("="));
                text2 = textArray1[num1].Substring(textArray1[num1].IndexOf("=") + 1);
                str = str.Replace(text1, text2);
            }
            return str;
            #endregion
        }


        /// <summary>
        /// 检验是否存在此字符串中
        /// </summary>
        /// <param name="ckstring">要判断的字符串</param>
        /// <param name="strContent">要分割的字符串</param>
        /// <param name="strSplit">根据此符号进行分割</param>
        /// <returns></returns>
        public static bool CheckIfSplitString(string strContent, string strSplit, string ckstring)
        {
            #region 检验是否存在此字符串中
            string[] ck = null;
            if (strContent.IndexOf(strSplit) < 0)
            {
                if (strContent == ckstring)
                    return true;
                else
                    return false;
            }
            ck = Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);

            for (int i = 0; i < ck.Length; i++)
            {
                if (ckstring == ck[i].ToString())
                    return true;//检验是否都是数字
            }
            return false;
            #endregion
        }


        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            #region 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumeric(id))
                {
                    return false;
                }
            }
            return true;
            #endregion

        }

        /// <summary>
        /// 获得网站域名public static string ServerDomain
        /// </summary>
        public static string ServerDomain
        {
            #region 获得网站域名
            get
            {
                string host = HttpContext.Current.Request.Url.Host.ToLower();
                string[] arr = host.Split('.');
                if (arr.Length < 3 || IsIp(host))
                {
                    return host;
                }
                string domain = host.Remove(0, host.IndexOf(".") + 1);
                if (domain.StartsWith("com.") || domain.StartsWith("net.") || domain.StartsWith("org.") || domain.StartsWith("gov."))
                {
                    return host;
                }
                return domain;
            }
            #endregion
        }

        /// <summary>
        /// Url编码，相当于 Server.UrlEncode
        /// </summary>
        public static string UrlEncode(string url)
        {
            return HttpContext.Current.Server.UrlEncode(url);
        }

        /// <summary>
        /// Url解码，相当于 Server.UrlDecode
        /// </summary>
        public static string UrlDecode(string url)
        {
            return HttpContext.Current.Server.UrlDecode(url);
        }


        #region 编码转换
        //写入数据库时进行转换
        public static string GB2312(string write)
        {
            #region
            //声明字符集
            System.Text.Encoding iso8859, gb2312;
            //iso8859
            iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
            //国标2312
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] gb;
            gb = gb2312.GetBytes(write);
            //返回转换后的字符
            return iso8859.GetString(gb);
            #endregion
        }
        //读出时进行转换
        public static string ISO8859_GB2312_Read(string read)
        {
            #region
            //声明字符集
            System.Text.Encoding iso8859, gb2312;
            //iso8859
            iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
            //国标2312
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] iso;
            iso = iso8859.GetBytes(read);
            //返回转换后的字符
            return gb2312.GetString(iso);
            #endregion
        }


        //批量数据转换
        //其实就是将dataset的内容读出到xml文件，然后再输出
        public static DataSet ISO8859_GB2312(DataSet ds)
        {
            #region
            string xml;
            xml = ds.GetXml();
            ds.Clear();
            //声明字符集
            System.Text.Encoding iso8859, gb2312;
            //iso8859
            iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
            //国标2312
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] bt;
            bt = iso8859.GetBytes(xml);
            xml = gb2312.GetString(bt);
            ds.ReadXml(new System.IO.StringReader(xml));
            return ds;
            #endregion
        }
        #endregion


        /// <summary>
        /// 编码成 sql 文本可以接受的格式
        /// </summary>
        public static string SqlEncode(string s)
        {
            #region 编码成 sql 文本可以接受的格式
            if (null == s || 0 == s.Length)
            {
                return string.Empty;
            }

            return s.Trim().Replace("'", "''");
            #endregion
        }

        /// <summary>
        /// 判断字符串是否存在操作数据库的安全隐患
        /// </summary>
        public static bool IsSafety(string s)
        {
            #region 判断字符串是否存在操作数据库的安全隐患 public static bool IsSafety(string s)
            string str = s.Replace("%20", " ");
            str = Regex.Replace(str, @"\s", " ");
            string pattern = @"select |insert |delete from |count\(|drop table|update |truncate |asc\(|mid\(|char\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|""|\'| or ";
            return !Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
            #endregion
        }
        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileUrl">文件地址</param>
        public static void Deletefile(string fileUrl)
        {
            System.IO.File.Delete(fileUrl);
        }
        #endregion

    }


    /**/
    /// <summary>
    /// SqlKey 的摘要说明。
    /// </summary>
    public class SqlKey
    {
        private HttpRequest request;
        private const string StrKeyWord = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and|sa|with|<|alert|script";
        private const string StrRegex = @"-|;|,|/|(|)|[|]|}|{|%|@|*|!|'";
        public SqlKey(System.Web.HttpRequest _request)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.request = _request;
        }

        /**/
        /// <summary>
        /// 只读属性 SQL关键字
        /// </summary>
        public static string KeyWord
        {
            get
            {
                return StrKeyWord;
            }
        }
        /**/
        /// <summary>
        /// 只读属性过滤特殊字符
        /// </summary>
        public static string RegexString
        {
            get
            {
                return StrRegex;
            }
        }
        /**/
        /// <summary>
        /// 检查URL参数中是否带有SQL注入可能关键字。
        /// </summary>
        /// <param name="_request">当前HttpRequest对象</param>
        /// <returns>存在SQL注入关键字true存在，false不存在</returns>
        public bool CheckRequestQuery()
        {
            if (request.QueryString.Count != 0)
            {
                //若URL中参数存在，逐个比较参数。
                foreach (string i in this.request.QueryString)
                {
                    // 检查参数值是否合法。
                    if (i == "__VIEWSTATE") continue;
                    if (i == "__EVENTVALIDATION") continue;
                    if (CheckKeyWord(request.QueryString[i].ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /**/
        /// <summary>
        /// 检查URL参数中是否带有SQL注入可能关键字。
        /// </summary>
        /// <param name="_request">当前HttpRequest对象</param>
        /// <returns>存在SQL注入关键字true存在，false不存在</returns>
        public bool CheckRequestQueryint()
        {
            if (request.QueryString.Count != 0)
            {
                //若URL中参数存在，逐个比较参数。
                foreach (string i in this.request.QueryString)
                {
                    int intquer = 0;
                    try
                    {
                        intquer = int.Parse(request.QueryString[i].ToString());
                        return false;
                    }
                    catch { return true; }
                }
            }
            return false;
        }
        /**/
        /// <summary>
        /// 检查提交表单中是否存在SQL注入可能关键字
        /// </summary>
        /// <param name="_request">当前HttpRequest对象</param>
        /// <returns>存在SQL注入关键字true存在，false不存在</returns>
        public bool CheckRequestForm()
        {
            if (request.Form.Count > 0)
            {

                //获取提交的表单项不为0 逐个比较参数
                foreach (string i in this.request.Form)
                {
                    if (i == "__VIEWSTATE") continue;
                    if (i == "__EVENTVALIDATION") continue;
                    //检查参数值是否合法
                    if (CheckKeyWord(request.Form[i]))
                    {
                        //存在SQL关键字
                        return true;

                    }
                }
            }
            return false;
        }

        /**/
        /// <summary>
        /// 静态方法，检查_sword是否包涵SQL关键字
        /// </summary>
        /// <param name="_sWord">被检查的字符串</param>
        /// <returns>存在SQL关键字返回true，不存在返回false</returns>
        public static bool CheckKeyWord(string _sWord)
        {
            string word = _sWord;
            string[] patten1 = StrKeyWord.Split('|');
            string[] patten2 = StrRegex.Split('|');
            foreach (string i in patten1)
            {
                if (word.Contains(" " + i) || word.Contains(i + " "))
                {
                    return true;
                }
            }
            foreach (string i in patten2)
            {
                if (word.Contains(i))
                {
                    return true;
                }
            }
            return false;
        }

        /**/
        /// <summary>
        /// 反SQL注入:返回1无注入信息，否则返回错误处理
        /// </summary>
        /// <returns>返回1无注入信息，否则返回错误处理</returns>
        public string CheckMessage()
        {
            string msg = "1";
            if (CheckRequestQuery()) //CheckRequestQuery() || CheckRequestForm()
            {
                //msg = "<span style='font-size:24px;'>非法操作！<br>";
                //msg += "操作IP：" + request.ServerVariables["REMOTE_ADDR"] + "<br>";
                //msg += "操作时间：" + DateTime.Now + "<br>";
                //msg += "页面：" + request.ServerVariables["URL"].ToLower() + "<br>";
                //msg += "<a href="#" onclick="history.back()">返回上一页</a></span>";
            }
            return msg.ToString();
        }

        

    }
}
