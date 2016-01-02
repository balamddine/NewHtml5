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
        rdr.Close();
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
        rdr.Close();
        return Message;
    }

    public void AddMessage(Messages msg)
    {
        Provider.DataAccessConcrete().AddMessage(msg.FromID, msg.ToID, msg.Message, msg.Status);
    }

    public List<Messages> GetMessagesByPaging(long pagesize, long startindex, long FromID, long ToID)
    {
        List<Messages> L = new List<Messages>();
        Messages Message = null;
        SqlDataReader rdr = Provider.DataAccessConcrete().GetMessagesByPaging(pagesize, startindex, FromID, ToID);
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
        rdr.Close();
        return L;
    }
}