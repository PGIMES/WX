using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Users 的摘要说明
/// </summary>
public class WPGIUserEn
{
    public WPGIUserEn()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 员工编号
    /// </summary>
    public string workcode { get; set; }

    /// <summary>
    /// 员工姓名
    /// </summary>
    public string workname { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string telphone { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string password { get; set; }

    /// <summary>
    /// 全局凭证唯一Id
    /// </summary>
    public string openid { get; set; }
}