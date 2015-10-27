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
    /// �ַ���������
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
        /// ��ȡ�ַ������ȣ�Ӣ�ģ�1�����ģ�2
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
        /// ��ȡ�ַ�����Ӣ�ģ�1�����ģ�2
        /// </summary>
        /// <param name="source">ָ���ַ���</param>
        /// <param name="len">Ҫ��ȡ�ĳ���</param>
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
        /// �����ַ�����ʵ����, 1�����ֳ���Ϊ2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            #region �����ַ�����ʵ����, 1�����ֳ���Ϊ2
            return Encoding.Default.GetBytes(str).Length;
            #endregion
        }

        /// <summary>
        /// �ж�ָ���ַ�����ָ���ַ��������е�λ��
        /// </summary>
        /// <param name="strSearch">�ַ���</param>
        /// <param name="stringArray">�ַ�������</param>
        /// <param name="caseInsensetive">�Ƿ����ִ�Сд, trueΪ������, falseΪ����</param>
        /// <returns>�ַ�����ָ���ַ��������е�λ��, �粻�����򷵻�-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            #region �ж�ָ���ַ�����ָ���ַ��������е�λ��
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
        /// �ж�ָ���ַ�����ָ���ַ��������е�λ��
        /// </summary>
        /// <param name="strSearch">�ַ���</param>
        /// <param name="stringArray">�ַ�������</param>
        /// <returns>�ַ�����ָ���ַ��������е�λ��, �粻�����򷵻�-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            #region �ж�ָ���ַ�����ָ���ַ��������е�λ��
            return GetInArrayID(strSearch, stringArray, true);
            #endregion
        }

        /// <summary>
        /// ȥ�������еĺ��е�ͼƬ������ָ�����ݣ�
        /// </summary>
        /// <param name="Oldstring">ԭ���ͽ������ַ���</param>
        /// <param name="starstring">��ʼ�ַ�</param>
        /// <param name="endstring">�����ַ�</param>
        /// <returns>����ȥ������ַ���</returns>
        public static string removeimg(string Oldstring, string starstring, string endstring)
        {
            #region ȥ�������еĺ��е�ͼƬ
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
        /// �Ƴ����е�html��ǩ
        /// </summary>
        /// <param name="HTML">htmlԴ����</param>
        /// <returns></returns>
        public static string ParseTags(string HTML)
        {
            #region �Ƴ����е�html��ǩ
            return System.Text.RegularExpressions.Regex.Replace(HTML, "<[^>]*>", "");
            #endregion
        }

        /// <summary>
        /// ȡ���ַ����еĲ�������
        /// </summary>
        /// <param name="Oldstring">ԭ���ͽ������ַ���</param>
        /// <param name="starstring">��ʼ�ַ�</param>
        /// <param name="endstring">�����ַ�</param>
        /// <returns>�����ַ����еĲ�������</returns>
        public static string CutString(string Oldstring, string starstring, string endstring)
        {
            #region ȡ���ַ����еĲ�������
            string newstring = Oldstring;
            newstring = newstring.ToLower();
            int star = newstring.IndexOf(starstring);
            int end = newstring.IndexOf(endstring, star);
            newstring = newstring.Substring(star, end - star);
            return newstring;
            #endregion
        }

        // ȡ�����е������
        public static string getmaxnum(string[] numarray)
        {
            #region ȡ�����е������
            string maxnum = numarray[0];
            for (int i = 1; i < numarray.Length; i++)
            {
                if (int.Parse(numarray[i].ToString()) > int.Parse(maxnum.ToString()))
                    maxnum = numarray[i];
            }
            return maxnum;
            #endregion
        }

        //ȡfloat��nλС��λ���������룩
        public static float round_floatsf(float f, int bits)
        {
            #region ȡfloat��nλС��λ���������룩
            int iii = ((int)(f * 10 + 0.5));
            float tt = (float.Parse(iii.ToString()) / 10);
            return tt;
            #endregion
        }

        //�������������ĸ�������
        public static string RndNum1(int VcodeNum)
        {
            #region �������������ĸ�������
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

        //����:A46U-2VD4 ��ʽ��������ĸ���
        public static string RndNum2(int VcodeNum)
        {
            #region ����:A46U-2VD4 ��ʽ��������ĸ���
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

        //�磺2|3|0|4 �� 1|4|2|3 �����ַ����еĸ��������
        public static string NewNumTotal(string oldchar, string newchar)
        {
            #region �磺2|3|0|4 �� 1|4|2|3 �����ַ����еĸ��������
            string finanyarray = null;
            string delimStr = "|";
            char[] delimiter = delimStr.ToCharArray();
            string[] oldarray = oldchar.Split(delimiter);//�ָ������
            string[] newarray = newchar.Split(delimiter);//�ָ������
            int num = oldarray.Length;//ȡ�ܸ���
            for (int i = 0; i < num; i++)
            {
                int j = i + 1;
                int newnum = int.Parse(oldarray[i]) + int.Parse(newarray[i]);//�������Ӧ�������
                oldarray[i] = newnum.ToString();//�Ѽ�֮�����ٴ浽������
            }

            //�Ѽ�֮���ַ������кϲ�
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
        ///  �ָ������ return oldstring.Split(new char[] { 'A' });//�ָ������
        /// </summary>
        /// <param name="oldstring">���ָ���ַ���</param>
        /// <param name="split_char">�����ָ���ַ�</param>
        /// <returns></returns>
        public static string[] SplittoArray(string oldstring, string split_char)
        {
            #region �ָ�����
            char[] delimiter = split_char.ToCharArray();
            string[] finanyarray = oldstring.Split(delimiter);
            return finanyarray;

            // return oldstring.Split(new char[] { 'A' });//�ָ������
            #endregion
        }


        /// <summary>
        /// �ָ��ַ���
        /// </summary>
        /// <param name="strContent">���ָ��ַ�</param>
        /// <param name="strSplit">���зָ���ַ����ɶ���ַ���</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit)
        {
            #region  �ָ��ַ���
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            #endregion
        }

        /// <summary>
        /// ��ȡ�ַ������ȣ���������ʽ��
        /// </summary>
        public static string GetFirstString(string stringToSub, int length, bool Addpoint)
        {
            #region ��ȡ�ַ������ȣ���������ʽ��
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
            //����ع�����ϰ��ʡ�Ժ� 
            if (Addpoint)
            {
                if (nLength > length)
                    sb.Append("...");
            }
            return sb.ToString();
            #endregion
        }

        /// <summary>
        /// ��ȡ�ַ�������(ת��bit)
        /// </summary>
        public static string CutString(string inputString, int len, bool Addpoint)
        {
            #region ��ȡ�ַ�������(ת��bit)
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
            //����ع�����ϰ��ʡ�Ժ� 
            if (Addpoint)
            {
                byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
                if (mybyte.Length > len)
                    tempString += "...";
            }
            return tempString;
            #endregion
        }

        //�ʼ���ʽ�Ƿ���ȷ
        public static bool isEmail(string inputEmail)
        {
            #region �ʼ���ʽ�Ƿ���ȷ
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

        //��ȡҳ���ļ���Ϣ
        public static string GetWebContent(string Url)
        {
            #region ��ȡҳ���ļ���Ϣ
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                //����һ��HttpWebRequest����
                request.Timeout = 30000;
                //�������ӳ�ʱʱ��
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
        /// ��ȡ�ͻ���IP 
        /// </summary>
        public static string UserIp
        {
            #region ��ȡ�ͻ���IP  public static string UserIp
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
        /// �ж��ַ����Ƿ�����ȷ�� IP ��ַ��ʽ
        /// </summary>
        public static bool IsIp(string s)
        {
            #region  �ж��ַ����Ƿ�����ȷ�� IP ��ַ��ʽ public static bool IsIp(string s)
            string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
            return Regex.IsMatch(s, pattern);
            #endregion
        }

        /// <summary>
        /// ��ȡҳ���ύ�Ĳ���ֵ���൱�� Request.Form
        /// </summary>
        public static string Post(string name)
        {
            #region ��ȡҳ���ύ�Ĳ���ֵ���൱�� Request.Form public static string Post(string name)
            string value = HttpContext.Current.Request.Form[name];
            return value == null ? string.Empty : value.Trim();
            #endregion
        }

        /// <summary>
        /// ��ȡҳ���ַ�Ĳ���ֵ���൱�� Request.QueryString
        /// </summary>
        public static string Get(string name)
        {
            #region ��ȡҳ���ַ�Ĳ���ֵ���൱�� Request.QueryString public static string Get(string name)
            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
            #endregion
        }

        /// <summary>
        /// ��ȡҳ���ַ�Ĳ���ֵ����鰲ȫ�ԣ��൱�� Request.QueryString
        /// chkType �� CheckGetEnum.Int�� CheckGetEnum.Safety�������ͣ�
        /// CheckGetEnum.Int ��֤������������
        /// CheckGetEnum.Safety ��֤�ύ�Ĳ���ֵû�в������ݿ�����
        /// </summary>
        public static string Get(string name, CheckGetEnum chkType)
        {
            #region ��ȡҳ���ַ�Ĳ���ֵ����鰲ȫ�ԣ��൱�� Request.QueryString public static string Get(string name, CheckGetEnum chkType)
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
                //"��ַ���в�����" + name + "����ֵ������Ҫ������Ǳ����в���벻Ҫ�ֶ��޸�URL��");
                return string.Empty;
            }
            return value;
            #endregion
        }


        /// <summary>
        /// �ж��ַ����Ƿ����������
        /// </summary>
        public static bool IsNumeric(string s)
        {
            #region �ж��ַ����Ƿ���������� public static bool IsNumeric(string s)
            string pattern = @"^\-?[0-9]+$";
            return Regex.IsMatch(s, pattern);
            #endregion
        }


        #region ö������ QueryStringֵ�ļ��
        /// <summary>
        /// QueryStringֵ�ļ��
        /// </summary>
        public enum CheckGetEnum
        {
            Int = 0,
            Safety = 1
        }
        #endregion

        /// <summary>
        /// �ж��ַ����Ƿ�Ϸ������ڸ�ʽ
        /// </summary>
        public static bool IsDate(string value)
        {
            #region �ж��ַ����Ƿ�Ϸ������ڸ�ʽ protected virtual bool IsDate(string value)
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
        /// ��õ�ǰ����·��
        /// </summary>
        /// <param name="strPath">ָ����·��</param>
        /// <returns>����·��</returns>
        public static string GetMapPath(string strPath)
        {
            #region ��õ�ǰ����·��
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //��web��������
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
            #endregion
        }

        /// <summary>
        /// �����ԴURL
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
        /// �ж��ļ����Ƿ�Ϊ���������ֱ����ʾ��ͼƬ�ļ���
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <returns>�Ƿ����ֱ����ʾ</returns>
        public static bool IsImgFilename(string filename)
        {
            #region �ж��ļ����Ƿ�Ϊ���������ֱ����ʾ��ͼƬ�ļ���
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
        /// ����Ƿ�����ȷ��Url
        /// </summary>
        /// <param name="strUrl">Ҫ��֤��Url</param>
        /// <returns>�жϽ��</returns>
        public static bool IsURL(string strUrl)
        {
            #region ����Ƿ�����ȷ��Url
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
            #endregion
        }

        /// <summary>
        /// ����ָ�����滻(���ֹ���)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            #region ����ָ�����滻(���ֹ���)
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
        /// �����Ƿ���ڴ��ַ�����
        /// </summary>
        /// <param name="ckstring">Ҫ�жϵ��ַ���</param>
        /// <param name="strContent">Ҫ�ָ���ַ���</param>
        /// <param name="strSplit">���ݴ˷��Ž��зָ�</param>
        /// <returns></returns>
        public static bool CheckIfSplitString(string strContent, string strSplit, string ckstring)
        {
            #region �����Ƿ���ڴ��ַ�����
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
                    return true;//�����Ƿ�������
            }
            return false;
            #endregion
        }


        /// <summary>
        /// �жϸ������ַ�������(strNumber)�е������ǲ��Ƕ�Ϊ��ֵ��
        /// </summary>
        /// <param name="strNumber">Ҫȷ�ϵ��ַ�������</param>
        /// <returns>���򷵼�true �����򷵻� false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            #region �жϸ������ַ�������(strNumber)�е������ǲ��Ƕ�Ϊ��ֵ��
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
        /// �����վ����public static string ServerDomain
        /// </summary>
        public static string ServerDomain
        {
            #region �����վ����
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
        /// Url���룬�൱�� Server.UrlEncode
        /// </summary>
        public static string UrlEncode(string url)
        {
            return HttpContext.Current.Server.UrlEncode(url);
        }

        /// <summary>
        /// Url���룬�൱�� Server.UrlDecode
        /// </summary>
        public static string UrlDecode(string url)
        {
            return HttpContext.Current.Server.UrlDecode(url);
        }


        #region ����ת��
        //д�����ݿ�ʱ����ת��
        public static string GB2312(string write)
        {
            #region
            //�����ַ���
            System.Text.Encoding iso8859, gb2312;
            //iso8859
            iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
            //����2312
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] gb;
            gb = gb2312.GetBytes(write);
            //����ת������ַ�
            return iso8859.GetString(gb);
            #endregion
        }
        //����ʱ����ת��
        public static string ISO8859_GB2312_Read(string read)
        {
            #region
            //�����ַ���
            System.Text.Encoding iso8859, gb2312;
            //iso8859
            iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
            //����2312
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] iso;
            iso = iso8859.GetBytes(read);
            //����ת������ַ�
            return gb2312.GetString(iso);
            #endregion
        }


        //��������ת��
        //��ʵ���ǽ�dataset�����ݶ�����xml�ļ���Ȼ�������
        public static DataSet ISO8859_GB2312(DataSet ds)
        {
            #region
            string xml;
            xml = ds.GetXml();
            ds.Clear();
            //�����ַ���
            System.Text.Encoding iso8859, gb2312;
            //iso8859
            iso8859 = System.Text.Encoding.GetEncoding("iso8859-1");
            //����2312
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
        /// ����� sql �ı����Խ��ܵĸ�ʽ
        /// </summary>
        public static string SqlEncode(string s)
        {
            #region ����� sql �ı����Խ��ܵĸ�ʽ
            if (null == s || 0 == s.Length)
            {
                return string.Empty;
            }

            return s.Trim().Replace("'", "''");
            #endregion
        }

        /// <summary>
        /// �ж��ַ����Ƿ���ڲ������ݿ�İ�ȫ����
        /// </summary>
        public static bool IsSafety(string s)
        {
            #region �ж��ַ����Ƿ���ڲ������ݿ�İ�ȫ���� public static bool IsSafety(string s)
            string str = s.Replace("%20", " ");
            str = Regex.Replace(str, @"\s", " ");
            string pattern = @"select |insert |delete from |count\(|drop table|update |truncate |asc\(|mid\(|char\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|""|\'| or ";
            return !Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
            #endregion
        }
        #region ɾ���ļ�
        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="fileUrl">�ļ���ַ</param>
        public static void Deletefile(string fileUrl)
        {
            System.IO.File.Delete(fileUrl);
        }
        #endregion

    }


    /**/
    /// <summary>
    /// SqlKey ��ժҪ˵����
    /// </summary>
    public class SqlKey
    {
        private HttpRequest request;
        private const string StrKeyWord = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and|sa|with|<|alert|script";
        private const string StrRegex = @"-|;|,|/|(|)|[|]|}|{|%|@|*|!|'";
        public SqlKey(System.Web.HttpRequest _request)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.request = _request;
        }

        /**/
        /// <summary>
        /// ֻ������ SQL�ؼ���
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
        /// ֻ�����Թ��������ַ�
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
        /// ���URL�������Ƿ����SQLע����ܹؼ��֡�
        /// </summary>
        /// <param name="_request">��ǰHttpRequest����</param>
        /// <returns>����SQLע��ؼ���true���ڣ�false������</returns>
        public bool CheckRequestQuery()
        {
            if (request.QueryString.Count != 0)
            {
                //��URL�в������ڣ�����Ƚϲ�����
                foreach (string i in this.request.QueryString)
                {
                    // ������ֵ�Ƿ�Ϸ���
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
        /// ���URL�������Ƿ����SQLע����ܹؼ��֡�
        /// </summary>
        /// <param name="_request">��ǰHttpRequest����</param>
        /// <returns>����SQLע��ؼ���true���ڣ�false������</returns>
        public bool CheckRequestQueryint()
        {
            if (request.QueryString.Count != 0)
            {
                //��URL�в������ڣ�����Ƚϲ�����
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
        /// ����ύ�����Ƿ����SQLע����ܹؼ���
        /// </summary>
        /// <param name="_request">��ǰHttpRequest����</param>
        /// <returns>����SQLע��ؼ���true���ڣ�false������</returns>
        public bool CheckRequestForm()
        {
            if (request.Form.Count > 0)
            {

                //��ȡ�ύ�ı��Ϊ0 ����Ƚϲ���
                foreach (string i in this.request.Form)
                {
                    if (i == "__VIEWSTATE") continue;
                    if (i == "__EVENTVALIDATION") continue;
                    //������ֵ�Ƿ�Ϸ�
                    if (CheckKeyWord(request.Form[i]))
                    {
                        //����SQL�ؼ���
                        return true;

                    }
                }
            }
            return false;
        }

        /**/
        /// <summary>
        /// ��̬���������_sword�Ƿ����SQL�ؼ���
        /// </summary>
        /// <param name="_sWord">�������ַ���</param>
        /// <returns>����SQL�ؼ��ַ���true�������ڷ���false</returns>
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
        /// ��SQLע��:����1��ע����Ϣ�����򷵻ش�����
        /// </summary>
        /// <returns>����1��ע����Ϣ�����򷵻ش�����</returns>
        public string CheckMessage()
        {
            string msg = "1";
            if (CheckRequestQuery()) //CheckRequestQuery() || CheckRequestForm()
            {
                //msg = "<span style='font-size:24px;'>�Ƿ�������<br>";
                //msg += "����IP��" + request.ServerVariables["REMOTE_ADDR"] + "<br>";
                //msg += "����ʱ�䣺" + DateTime.Now + "<br>";
                //msg += "ҳ�棺" + request.ServerVariables["URL"].ToLower() + "<br>";
                //msg += "<a href="#" onclick="history.back()">������һҳ</a></span>";
            }
            return msg.ToString();
        }

        

    }
}
