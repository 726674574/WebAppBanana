using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banana.Bll.Db;
using Banana.Wapsite.Common;

namespace Banana.Wapsite
{
    public partial class remai : System.Web.UI.Page
    {
        protected string pro_type1
        {
            get
            {
                try
                {
                    return Request.QueryString["pro_type1"].ToString();
                }
                catch
                {

                    return "";
                }
            }

        }
        /// <summary>
        /// 二级分类
        /// </summary>
        protected string pro_type2
        {
            get
            {
                try
                {
                    return Request.QueryString["pro_type2"].ToString();
                }
                catch
                {

                    return "";
                }
            }

        }
        /// <summary>
        ///当前 类别的一级类，
        /// </summary>
        protected string currenttype1
        {
            get
            {
                try
                {
                    if (pro_type1.NoEmpty())
                    {
                        return pro_type1;
                    }
                    else
                    {
                        return new ProductTypeBll().GetAll("top 1 *", " [typeint]=" + pro_type2, null, " id").Entity[0].Id.ToString();
                    }
                }
                catch
                {

                    return "";
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 加载菜单类别
        /// </summary>
        /// <returns></returns>
        public string loadCategory()
        {
            ProductTypeBll protypebll = new ProductTypeBll();
            StringBuilder sb = new StringBuilder();
            var typelist = protypebll.GetAll("*", " [type]=1", null, " [order] desc").Entity;
            string categorytemp = "<li {0}><h2><a href=\"#\">{1}</a></h2>{2}</li>";//0 是否当前类 1一级类名称 2 二级类名称 class=\"sel\"
            string childtemp = "<a href=\"prodlist.aspx?pro_type2={0}\" class=\"bdradius6\">{1}</a>";
            string childstr = "";
            if (typelist.Count > 0)
            {
                int index = 0;
                for (int i = 0; i < typelist.Count; i++)
                {
                    var childlist = protypebll.GetAll("*", " [typeint]=" + typelist[i].Id, null, " [order] desc").Entity;
                    if (childlist.Count > 0)
                    {
                        childstr = "";
                        childstr += "<p>";
                        for (int j = 0; j < childlist.Count; j++)
                        {
                            childstr += string.Format(childtemp, childlist[j].Id, childlist[j].Name);
                        }
                        childstr += "</p>";

                    }
                    //当前显示类
                    string parenttypeid = "";
                    if (pro_type2.NoEmpty())
                    {
                        parenttypeid = protypebll.GetByPrimaryKey(pro_type2.ToInt()).Entity == null ? "" : protypebll.GetByPrimaryKey(pro_type2.ToInt()).Entity.TypeInt.ToString();
                    }
                    if (currenttype1 == typelist[i].Id.ToString() || parenttypeid == typelist[i].Id.ToString())
                        sb.Append(string.Format(categorytemp, "class=\"sel\" onClick=\"flcontLiShowsSub(" + index + ")\"", typelist[i].Name, childstr));
                    else
                        sb.Append(string.Format(categorytemp, "onClick=\"flcontLiShowsSub(" + index + ")\"", typelist[i].Name, childstr));


                    index++;

                }
            }
            return sb.ToString(); ;
        }
    }
}