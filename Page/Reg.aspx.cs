using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page_Reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WPGIUserEn user = (WPGIUserEn)HttpContext.Current.Session["user"];
        //如果当前用户未登陆，先获取授权 
        if (user == null)
        {
            WUserEn userInfo = PageShowQuan.GetShouQuanMessage();
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.OpenID))
            {
                //授权成功
                LogHelper.Write("第9步：" + userInfo.OpenID);

                WPGIUserEn wuser = UserModel.getWeChatUser(userInfo.OpenID);
                if (wuser == null || string.IsNullOrEmpty(wuser.workcode))
                {//账号未关联，跳转至登录界面
                    LogHelper.Write("第10步：" + userInfo.OpenID);
                    System.Web.HttpContext.Current.Response.Redirect(@"../Login.aspx?openid=" + userInfo.OpenID + "&nickname=" + userInfo.NickName + "&transferurl=SiteDeclareList");
                }
                else 
                {//无此权限
                    HttpContext.Current.Session["user"] = wuser;
                    System.Web.HttpContext.Current.Response.Redirect(@"../WarnPage.aspx");
                }
                LogHelper.Write("第12步：" + wuser.openid);
            }
            else
            {//获取授权失败，也跳转至登录页面
                System.Web.HttpContext.Current.Response.Redirect(@"../Login.aspx?openid=" + userInfo.OpenID + "&nickname=" + userInfo.NickName + "&transferurl=SiteDeclareList");
            }
        }
        else//无此权限
        {
            System.Web.HttpContext.Current.Response.Redirect(@"../WarnPage.aspx");
        }
    }

    [WebMethod]
    public static string RegExec(string nameText, string emCodeText, string mobileText, string passwordText)
    {
        IsoDateTimeConverter iso = new IsoDateTimeConverter();//序列化JSON对象时,日期的处理格式 
        iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        string tempsql = @"select b.*,a.createbyid,a.createbyName,a.createDate,a.prtype,a.prreason,a.applydept,a.deptname  
                    from PUR_PR_Main_Form a 
                        inner join PUR_PR_Dtl_Form b on a.prno=b.prno 
                     where b.id=''";


        DataTable dt = DbHelperSQL.Query(tempsql).Tables[0];
        var json = JsonConvert.SerializeObject(dt, iso);
        return json;
    }

}