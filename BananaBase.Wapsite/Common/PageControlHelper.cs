using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Banana.Wapsite.Common;

namespace Banana.Wapsite
{
    public static class PageControlHelper
    {
        #region Script

        

        #region private static readonly string

        private static readonly string Scritp_AlertAndRedirect = @" <script type=""text/javascript"" language=""javascript"">
                                                                        alert(""{0}"");location.href=""{1}"";
                                                                    </script>";
        private static readonly string Scritp_Alert = @" <script type=""text/javascript"" language=""javascript"">
                                                                         alert(""{0}""); 
                                                                    </script>";

        private static readonly string Scritp_TopRedirect = @" <script type=""text/javascript"" language=""javascript"">" +
                                                                    @"top.location.href=""{0}"";
                                                                    </script>";
        private static readonly string Scritp_ToRedirect = @" <script type=""text/javascript"" language=""javascript"">" +
                                                                    @"location.href=""{0}"";
                                                                    </script>";

        private static readonly string Scritp_AlertAndBackHistory = @" <script type=""text/javascript"" language=""javascript"">
                                                                        alert(""{0}"");history.back();;
                                                                    </script>";

        #endregion

        #region AlertAndRedirect
        /// <summary>
        /// 弹出消息并跳转到url
        /// </summary>
        /// <param name="p"></param>
        /// <param name="message"></param>
        /// <param name="url"></param>
        public static void AlertAndRedirect(this Page p, string message, string url)
        {
            AlertAndRedirect(p, message, url, false);
        }
        /// <summary>
        /// 弹出消息并跳转到url
        /// </summary>
        /// <param name="p"></param>
        /// <param name="message"></param>
        /// <param name="url"></param>
        /// <param name="isAjax">是否是Ajax</param>
        public static void AlertAndRedirect(this Page p, string message, string url, bool isAjax)
        {
            if (isAjax)
                ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "AlertAndRedirect", string.Format(Scritp_AlertAndRedirect, Replace(message), url), false);
            else
                p.ClientScript.RegisterClientScriptBlock(p.GetType(), "AlertAndRedirect", string.Format(Scritp_AlertAndRedirect, Replace(message), url));
        }

        /// <summary>
        /// 弹出消息并跳转跳转到上一页
        /// </summary>
        public static void AlertAndBackHistory(this Page p, string message, bool isAjax)
        {
            if (isAjax)
                ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "AlertAndBackHistory", string.Format(Scritp_AlertAndBackHistory, Replace(message)), false);
            else
                p.ClientScript.RegisterClientScriptBlock(p.GetType(), "AlertAndBackHistory", string.Format(Scritp_AlertAndBackHistory, Replace(message)));
        }

        #endregion

        #region Alert
        #region Alert
        public static void Alert(this Page p, string message)
        {
            p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Scritp_Alert", string.Format(Scritp_Alert, Replace(message)));
        }

        public static void Alert(this Page p, Exception ex)
        {
            //  LogHelper.WriteError(ex.Message, ex);

            p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Scritp_Alert", string.Format(Scritp_Alert, Replace(ex.Message)));
        }
        public static void StartupScriptAlert(this Page p, string message, bool isAjax)
        {
            ScriptManager.RegisterStartupScript(p, p.GetType(), "Scritp_Alert", string.Format(Scritp_Alert, Replace(message)), false);
        }

        public static void Alert(this Page p, Exception ex, bool isAjax)
        {
            //  LogHelper.WriteError(ex.Message, ex);
            if (isAjax)
                StartupScriptAlert(p, ex.Message, isAjax);
            else
                p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Scritp_Alert", string.Format(Scritp_Alert, Replace(ex.Message)));
        }

        public static void Alert(this Page p, string message, bool isAjax)
        {
            if (isAjax)
                StartupScriptAlert(p, message, isAjax);
            else
                p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Scritp_Alert", string.Format(Scritp_Alert, Replace(message)));
        }
        #endregion

        public static void RegisterClientScriptBlock(this Page p, string script)
        {
            RegisterClientScriptBlock(p, script, false);
        }
        public static void RegisterClientScriptBlock(this Page p, string script, bool isAjax)
        {
            if (isAjax)
                ScriptManager.RegisterClientScriptBlock(p, p.GetType(), script.GetHashCode().ToString(), script, true);
            else
                p.ClientScript.RegisterClientScriptBlock(p.GetType(), script.GetHashCode().ToString(), script, true);
        }
        public static void RegisterStartupScript(this Page p, string script)
        {
            RegisterStartupScript(p, script, false);
        }
        public static void RegisterStartupScript(this Page p, string script, bool isAjax)
        {
            if (isAjax)
                ScriptManager.RegisterStartupScript(p, p.GetType(), script.GetHashCode().ToString(), script, true);
            else
                p.ClientScript.RegisterStartupScript(p.GetType(), script.GetHashCode().ToString(), script, true);
        }

        /// <summary>
        ///  跳转到url(框架)
        /// </summary>
        /// <param name="p"></param> 
        /// <param name="url"></param>
        public static void TopRedirect(this Page p, string url)
        {
            p.Response.TopRedirect(url);
        }

        /// <summary>
        ///  跳转到url(框架)
        /// </summary>
        /// <param name="p"></param> 
        /// <param name="url"></param>
        public static void TopRedirect(this HttpResponse response, string url)
        {
            response.Write(string.Format(Scritp_TopRedirect, url));
            response.End();
        }

        /// <summary>
        ///  跳转到url(框架)
        /// </summary>
        /// <param name="p"></param> 
        /// <param name="url"></param>
        public static void ToRedirect(this Page p, string url, bool isAjax)
        {
            if (isAjax)
                ScriptManager.RegisterClientScriptBlock(p, p.GetType(), "ToRedirect", string.Format(Scritp_ToRedirect, url), false);
            else
                p.ClientScript.RegisterClientScriptBlock(p.GetType(), "ToRedirect", string.Format(Scritp_ToRedirect, url));
        }

        public static string Replace(string s)
        {
            if (s.NotNullOrEmpty())
                s = System.Text.RegularExpressions.Regex.Replace(s, @"\r", "\\r");
            if (s.NotNullOrEmpty())
                s = System.Text.RegularExpressions.Regex.Replace(s, @"\n", "\\n");
            if (s.NotNullOrEmpty())
                s = s.Replace(@"""", @"\""");
            return s;
        }

        #endregion

        #region
        public static void dialogClose(this Page p, string message, bool isAjax)
        {
            if (isAjax)
            {
                if (message.NoEmpty())
                    ScriptManager.RegisterStartupScript(p, p.GetType(), "Scritp_Alert", "<script language='javascript'>alert('" + message.Replace("'", @"\'") + "');func(); function func(){parent.updialog.dialog('close');}</script>", false);
                else
                    ScriptManager.RegisterStartupScript(p, p.GetType(), "Scritp_Alert", "<script language='javascript'>func(); function func(){parent.updialog.dialog('close');}</script>", false);
            }
            else
            {
                if (message.NoEmpty())
                    p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Scritp_Alert", "<script language='javascript'>alert('" + message.Replace("'", @"\'") + "');func(); function func(){parent.updialog.dialog('close');}</script>");
                else
                    p.ClientScript.RegisterClientScriptBlock(p.GetType(), "Scritp_Alert", "<script language='javascript'>func(); function func(){parent.updialog.dialog('close');}</script>");
            }

        }
        #endregion

        #region ExecuteAjax
        public static void ExecuteAjax(this Page p, Action exec, string sucMsg)
        {
            ExecuteAjax(p, exec, sucMsg, "", false);
        }

        public static void ExecuteAjax(this Page p, Action exec, string sucMsg, string gourl)
        {
            ExecuteAjax(p, exec, sucMsg, gourl, false);
        }
        public static void ExecuteAjax(this Page p, Action exec, string sucMsg, string gourl, bool isTrans)
        {
            if (isTrans)
            {
                try
                {
                    TransactionsUtility.Action(() =>
                    {
                        exec();
                    });
                    if (gourl.NotNullOrEmpty())
                    {
                        if (sucMsg.NotNullOrEmpty())
                            p.AlertAndRedirect(sucMsg, gourl, true);
                        else
                            p.ToRedirect(gourl, true);
                    }
                    else
                    {
                        if (sucMsg.NotNullOrEmpty())
                            p.Alert(sucMsg, true);
                    }
                }
                catch (Exception ex)
                {
                    if (gourl.NotNullOrEmpty())
                    {
                        p.AlertAndRedirect(ex.Message, gourl, true);
                    }
                    else
                    {
                        p.Alert(ex.Message, true);
                    }
                }
            }
            else
            {
                try
                {
                    exec();
                    if (gourl.NotNullOrEmpty())
                    {
                        if (sucMsg.NotNullOrEmpty())
                            p.AlertAndRedirect(sucMsg, gourl, false);
                        else
                            p.ToRedirect(gourl, false);
                    }
                    else
                    {
                        if (sucMsg.NotNullOrEmpty())
                            p.Alert(sucMsg, false);
                    }
                }
                catch (Exception ex)
                {
                    if (gourl.NotNullOrEmpty())
                        p.AlertAndRedirect(ex.Message, gourl, false);
                    else
                        p.Alert(ex.Message, false);
                }
            }
        }

        #endregion

        #endregion
    }
}