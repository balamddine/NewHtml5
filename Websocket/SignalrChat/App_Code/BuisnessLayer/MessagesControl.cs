using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;

/// <summary>
/// Summary description for UserControl
/// </summary>
public class MessagesControl
{
   public List<Messages> GetAllMessages()
   {
        List<Messages> L = new List<Messages>();
        Messages Message = null;
        SqlDataReader rdr = Provider.DataAccessConcrete().GetAllMessages();
        while (rdr.Read()) {
            Message = new Messages();
            Message.ID = long.Parse(rdr["ID"].ToString());
            Message.Message = rdr["Message"].ToString();
            Message.Status = rdr[ "Status"].ToString();
            Message.ToID = long.Parse(rdr["ToID"].ToString());
            Message.FromID = long.Parse(rdr["FromID"].ToString());
            Message.DateSend = DateTime.Parse(rdr["DateSend"].ToString());
            L.Add(Message);
        }
        return L;
   }

    public Messages GetMessageByID(long ID)
    {
        Messages Message = new Messages();
        SqlDataReader rdr = Provider.DataAccessConcrete().GetMessageByID(ID);
        if (rdr.Read())
        {
            Message = new Messages();
            Message.ID = long.Parse(rdr["ID"].ToString());
            Message.Message = rdr["Message"].ToString();
            Message.Status = rdr["Status"].ToString();
            Message.ToID = long.Parse(rdr["ToID"].ToString());
            Message.FromID = long.Parse(rdr["FromID"].ToString());
            Message.DateSend = DateTime.Parse(rdr["DateSend"].ToString());
        }

        return Message;
    }

    public List<Messages> GetMessagesByPaging(long pagesize,long startindex)
    {
        List<Messages> L = new List<Messages>();
        Messages Message = null;
        SqlDataReader rdr = Provider.DataAccessConcrete().GetMessagesByPaging(pagesize, startindex);
        while (rdr.Read())
        {
            Message = new Messages();
            Message.ID = long.Parse(rdr["ID"].ToString());
            Message.Message = rdr["Message"].ToString();
            Message.Status = rdr["Status"].ToString();
            Message.ToID = long.Parse(rdr["ToID"].ToString());
            Message.FromID = long.Parse(rdr["FromID"].ToString());
            Message.DateSend = DateTime.Parse(rdr["DateSend"].ToString());
            L.Add(Message);
        }
        return L;
    }

    public void UpdateUserContext(Userrs user)
    {
         new DataAccess().UpdateUserContext(user.ID,user.ContextID);
    }

    public Userrs GetUserByEmail(string Email)
    {
        Userrs User = new Userrs();
        SqlDataReader rdr = new DataAccess().GetUserByEmail(Email);
        if(rdr.Read())
        {
            User = new Userrs();
            User.ID = long.Parse(rdr["ID"].ToString());
            User.Name = rdr["Name"].ToString();
            User.Status = rdr["Status"].ToString();
            User.Email = rdr["Email"].ToString();
            User.ContextID = rdr["ContextID"].ToString();
            User.LastConnected = DateTime.Parse(rdr["LastConnected"].ToString());
        }
        
        return User;
    }

    public void UpdateUser(Userrs user)
    {
        //throw new NotImplementedException();
    }
}