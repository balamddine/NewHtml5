using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    UserControl userCtrl = new UserControl();
    Userrs Userr = new Userrs();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0 && Request["lgtout"] != null && Request["lgtout"] != "")
            {
                Session["User"] = null;
                Session.Abandon();
            }
        }
        
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string Email = txtUserName.Text;
        string Password = TxtPassword.Text;
       
        Userr = userCtrl.GetUserByEmail(Email);
        if (Userr != null)
        {
            Session["User"] = Userr;
            Response.Redirect("Chat.aspx");
        }
        else
        {
            lbl_Error.Text = "Username/Password are incorrect";
        }
    }
}