﻿using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Chat : System.Web.UI.Page
{
    Userrs MyUser = new Userrs();
    UserControl MyUserCtrl = new UserControl();
    List<Userrs> Friends = new List<Userrs>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (Session["User"]==null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                MyUser = (Userrs)Session["User"];
                FillInfo();
                Friends = MyUserCtrl.GetAllFriends(MyUser.ID);
                FrdRpt.DataSource = Friends;
                FrdRpt.DataBind();
            }
            
        }
    }
    [WebMethod(EnableSession =true)]
    public static string UpdateUserInfo(string ContextID)
    {
        if (HttpContext.Current.Session["User"] != null)
        {
            Userrs u = (Userrs)HttpContext.Current.Session["User"];
            u.ContextID = ContextID;
            UserControl MyUserCtrl = new UserControl();
            MyUserCtrl.UpdateUserContext(u);
            HttpContext.Current.Session["User"] = u;
            return DateTime.Now.ToString();
        }
        return "";
    }
    [WebMethod(EnableSession = true)]
    public static string GetchatBody(long ToId,long pageSize)
    {
        string html = "";
        if (HttpContext.Current.Session["User"] != null)
        {
            Userrs u = (Userrs)HttpContext.Current.Session["User"];
            MessagesControl MyMessagesCtrl = new MessagesControl();
            List<Messages> MessagesL = new List<Messages>();
            MessagesL= MyMessagesCtrl.GetMessagesByPaging(pageSize,0,u.ID,ToId);
            Userrs Touser = new UserControl().GetUserByID(ToId);
            if (MessagesL.Count > 0)
            {
                foreach (Messages Message in MessagesL)
                {
                   
                    if (Message.FromID == u.ID)
                        html += "<li>You: " + Message.Message+"</li>";
                    else
                        html += "<li><span style='font-weight:bold'>"+Touser.Name + "</span>: " + Message.Message+"</li>";
                }
            }
            
        }
        return html;
    }
    
    private void FillInfo()
    {
        hfUserId.Value = MyUser.ID.ToString();
        ltWelcome.Text = MyUser.Email;
        ltName.Text = MyUser.Name;
        ltEmail.Text = MyUser.Email;
        
    }

    protected void FrdRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Userrs user = (Userrs)e.Item.DataItem;
        Literal litName = (Literal)e.Item.FindControl("litName");
        HtmlGenericControl lifriend =(HtmlGenericControl)e.Item.FindControl("lifriend");
        litName.Text = user.Name;
        lifriend.Attributes.Add("onclick", "InitChat("+ user.ID + ");");
    }
}